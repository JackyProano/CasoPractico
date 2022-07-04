using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    [Route("api/AsignacionClientes")]
    [ApiController]
    public class AsignacionClientesController : ControllerBase
    {
        private readonly IAsignacionClienteRepository _servicio;
        public AsignacionClientesController(IAsignacionClienteRepository iServicio)
        {
            _servicio = iServicio;
        }

        [HttpGet("{IdentificadorAsignacionClientes}")]
        public async Task<Resultado> ConsultarAsignacionClientes(int IdentificadorAsignacionClientes)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.ConsultaAsignacionClientePatio(IdentificadorAsignacionClientes);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;

        }

        [HttpPut]
        public async Task<Resultado> EditarAsignacionClientes(AsignacionCliente asignacionCliente)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EditaAsignacionClientePatio(asignacionCliente);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpPost]
        public async Task<Resultado> CrearAsignacionClientes(AsignacionCliente asignacionCliente)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.CreaAsignacionClientePatio(asignacionCliente);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpDelete("{IdentificadorAsignacionClientes}")]
        public async Task<Resultado> EliminarAsignacionClientes(int IdentificadorAsignacionClientes)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EliminaAsignacionClientePatio(IdentificadorAsignacionClientes);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }
    }
}
