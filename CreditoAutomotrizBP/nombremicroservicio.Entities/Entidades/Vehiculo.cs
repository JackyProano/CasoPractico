using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditoAutomotriz.Entities.Entidades
{
    public partial class Vehiculo
    {
        /// <summary>
        /// Identificador unico del vehiculo
        /// </summary>
        [Key]
        [Column("ve_id_vehiculo")]
        public int IdVehiculo { get; set; }

        /// <summary>
        /// Numero de identificador del vehiculo
        /// </summary>
        [Required]
        [StringLength(10)]
        [Column("ve_placa")]
        public string Placa { get; set; }

        /// <summary>
        /// Modelo del vehiculo
        /// </summary>
        [Required]
        [Column("ve_modelo")]
        public int Modelo { get; set; }

        /// <summary>
        /// Identificador de Marca del vehiculo
        /// </summary>
        [Required]
        [Column("ve_id_marca")]
        public int IdMarca { get; set; }

        /// <summary>
        /// Tipo del vehiculo
        /// </summary>
        [StringLength(50)]
        [Column("ve_tipo")]
        public string Tipo { get; set; }

        /// <summary>
        /// Cilindraje del vehiculo
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("ve_cilindraje")]
        public string Cilindraje { get; set; }

        /// <summary>
        /// Avaluo del vehiculo
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("ve_avaluo")]
        public string Avaluo { get; set; }

        //public virtual Marca MarcaVehiculo { get; set; }
        //public virtual ICollection<SolicitudCredito> SolicitudesVehiculo { get; set; }
    }
}
