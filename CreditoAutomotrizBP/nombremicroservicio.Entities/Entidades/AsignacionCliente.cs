using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditoAutomotriz.Entities.Entidades
{
    [Table("asignacion_cliente")]
    public partial class AsignacionCliente
    {
        /// <summary>
        /// Identificador unico de asignacion
        /// </summary>
        [Key]
        [Column("as_id_asignacion")]
        public int IdAsignacionCliente { get; set; }

        /// <summary>
        /// Numero de identificador del cliente
        /// </summary>
        [Required]
        [Column("as_id_cliente")]
        public int IdCliente { get; set; }

        /// <summary>
        /// Numero de identificador del patio
        /// </summary>
        [Required]
        [Column("as_id_patio")]
        public int IdPatio { get; set; }

        /// <summary>
        /// Fecha de Asignacion del cliente
        /// </summary>
        [Required]
        [Column("as_fecha_asignacion")]
        public DateTime FechaAsignacion { get; set; }

        //public virtual Cliente ClienteAsignacion { get; set; }
        //public virtual Patio PatioAsignacion { get; set; }
    }
}
