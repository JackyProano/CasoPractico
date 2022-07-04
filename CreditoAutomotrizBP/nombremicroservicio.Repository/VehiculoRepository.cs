using Microsoft.Extensions.Configuration;
using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Infrastructure.Context;
using System.Threading.Tasks;
using CreditoAutomotriz.Entities.Utilitarios;
using CreditoAutomotriz.Entities.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CreditoAutomotriz.Repository
{
    public partial class VehiculoRepository : IVehiculoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly BDDCreditoAutomotrizContext _context;
        public VehiculoRepository(BDDCreditoAutomotrizContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Metodo que permite la creacion del vehiculo
        /// </summary>
        /// <param name="objVehiculo">Registro a ser guardado</param>
        /// <returns>Resultado del guardado</returns>
        public async Task<Resultado> CreaVehiculo(Vehiculo objVehiculo)
        {
            Resultado respuesta;
            respuesta = await ValidaExistenciaVehiculo(objVehiculo.Placa);
            if (respuesta.RegistroExistente)
            {
                respuesta.RegistroExistente = true;
                respuesta.MensajeRespuesta = Mensajes.VehiculoExiste;
                return respuesta;
            }
            _context.Vehiculo.Add(objVehiculo);
            await _context.SaveChangesAsync();
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.RegistroCreado;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite editar un vehiculo
        /// </summary>
        /// <param name="objVehiculo"></param>
        /// <returns>Resultado de la edicion</returns>
        public async Task<Resultado> EditaVehiculo(Vehiculo objVehiculo)
        {
            Resultado respuesta;
            respuesta = await ValidaExistenciaVehiculo(objVehiculo.Placa);
            if (!respuesta.RegistroExistente)
            {
                respuesta.RegistroExistente = true;
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                return respuesta;
            }
            _context.Entry(objVehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(true);
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.RegistroModificado;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite eliminar un vehiculo
        /// </summary>
        /// <param name="IdentificadorVehiculo">Id del vehiculo a eliminar</param>
        /// <returns></returns>
        public async Task<Resultado> EliminaVehiculo(int IdentificadorVehiculo)
        {
            Resultado respuesta = new Resultado();
            var objVehiculo = await _context.Vehiculo.FindAsync(IdentificadorVehiculo);
            if (objVehiculo == null)
            {
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                respuesta.ObjetoRespuesta = objVehiculo;
                return respuesta;
            }
            //Verificar que no este asigando a una solicitud
            IEnumerable<SolicitudCredito> objSolicitudesCredito = null;
            objSolicitudesCredito = await (from m in _context.SolicitudCredito
                                           where m.IdVehiculo == IdentificadorVehiculo
                                           select m).ToListAsync();
            if (objSolicitudesCredito.Count() > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ErrorEliminar;
                respuesta.ObjetoRespuesta = objVehiculo;
                return respuesta;
            }
            _context.Vehiculo.Remove(objVehiculo);
            await _context.SaveChangesAsync();
            respuesta.MensajeRespuesta = Mensajes.RegistroEliminado;
            respuesta.ObjetoRespuesta = objVehiculo;
            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite consultar un vehiculo por numero de identificacion
        /// </summary>
        /// <param name="placa">Numero de placa a consultar</param>
        /// <returns>resultado de la consulta</returns>
        public async Task<Resultado> ValidaExistenciaVehiculo(string placa)
        {
            Resultado respuesta = new Resultado();
            respuesta.RegistroExistente = await _context.Vehiculo.AnyAsync(x => x.Placa == placa);
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite consultar un vehiculo
        /// </summary>
        /// <param name="parametroBusquedaVehiculo">parametro Busqueda del vehiculo</param>
        /// <returns>resultado de la consulta</returns>
        public async Task<Resultado> ConsultaVehiculo(int parametroBusquedaVehiculo)
        {
            Resultado respuesta = new Resultado();
            Vehiculo objVehiculo = await _context.Vehiculo.Where(s => s.Modelo == parametroBusquedaVehiculo || 
                                                                s.IdMarca == parametroBusquedaVehiculo).FirstOrDefaultAsync();
            if (objVehiculo == null)
            {
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                return respuesta;
            }
            respuesta.RegistroExistente = true;
            respuesta.EjecucionCorrecta = true;
            respuesta.ObjetoRespuesta = objVehiculo;
            return respuesta;
        }

    }
}
