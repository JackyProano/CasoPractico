using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<Resultado> CreaVehiculo(Vehiculo objVehiculo);
        Task<Resultado> EditaVehiculo(Vehiculo objVehiculo);
        Task<Resultado> EliminaVehiculo(int IdentificadorVehiculo);
        Task<Resultado> ValidaExistenciaVehiculo(string placa);
        Task<Resultado> ConsultaVehiculo(int parametroBusquedaVehiculo);

    }
}
