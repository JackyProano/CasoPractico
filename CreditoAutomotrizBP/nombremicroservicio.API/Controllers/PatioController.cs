using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    [Route("api/Patios")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatioRepository _servicio;
        public PatioController(IPatioRepository iServicio)
        {
            _servicio = iServicio;
        }

        [HttpGet("{IdentificadorPatio}")]
        public async Task<Resultado> ConsultarPatio(int IdentificadorPatio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.ConsultaPatio(IdentificadorPatio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;

        }

        [HttpPut]
        [Route("Patio")]
        public async Task<Resultado> EditarPatio(Patio patio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EditaPatio(patio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpPost]
        [Route("Patio")]
        public async Task<Resultado> CrearPatio(Patio patio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.CreaPatio(patio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpDelete("{IdentificadorPatio}")]
        public async Task<Resultado> EliminarPatio(int IdentificadorPatio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EliminaPatio(IdentificadorPatio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }
    }
}
