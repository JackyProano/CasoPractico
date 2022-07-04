using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using CreditoAutomotriz.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CreditoAutomotriz.Domain.Interfaces;

namespace CreditoAutomotriz.Repository
{
    public class PatioRepository : IPatioRepository
    {
        private readonly IConfiguration _configuration;
        private readonly BDDCreditoAutomotrizContext _context;
        public PatioRepository(BDDCreditoAutomotrizContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Metodo que permite la creacion del patio
        /// </summary>
        /// <param name="objPatio">Registro a ser guardado</param>
        /// <returns>Resultado del guardado</returns>
        public async Task<Resultado> CreaPatio(Patio objPatio)
        {
            Resultado respuesta;
            respuesta = await ValidaExistenciaPatio(objPatio.NumeroPuntoVenta);
            if (respuesta.RegistroExistente)
            {
                respuesta.RegistroExistente = true;
                respuesta.MensajeRespuesta = Mensajes.PatioExiste;
                return respuesta;
            }
            _context.Patio.Add(objPatio);
            await _context.SaveChangesAsync();
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.RegistroCreado;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite editar un patio
        /// </summary>
        /// <param name="objPatio"></param>
        /// <returns>Resultado de la edicion</returns>
        public async Task<Resultado> EditaPatio(Patio objPatio)
        {
            Resultado respuesta;
            respuesta = await ValidaExistenciaPatio(objPatio.NumeroPuntoVenta);
            if (!respuesta.RegistroExistente)
            {
                respuesta.RegistroExistente = true;
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                return respuesta;
            }
            _context.Entry(objPatio).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(true);
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.RegistroModificado;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite eliminar un patio
        /// </summary>
        /// <param name="identificadorPatio">Id del patio a eliminar</param>
        /// <returns></returns>
        public async Task<Resultado> EliminaPatio(int identificadorPatio)
        {
            Resultado respuesta = new Resultado();
            var objPatio = await _context.Patio.FindAsync(identificadorPatio);
            if (objPatio == null)
            {
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                respuesta.ObjetoRespuesta = objPatio;
                return respuesta;
            }
            //Verificar que no este ligado con un ejecutivo
            IEnumerable<Ejecutivo> objEjecutivos = null;
            objEjecutivos = await (from m in _context.Ejecutivo
                                    where m.IdPatioLabora == identificadorPatio
                                   select m).ToListAsync();
            if (objEjecutivos.Count() > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ErrorEliminar;
                respuesta.ObjetoRespuesta = objPatio;
                return respuesta;
            }
            //Verificar que no este asigando a un cliente
            IEnumerable<AsignacionCliente> objAsignacionClientes = null;
            objAsignacionClientes = await (from x in _context.AsignacionCliente
                                   where x.IdPatio == identificadorPatio
                                   select x).ToListAsync();
            if (objAsignacionClientes.Count() > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ErrorEliminar;
                respuesta.ObjetoRespuesta = objPatio;
                return respuesta;
            }
            //Verificar que no este asigando a una solicitud
            IEnumerable<SolicitudCredito> objSolicitudesCredito = null;
            objSolicitudesCredito = await (from n in _context.SolicitudCredito
                                           where n.IdPatio == identificadorPatio
                                           select n).ToListAsync();
            if (objSolicitudesCredito.Count() > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ErrorEliminar;
                respuesta.ObjetoRespuesta = objPatio;
                return respuesta;
            }
            _context.Patio.Remove(objPatio);
            await _context.SaveChangesAsync();
            respuesta.MensajeRespuesta = Mensajes.RegistroEliminado;
            respuesta.ObjetoRespuesta = objPatio;
            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite consultar un patio por numero punto de venta
        /// </summary>
        /// <param name="numeroPuntoVenta">Numero de patio a consultar</param>
        /// <returns>resultado de la consulta</returns>
        public async Task<Resultado> ValidaExistenciaPatio(int numeroPuntoVenta)
        {
            Resultado respuesta = new Resultado();
            respuesta.RegistroExistente = await _context.Patio.AnyAsync(x => x.NumeroPuntoVenta == numeroPuntoVenta);
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite consultar un patio
        /// </summary>
        /// <param name="IdentificadorPatio">Id de patio a consultar</param>
        /// <returns>resultado de la consulta</returns>
        public async Task<Resultado> ConsultaPatio(int IdentificadorPatio)
        {
            Resultado respuesta = new Resultado();
            respuesta.ObjetoRespuesta = await _context.Patio.FindAsync(IdentificadorPatio);
            if (respuesta.ObjetoRespuesta == null)
            {
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                return respuesta;
            }
            respuesta.RegistroExistente = true;
            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }

    }
}
