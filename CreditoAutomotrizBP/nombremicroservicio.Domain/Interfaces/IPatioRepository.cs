using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Entities.Utilitarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<Resultado> CreaPatio(Patio objPatio);
        Task<Resultado> ValidaExistenciaPatio(int numeroPuntoVenta);
        Task<Resultado> EditaPatio(Patio objPatio);
        Task<Resultado> EliminaPatio(int numeroPuntoVenta);
        Task<Resultado> ConsultaPatio(int IdentificadorPatio);
    }
}
