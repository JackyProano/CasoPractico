using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditoAutomotriz.Entities.Entidades
{
    [Table("solicitud_credito")]
    public partial class SolicitudCredito
    {
        /// <summary>
        /// Identificador unico de solicitud
        /// </summary>
        [Key]
        [Column("so_id_solicitud")]
        public int IdSolicitudCredito { get; set; }

        /// <summary>
        /// Numero de identificador del cliente
        /// </summary>
        [Required]
        [Column("so_fecha_elaboracion")]
        public DateTime FechaElaboracion { get; set; }

        /// <summary>
        /// Numero de identificador del cliente
        /// </summary>
        [Required]
        [Column("so_id_cliente")]
        public int IdCliente { get; set; }

        /// <summary>
        /// Numero de identificador del patio
        /// </summary>
        [Required]
        [Column("so_id_patio")]
        public int IdPatio { get; set; }

        /// <summary>
        /// Numero de identificador del vehiculo
        /// </summary>
        [Required]
        [Column("so_id_vehiculo")]
        public int IdVehiculo { get; set; }

        /// <summary>
        /// Meses plazo
        /// </summary>
        [Required]
        [Column("so_meses_plazo")]
        public int MesesPlazo { get; set; }

        /// <summary>
        /// Cuotas
        /// </summary>
        [Required]
        [Column("so_cuotas")]
        public int Cuotas { get; set; }

        /// <summary>
        /// Entrada
        /// </summary>
        [Required]
        [Column("so_entrada" , TypeName = "money")]
        public decimal Entrada { get; set; }

        /// <summary>
        /// Numero de identificador del ejecutivo
        /// </summary>
        [Required]
        [Column("so_id_ejecutivo")]
        public int IdEjecutivo { get; set; }

        /// <summary>
        /// Numero de identificador del ejecutivo
        /// </summary>
        [Column("so_observacion")]
        [StringLength(100)]
        public string Observacion { get; set; }

        /// <summary>
        /// Numero de identificador del ejecutivo
        /// </summary>
        [Required]
        [Column("so_estado")]
        [StringLength(10)]
        public string Estado { get; set; }


        //public virtual Cliente ClienteSolicitud { get; set; }
        //public virtual Patio PatioSolicitud { get; set; }
        //public virtual Vehiculo VehiculoSolicitud { get; set; }
        //public virtual Ejecutivo EjecutivoSolicitud { get; set; }
    }
}
