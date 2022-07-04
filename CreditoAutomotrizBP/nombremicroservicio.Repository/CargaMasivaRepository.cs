using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using CreditoAutomotriz.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Repository
{
    public class CargaMasivaRepository : ICargaMasivaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly BDDCreditoAutomotrizContext _context;
        public CargaMasivaRepository(BDDCreditoAutomotrizContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Metodo que permite cargar datos iniciales desde un archivo
        /// </summary>
        /// <returns>Resultado de la carga inicial</returns>
        public async Task<Resultado> CargaInicial(string strTipoCarga)
        {
            string strDocumento = string.Empty;
            Resultado respuesta = new Resultado();
            switch (strTipoCarga)
            {
                case TipoCarga.CargaClientes:
                    strDocumento = _configuration["RutasFile:Clientes"];
                    break;
                case TipoCarga.CargaEjecutivos:
                    strDocumento = _configuration["RutasFile:Ejecutivos"];
                    break;
                case TipoCarga.CargaMarcas:
                    strDocumento = _configuration["RutasFile:Marcas"];
                    break;
            }
            respuesta = await ProcesaCarga(strDocumento, strTipoCarga);
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite procesar la carga dependiendo del tipo de carga
        /// </summary>
        /// <param name="strDocumento"></param>
        /// <param name="strTipoCarga"></param>
        /// <returns></returns>
        public async Task<Resultado> ProcesaCarga(string strDocumento, string strTipoCarga)
        {
            List<Cliente> lstCliente = new List<Cliente>();
            List<Ejecutivo> lstEjecutivo = new List<Ejecutivo>();
            List<Marca> lstMarca = new List<Marca>();
            Resultado respuesta = new Resultado();
            StreamReader StrArchivo = new StreamReader(strDocumento);
            string linea;
            while ((linea = StrArchivo.ReadLine()) != null)
            {
                string[] fila = linea.Split(";");
                switch (strTipoCarga)
                {
                    case TipoCarga.CargaClientes:
                        Cliente nuevoCliente = new Cliente()
                        {
                            Identificacion = fila[0],
                            Nombres = fila[1],
                            Edad = Convert.ToInt32(fila[2]),
                            FechaNacimiento = Convert.ToDateTime(fila[3]),
                            Apellidos = fila[4],
                            Direccion = fila[5],
                            Telefono = fila[6],
                            EstadoCivil = fila[7],
                            IdentificacionConyuge = fila[8],
                            NombresConyuge = fila[9],
                            EsSujetoCredito = Convert.ToBoolean(Convert.ToInt32(fila[10]))
                        };
                        lstCliente.Add(nuevoCliente);
                        break;
                    case TipoCarga.CargaEjecutivos:
                        Ejecutivo nuevoEjecutivo = new Ejecutivo()
                        {
                            Identificacion = fila[0],
                            Nombres = fila[1],
                            Apellidos = fila[2],
                            Direccion = fila[3],
                            TelefonoConvencional = fila[4],
                            Celular = fila[5],
                            IdPatioLabora = Convert.ToInt32(fila[6]),
                            Edad = Convert.ToInt32(fila[7])
                        };
                        lstEjecutivo.Add(nuevoEjecutivo);
                        break;
                    case TipoCarga.CargaMarcas:
                        Marca nuevaMarca = new Marca()
                        {
                            DescripcionMarca = fila[0]
                        };
                        lstMarca.Add(nuevaMarca);
                        break;
                    default:
                        break;
                }
            }
            switch (strTipoCarga)
            {
                case TipoCarga.CargaClientes:
                    respuesta = await GuardaClientes(lstCliente);
                    break;
                case TipoCarga.CargaEjecutivos:
                    respuesta = await GuardaEjecutivos(lstEjecutivo);
                    break;
                case TipoCarga.CargaMarcas:
                    respuesta = await GuardaMarcas(lstMarca);
                    break;
                default:
                    break;
            }
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite Guardar los clientes 
        /// </summary>
        /// <param name="lstCliente">Listado de clientes a guardar</param>
        /// <returns>Resultado del guardado</returns>
        public async Task<Resultado> GuardaClientes(List<Cliente> lstClientes)
        {
            Resultado respuesta = new Resultado();
            List<Cliente> clientesExistentes = new List<Cliente>();
            List<Cliente> clientesIngresados = new List<Cliente>(lstClientes);
            List<Cliente> clientesDuplicados = new List<Cliente>();
            bool clienteDuplicado = false;
            foreach (Cliente item in lstClientes)
            {
                respuesta = await ExisteCliente(item.Identificacion);
                if (respuesta.RegistroExistente)
                {
                    clientesIngresados.Remove(item);
                    clientesExistentes.Add(item);
                }
                clienteDuplicado = clientesIngresados.Where(x => x.Identificacion == item.Identificacion).Count() > 1;
                if (clienteDuplicado)
                {
                    clientesIngresados.Remove(item);
                    clientesExistentes.Add(item);
                }
            }
            if (clientesIngresados.Count > 0)
            {
                _context.Cliente.AddRange(clientesIngresados);
                await _context.SaveChangesAsync();
                respuesta.MensajeRespuesta = clientesIngresados.Count + Mensajes.ClientesGuardados;
            }
            if (clientesExistentes.Count > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ClienteExiste;
                respuesta.ObjetoRespuesta = clientesExistentes;
            }

            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite Guardar los ejecutivos 
        /// </summary>
        /// <param name="lstEjecutivo">Listado de ejecutivos a guardar</param>
        /// <returns>Resultado del guardado</returns>
        public async Task<Resultado> GuardaEjecutivos(List<Ejecutivo> lstEjecutivos)
        {
            Resultado respuesta = new Resultado();
            List<Ejecutivo> ejecutivosExistentes = new List<Ejecutivo>();
            List<Ejecutivo> ejecutivosIngresados = new List<Ejecutivo>(lstEjecutivos);
            bool ejecutivoDuplicado = false;
            foreach (Ejecutivo item in lstEjecutivos)
            {
                respuesta = await ExisteEjecutivo(item.Identificacion);
                if (respuesta.RegistroExistente)
                {
                    ejecutivosIngresados.Remove(item);
                    ejecutivosExistentes.Add(item);
                }
                ejecutivoDuplicado = ejecutivosIngresados.Where(x => x.Identificacion == item.Identificacion).Count() > 1;
                if (ejecutivoDuplicado)
                {
                    ejecutivosIngresados.Remove(item);
                    ejecutivosExistentes.Add(item);
                }
                bool existePatio = await _context.Patio.AnyAsync(x => x.IdPatio == item.IdPatioLabora);
                if(!existePatio)
                    ejecutivosIngresados.Remove(item);
            }

            if (ejecutivosIngresados.Count > 0)
            {
                _context.Ejecutivo.AddRange(ejecutivosIngresados);
                await _context.SaveChangesAsync();
                respuesta.MensajeRespuesta = ejecutivosIngresados.Count + Mensajes.EjecutivosGuardados;
            }
            if (ejecutivosExistentes.Count > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ClienteExiste;
                respuesta.ObjetoRespuesta = ejecutivosExistentes;
            }

            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite Guardar los marcas 
        /// </summary>
        /// <param name="lstMarcas">Listado de marcas a guardar</param>
        /// <returns>Resultado del guardado</returns>
        public async Task<Resultado> GuardaMarcas(List<Marca> lstMarcas)
        {
            Resultado respuesta = new Resultado();
            List<Marca> marcasExistentes = new List<Marca>();
            List<Marca> marcasIngresadas = new List<Marca>(lstMarcas);
            bool marcaDuplicada = false;
            foreach (Marca item in lstMarcas)
            {
                respuesta = await ExisteMarca(item.DescripcionMarca);
                if (respuesta.RegistroExistente)
                {
                    marcasIngresadas.Remove(item);
                    marcasExistentes.Add(item);
                }
                marcaDuplicada = marcasIngresadas.Where(x => x.DescripcionMarca == item.DescripcionMarca).Count() > 1;
                if (marcaDuplicada)
                {
                    marcasIngresadas.Remove(item);
                    marcasExistentes.Add(item);
                }
            }
            if (marcasIngresadas.Count > 0)
            {
                _context.Marca.AddRange(marcasIngresadas);
                await _context.SaveChangesAsync();
                respuesta.MensajeRespuesta = marcasIngresadas.Count + Mensajes.MarcasGuardadas;
            }
            if (marcasExistentes.Count > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ClienteExiste;
                respuesta.ObjetoRespuesta = marcasExistentes;
            }

            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }

        /// <summary>
        /// Metodo que valida si existe un cliente guardado con el mismo numero de identificacion
        /// </summary>
        /// <param name="Identificacion">Identificacion a buscar</param>
        /// <returns>Resultado de coincidencia</returns>
        public async Task<Resultado> ExisteCliente(string Identificacion)
        {
            Resultado respuesta = new Resultado();
            respuesta.RegistroExistente = await _context.Cliente.AnyAsync(x => x.Identificacion == Identificacion);
            return respuesta;
        }

        /// <summary>
        /// Metodo que valida si existe un ejecutivo guardado con el mismo numero de identificacion
        /// </summary>
        /// <param name="Identificacion">Identificacion a buscar</param>
        /// <returns>Resultado de coincidencia</returns>
        public async Task<Resultado> ExisteEjecutivo(string Identificacion)
        {
            Resultado respuesta = new Resultado();
            respuesta.RegistroExistente = await _context.Ejecutivo.AnyAsync(x => x.Identificacion == Identificacion);
            return respuesta;
        }

        /// <summary>
        /// Metodo que valida si existe un marca guardado con la misma descripcion
        /// </summary>
        /// <param name="Identificacion">Identificacion a buscar</param>
        /// <returns>Resultado de coincidencia</returns>
        public async Task<Resultado> ExisteMarca(string strmarca)
        {
            Resultado respuesta = new Resultado();
            respuesta.RegistroExistente = await _context.Marca.AnyAsync(x => x.DescripcionMarca == strmarca);
            return respuesta;
        }

    }
}
