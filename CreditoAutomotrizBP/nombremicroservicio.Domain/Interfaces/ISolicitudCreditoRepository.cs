using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface ISolicitudCreditoRepository
    {
        Task<Resultado> CreaSolicitudCredito(SolicitudCredito objSolicitudCredito);
        Task<Resultado> ExisteSolicitudFecha(int idCliente, int idPatio);
        Task<string> ValidarEstado(int IdCliente, int idPatio);
        Task<bool> ValidarVehiculo(int idVehiculo);
        Task<Resultado> ConsultaSolicitud(int idSolicitud);
    }
}
