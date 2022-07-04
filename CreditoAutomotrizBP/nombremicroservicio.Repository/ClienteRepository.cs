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
    public partial class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly BDDCreditoAutomotrizContext _context;
        public ClienteRepository(BDDCreditoAutomotrizContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Metodo que permite la creacion del cliente
        /// </summary>
        /// <param name="objCliente">Registro a ser guardado</param>
        /// <returns>Resultado del guardado</returns>
        public async Task<Resultado> CreaCliente(Cliente objCliente)
        {
            Resultado respuesta;
            respuesta = await ValidaExistenciaCliente(objCliente.Identificacion);
            if (respuesta.RegistroExistente)
            {
                respuesta.RegistroExistente = true;
                respuesta.MensajeRespuesta = Mensajes.ClienteExiste;
                return respuesta;
            }
            _context.Cliente.Add(objCliente);
            await _context.SaveChangesAsync();
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.RegistroCreado;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite editar un cliente
        /// </summary>
        /// <param name="objCliente"></param>
        /// <returns>Resultado de la edicion</returns>
        public async Task<Resultado> EditaCliente(Cliente objCliente)
        {
            Resultado respuesta;
            respuesta = await ValidaExistenciaCliente(objCliente.Identificacion);
            if (!respuesta.RegistroExistente)
            {
                respuesta.RegistroExistente = true;
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                return respuesta;
            }
            _context.Entry(objCliente).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(true);
            respuesta.EjecucionCorrecta = true;
            respuesta.MensajeRespuesta = Mensajes.RegistroModificado;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite eliminar un cliente
        /// </summary>
        /// <param name="numeroPuntoVenta">Numero del cliente a eliminar</param>
        /// <returns></returns>
        public async Task<Resultado> EliminaCliente(int IdentificadorCliente)
        {
            Resultado respuesta = new Resultado();
            var objCliente = await _context.Cliente.FindAsync(IdentificadorCliente);
            if (objCliente == null)
            {
                respuesta.MensajeRespuesta = Mensajes.RegistroNoExiste;
                respuesta.ObjetoRespuesta = objCliente;
                return respuesta;
            }
            //Verificar que no este asigando a un cliente
            IEnumerable<AsignacionCliente> objAsignacionClientes = null;
            objAsignacionClientes = await (from m in _context.AsignacionCliente
                                           where m.IdCliente == IdentificadorCliente
                                           select m).ToListAsync();
            if (objAsignacionClientes.Count() > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ErrorEliminar;
                respuesta.ObjetoRespuesta = objCliente;
                return respuesta;
            }
            //Verificar que no este asigando a una solicitud
            IEnumerable<SolicitudCredito> objSolicitudesCredito = null;
            objSolicitudesCredito = await (from m in _context.SolicitudCredito
                                           where m.IdCliente == IdentificadorCliente
                                           select m).ToListAsync();
            if (objSolicitudesCredito.Count() > 0)
            {
                respuesta.MensajeRespuesta = Mensajes.ErrorEliminar;
                respuesta.ObjetoRespuesta = objCliente;
                return respuesta;
            }
            _context.Cliente.Remove(objCliente);
            await _context.SaveChangesAsync();
            respuesta.MensajeRespuesta = Mensajes.RegistroEliminado;
            respuesta.ObjetoRespuesta = objCliente;
            respuesta.EjecucionCorrecta = true;
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite consultar un cliente por numero de identificacion
        /// </summary>
        /// <param name="identificacion">Numero de identificacion a consultar</param>
        /// <returns>resultado de la consulta</returns>
        public async Task<Resultado> ValidaExistenciaCliente(string identificacion)
        {
            Resultado respuesta = new Resultado();
            respuesta.RegistroExistente = await _context.Cliente.AnyAsync(x => x.Identificacion == identificacion);
            return respuesta;
        }

        /// <summary>
        /// Metodo que permite consultar un cliente
        /// </summary>
        /// <param name="IdentificadorCliente">Id de cliente a consultar</param>
        /// <returns>resultado de la consulta</returns>
        public async Task<Resultado> ConsultaCliente(int IdentificadorCliente)
        {
            Resultado respuesta = new Resultado();
            respuesta.ObjetoRespuesta = await _context.Cliente.FindAsync(IdentificadorCliente);
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
