using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using CreditoAutomotriz.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Repository
{
    public class SolicitudCreditoRepository : ISolicitudCreditoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly BDDCreditoAutomotrizContext _context;
        public SolicitudCreditoRepository(BDDCreditoAutomotrizContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Metodo que permite Crear la solicitud
        /// </summary>
        /// <param name="objSolicitudCredito">Datos de la solicitud</param>
        /// <returns>Resultado</returns>
        public async Task<Resultado> CreaSolicitudCredito(SolicitudCredito objSolicitudCredito)
        {
            Resultado respuesta = new Resultado();
            respuesta = await ExisteSolicitudFecha(objSolicitudCredito.IdCliente, objSolicitudCredito.IdPatio);
            string Estado = await ValidarEstado(objSolicitudCredito.IdCliente, objSolicitudCredito.IdPatio);
            if (respuesta.EjecucionCorrecta && Estado.Equals(Mensajes.Activo))
            {
                respuesta.MensajeRespuesta = Mensajes.ExisteSolicirud + DateTime.Now.Date.ToShortDateString() + " en estado " + Estado;
                respuesta.EjecucionCorrecta = true;
                return respuesta;
            }
            bool VehiculoActivo = await ValidarVehiculo(objSolicitudCredito.IdVehiculo);
            if (VehiculoActivo)
            {
                respuesta.MensajeRespuesta = Mensajes.VehiculoReservado;
                respuesta.EjecucionCorrecta = true;
                return respuesta;
            }
            // Asignar Solicitud credito
            _context.SolicitudCredito.Add(objSolicitudCredito);
            // Asignar Cliente Patio
            AsignacionCliente ObjClientePatio = new AsignacionCliente()
            {
                IdCliente = objSolicitudCredito.IdCliente,
                IdPatio = objSolicitudCredito.IdPatio,
                FechaAsignacion = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"))
            };
            _context.AsignacionCliente.Add(ObjClientePatio);
            await _context.SaveChangesAsync();
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.SolicitudCreadoOk;
            return respuesta;
        }

        /// <summary>
        /// Meotod que permite validar si esxiste solicitud de acuerdo a condiciones preestablecidas
        /// </summary>
        /// <param name="idCliente">Id del cliente</param>
        /// <param name="idPatio">Id del patio</param>
        /// <returns></returns>
        public async Task<Resultado> ExisteSolicitudFecha(int idCliente, int idPatio)
        {
            Resultado respuesta = new Resultado();
            if (await _context.SolicitudCredito.AnyAsync(x => x.IdCliente == idCliente))
            {
                AsignacionCliente objAsignacionCliente = new AsignacionCliente();
                objAsignacionCliente = await _context.AsignacionCliente.Where(x => x.IdCliente == idCliente).FirstOrDefaultAsync();
                if (objAsignacionCliente != null)
                {
                    if (DateTime.Compare(Convert.ToDateTime(objAsignacionCliente.FechaAsignacion), DateTime.Now.Date) == 0)
                        respuesta.EjecucionCorrecta = true;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite validar el estado de solicitudes ingresadas
        /// </summary>
        /// <param name="IdCliente">Id cliente</param>
        /// <param name="idPatio">Id de patio</param>
        /// <returns>Resultado</returns>
        public async Task<string> ValidarEstado(int IdCliente, int idPatio)
        {
            SolicitudCredito oValidarSolicitud = new SolicitudCredito();
            oValidarSolicitud = await _context.SolicitudCredito.FirstOrDefaultAsync(x => x.IdCliente == IdCliente
            && x.IdPatio == idPatio);
            if (oValidarSolicitud != null && !string.IsNullOrEmpty(oValidarSolicitud.Estado))
                return oValidarSolicitud.Estado;

            return string.Empty;
        }

        /// <summary>
        /// Metodo que permite validar vehiculo disponible
        /// </summary>
        /// <param name="idVehiculo">Id de vehiculo</param>
        /// <returns>Resultado</returns>
        public async Task<bool> ValidarVehiculo(int idVehiculo)
        {
            SolicitudCredito oValidarVehiculo = await _context.SolicitudCredito.FirstOrDefaultAsync(x => x.IdVehiculo == idVehiculo
            && x.Estado.Equals(Mensajes.Activo));
            if (oValidarVehiculo != null)
                return true;
            return false;
        }

        /// <summary>
        /// Metodo que permite Consultar una solicitud
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud</param>
        /// <returns>Resultado de la consulta</returns>
        public async Task<Resultado> ConsultaSolicitud(int idSolicitud)
        {
            Resultado respuesta = new Resultado();
            respuesta.ObjetoRespuesta = await _context.SolicitudCredito.FirstOrDefaultAsync(x => x.IdSolicitudCredito == idSolicitud);
            if (respuesta.ObjetoRespuesta == null)
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
            return respuesta;
        }
    }
}
