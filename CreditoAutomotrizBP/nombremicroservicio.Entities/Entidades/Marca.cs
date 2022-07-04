using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CreditoAutomotriz.Entities.Entidades
{
    public partial class Marca
    {
        //public Marca()
        //{
        //    VehiculosMarca = new HashSet<Vehiculo>();
        //}

        /// <summary>
        /// Identificador unico de la marca
        /// </summary>
        [Key]
        [Column("ma_id_marca")]
        public int IdMarca { get; set; }

        /// <summary>
        /// Descripcion de la marca
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("ma_descripcion_marca")]
        public string DescripcionMarca { get; set; }

        //public virtual ICollection<Vehiculo> VehiculosMarca { get; set; }
    }
}
