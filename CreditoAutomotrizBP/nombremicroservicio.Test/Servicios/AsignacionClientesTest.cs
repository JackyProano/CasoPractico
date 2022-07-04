using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.Entidades;
using CreditoAutomotriz.Repository;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Test.Servicios
{
    //[TestClass]
    public class AsignacionClientesTest
    {
        //[TestMethod]
        public async Task AsignarCliente()
        {
            string nombreBD = Guid.NewGuid().ToString();
            var contexto = BDDTest.ConstruirContext(nombreBD);
            AsignacionCliente objAsignacion = new AsignacionCliente()
            {
                IdCliente = 20,
                IdPatio = 5,
                FechaAsignacion = DateTime.Now
            };
            contexto.Add(objAsignacion);
            await contexto.SaveChangesAsync();

            //Validación prueba
            IAsignacionClienteRepository _servicio = new AsignacionClienteRepository(contexto,null);
            var respuesta = await _servicio.CreaAsignacionClientePatio(objAsignacion);
            Assert.IsTrue(respuesta.EjecucionCorrecta, "true");
        }

    }
}
