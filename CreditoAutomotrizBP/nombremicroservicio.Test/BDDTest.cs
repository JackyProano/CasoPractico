using CreditoAutomotriz.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Test
{
    public class BDDTest
    {
        public static BDDCreditoAutomotrizContext ConstruirContext(string baseDatos)
        {
            var dbBase = new DbContextOptionsBuilder<BDDCreditoAutomotrizContext>().UseInMemoryDatabase(baseDatos).Options;
            var dbContext = new BDDCreditoAutomotrizContext(dbBase);
            return dbContext;
        }
    }
}
