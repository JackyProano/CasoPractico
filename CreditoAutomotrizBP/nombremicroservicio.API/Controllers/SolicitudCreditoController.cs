using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    [Route("api/SolicitudCredito")]
    [ApiController]
    public class SolicitudCreditoController : ControllerBase
    {
        private readonly ISolicitudCreditoRepository _servicio;
        public SolicitudCreditoController(ISolicitudCreditoRepository iServicio)
        {
            _servicio = iServicio;
        }

        [HttpGet("{idSolicitud}")]
        public async Task<Resultado> ConsultarSolicitudCredito(int idSolicitud)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.ConsultaSolicitud(idSolicitud);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpPost]
        public async Task<Resultado> CrearSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.CreaSolicitudCredito(solicitudCredito);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }
    }
}
