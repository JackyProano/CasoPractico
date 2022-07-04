using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Entities.Utilitarios
{
    public class Resultado
    {
        public bool EjecucionCorrecta { get; set; }
        public string MensajeRespuesta { get; set; }
        public object ObjetoRespuesta { get; set; }
        public bool RegistroExistente { get; set; }
    }
}
