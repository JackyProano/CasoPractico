using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/Clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _servicio;
        public ClienteController(IClienteRepository iServicio)
        {
            _servicio = iServicio;
        }


        [HttpGet("{IdentificadorCliente}")]
        public async Task<Resultado> ConsultarCliente(int IdentificadorCliente)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.ConsultaCliente(IdentificadorCliente);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;

        }

        [HttpPut]
        [Route("Cliente")]
        public async Task<Resultado> EditarCliente(Cliente patio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EditaCliente(patio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpPost]
        [Route("Cliente")]
        public async Task<Resultado> CrearCliente(Cliente patio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.CreaCliente(patio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpDelete("{IdentificadorCliente}")]
        public async Task<Resultado> EliminarCliente(int IdentificadorCliente)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EliminaCliente(IdentificadorCliente);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }
    }
}