using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CreditoAutomotriz.Entities.Entidades
{
    public partial class Patio
    {
        //public Patio()
        //{
        //    AsignacionClientesPatio = new HashSet<AsignacionCliente>();
        //    EjecutivosPatio = new HashSet<Ejecutivo>();
        //    SolicitudesPatio = new HashSet<SolicitudCredito>();
        //}

        /// <summary>
        /// Identificador del patio
        /// </summary>
        [Key]
        [Column("pa_id_patio")]
        public int IdPatio { get; set; }

        /// <summary>
        /// Nombre del patio
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("pa_nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Direccion del patio
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("pa_direccion")]
        public string Direccion { get; set; }

        /// <summary>
        /// Telefono del patio
        /// </summary>
        [Required]
        [StringLength(10)]
        [Column("pa_telefono")]
        public string Telefono { get; set; }

        /// <summary>
        /// Numero Punto de Venta
        /// </summary>
        [Column("pa_numero_punto_venta")]
        public int NumeroPuntoVenta { get; set; }

        //public virtual ICollection<AsignacionCliente> AsignacionClientesPatio { get; set; }
        //public virtual ICollection<Ejecutivo> EjecutivosPatio { get; set; }
        //public virtual ICollection<SolicitudCredito> SolicitudesPatio { get; set; }

    }
}
