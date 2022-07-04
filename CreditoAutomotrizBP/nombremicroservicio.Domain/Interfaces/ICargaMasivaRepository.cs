using CreditoAutomotriz.Entities.Utilitarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface ICargaMasivaRepository
    {
        Task<Resultado> CargaInicial(string strTipoCarga);
    }
}
