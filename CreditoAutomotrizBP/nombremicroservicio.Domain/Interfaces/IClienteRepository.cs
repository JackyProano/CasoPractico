using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Resultado> CreaCliente(Cliente objCliente);
        Task<Resultado> EditaCliente(Cliente objCliente);
        Task<Resultado> EliminaCliente(int IdentificadorCliente);
        Task<Resultado> ValidaExistenciaCliente(string identificacion);
        Task<Resultado> ConsultaCliente(int idCliente);

    }
}
