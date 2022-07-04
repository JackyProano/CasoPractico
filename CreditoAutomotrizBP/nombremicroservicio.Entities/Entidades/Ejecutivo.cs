using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CreditoAutomotriz.Entities.Entidades
{
    public partial class Ejecutivo
    {
        //public Ejecutivo()
        //{
        //    SolicitudesEjecutivo = new HashSet<SolicitudCredito>();
        //}

        /// <summary>
        /// Identificador unico del ejecutivo
        /// </summary>
        [Key]
        [Column("ej_id_ejecutivo")]
        public int IdEjecutivo { get; set; }

        /// <summary>
        /// Numero de identificador del ejecutivo
        /// </summary>
        [Required]
        [StringLength(10)]
        [Column("ej_identificacion")]
        public string Identificacion { get; set; }

        /// <summary>
        /// Nombres del ejecutivo
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("ej_nombres")]
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del ejecutivo
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("ej_apellidos")]
        public string Apellidos { get; set; }

        /// <summary>
        /// Direccion del ejecutivo
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("ej_direccion")]
        public string Direccion { get; set; }

        /// <summary>
        /// Telefono convencional del ejecutivo
        /// </summary>
        [Required]
        [StringLength(10)]
        [Column("ej_telefono_convencional")]
        public string TelefonoConvencional { get; set; }

        /// <summary>
        /// Celular del ejecutivo
        /// </summary>
        [Required]
        [StringLength(10)]
        [Column("ej_celular")]
        public string Celular { get; set; }

        /// <summary>
        /// Identificador del patio en el cual labora el ejecutivo
        /// </summary>
        [Required]
        [Column("ej_id_patio")]
        public int IdPatioLabora { get; set; }

        /// <summary>
        /// Edad del ejecutivo
        /// </summary>
        [Required]
        [Column("ej_edad")]
        public int Edad { get; set; }

        //public virtual Patio PatioEjecutivo { get; set; }
        //public virtual ICollection<SolicitudCredito> SolicitudesEjecutivo { get; set; }

    }
}
