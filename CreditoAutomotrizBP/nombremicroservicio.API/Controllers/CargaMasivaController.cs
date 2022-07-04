using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    [Route("api/CargaMasiva")]
    [ApiController]
    public class CargaMasivaController : ControllerBase
    {
        private readonly ICargaMasivaRepository _servicio;
        public CargaMasivaController(ICargaMasivaRepository iServicio)
        {
            _servicio = iServicio;
        }
        /// <summary>
        /// Método para carga de datos iniciales de clientes
        /// </summary>
        /// <returns></returns>
        [Route("CargaInicialDatos")]
        public async Task<Resultado> CargaInicial()
        {
            Resultado respuesta = new Resultado();
            string strTipoCarga = "C";
            try
            {
                respuesta = await _servicio.CargaInicial(strTipoCarga);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        /// <summary>
        /// Método para carga de datos iniciales
        /// </summary>
        /// <returns></returns>
        [Route("CargaInicialDatos/{strTipoCarga}")]
        public async Task<Resultado> CargaInicial(string strTipoCarga)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.CargaInicial(strTipoCarga);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }
    }
}
