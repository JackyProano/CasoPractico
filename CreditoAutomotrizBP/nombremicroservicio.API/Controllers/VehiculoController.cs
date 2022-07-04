using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    [Route("api/Vehiculos")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoRepository _servicio;
        public VehiculoController(IVehiculoRepository iServicio)
        {
            _servicio = iServicio;
        }

        [HttpGet("{ParametroBusquedaVehiculo}")]
        public async Task<Resultado> ConsultarVehiculo(int ParametroBusquedaVehiculo)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.ConsultaVehiculo(ParametroBusquedaVehiculo);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;

        }

        [HttpPut]
        [Route("Vehiculo")]
        public async Task<Resultado> EditarVehiculo(Vehiculo patio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EditaVehiculo(patio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpPost]
        [Route("Vehiculo")]
        public async Task<Resultado> CrearVehiculo(Vehiculo patio)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.CreaVehiculo(patio);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }

        [HttpDelete("{IdentificadorVehiculo}")]
        public async Task<Resultado> EliminarVehiculo(int IdentificadorVehiculo)
        {
            Resultado respuesta = new Resultado();
            try
            {
                respuesta = await _servicio.EliminaVehiculo(IdentificadorVehiculo);
            }
            catch (Exception ex)
            {
                respuesta.MensajeRespuesta = ex.ToString();
            }
            return respuesta;
        }
    }
}
