using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IAsignacionClienteRepository
    {
        Task<Resultado> CreaAsignacionClientePatio(AsignacionCliente objAsignacioncliente);
        Task<Resultado> EditaAsignacionClientePatio(AsignacionCliente objAsignacioncliente);
        Task<Resultado> EliminaAsignacionClientePatio(int idAsignacion);
        Task<Resultado> ConsultaAsignacionClientePatio(int idAsignacion);
    }
}
