using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using CreditoAutomotriz.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Repository
{
    public class AsignacionClienteRepository : IAsignacionClienteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly BDDCreditoAutomotrizContext _context;
        public AsignacionClienteRepository(BDDCreditoAutomotrizContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Metodo que permite crear la asigancion del cliente
        /// </summary>
        /// <param name="objAsignacioncliente">Datos de asignacion del cliente</param>
        /// <returns>resultado de la creacion</returns>
        public async Task<Resultado> CreaAsignacionClientePatio(AsignacionCliente objAsignacioncliente)
        {
            Resultado respuesta = new Resultado();
            respuesta = await ExisteAsignacion(objAsignacioncliente.IdCliente);
            if (respuesta.EjecucionCorrecta)
            {
                respuesta.MensajeRespuesta = Mensajes.AsignacionClienteExiste;
                respuesta.RegistroExistente = true;
                return respuesta;
            }
            var Cliente = await _context.Cliente.FindAsync(objAsignacioncliente.IdCliente);
            if(Cliente == null)
            {
                respuesta.MensajeRespuesta = Mensajes.ClienteNoExiste;
                respuesta.RegistroExistente = false;
                return respuesta;
            }
            var Patio = await _context.Patio.FindAsync(objAsignacioncliente.IdPatio);
            if (Patio == null)
            {
                respuesta.MensajeRespuesta = Mensajes.PatioNoExiste;
                respuesta.RegistroExistente = false;
                return respuesta;
            }
            objAsignacioncliente.FechaAsignacion = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            _context.AsignacionCliente.Add(objAsignacioncliente);
            await _context.SaveChangesAsync();
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.RegistroCreado;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite editar la asignacion de un cliente
        /// </summary>
        /// <param name="objAsignacioncliente">Datos de asignacion para modificar</param>
        /// <returns>Resultado de la modificacion</returns>
        public async Task<Resultado> EditaAsignacionClientePatio(AsignacionCliente objAsignacioncliente)
        {
            Resultado respuesta = new Resultado();
            var Cliente = await _context.Cliente.FindAsync(objAsignacioncliente.IdCliente);
            if (Cliente == null)
            {
                respuesta.MensajeRespuesta = Mensajes.ClienteNoExiste;
                respuesta.RegistroExistente = false;
                return respuesta;
            }
            var Patio = await _context.Patio.FindAsync(objAsignacioncliente.IdPatio);
            if (Patio == null)
            {
                respuesta.MensajeRespuesta = Mensajes.PatioNoExiste;
                respuesta.RegistroExistente = false;
                return respuesta;
            }
            _context.Entry(objAsignacioncliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            respuesta.MensajeRespuesta = Mensajes.RegistroModificado;
            respuesta.EjecucionCorrecta = true;
            respuesta.ObjetoRespuesta = objAsignacioncliente;
            return respuesta;
        }

        /// <summary>
        ///  Metodo que permite eliminar la asignacion de un cliente
        /// </summary>
        /// <param name="idAsignacion">Identificador de asignacion</param>
        /// <returns>Resultado</returns>
        public async Task<Resultado> EliminaAsignacionClientePatio(int idAsignacion)
        {
            Resultado respuesta = new Resultado();
            var objAsignacionCliente = await _context.AsignacionCliente.FindAsync(idAsignacion);
            if (objAsignacionCliente == null)
            {
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                respuesta.ObjetoRespuesta = objAsignacionCliente;
                return respuesta;
            }
            _context.AsignacionCliente.Remove(objAsignacionCliente);
            await _context.SaveChangesAsync();
            respuesta.MensajeRespuesta = Mensajes.RegistroEliminado;
            respuesta.ObjetoRespuesta = objAsignacionCliente;
            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }
        public async Task<Resultado> ExisteAsignacion(int idCliente)
        {
            Resultado respuesta = new Resultado();
            respuesta.EjecucionCorrecta = await _context.AsignacionCliente.AnyAsync(x => x.IdCliente == idCliente);
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite Consultar una asignacion
        /// </summary>
        /// <param name="idAsignacion">Id asignacion</param>
        /// <returns>Resultado de la consulta</returns>
        public async Task<Resultado> ConsultaAsignacionClientePatio(int idAsignacion)
        {
            Resultado respuesta = new Resultado();
            respuesta.ObjetoRespuesta = await _context.AsignacionCliente.FirstOrDefaultAsync(x => x.IdAsignacionCliente == idAsignacion);
            if (respuesta.ObjetoRespuesta == null)
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
            return respuesta;
        }
    }
}
