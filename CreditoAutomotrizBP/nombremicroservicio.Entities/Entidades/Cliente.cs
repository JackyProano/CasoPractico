using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditoAutomotriz.Entities.Entidades
{
    public partial class Cliente
    {
        /// <summary>
        /// Identificador unico del cliente
        /// </summary>
        [Key]
        [Column("cl_id_cliente")]
        public int IdCliente { get; set; }

        /// <summary>
        /// Numero de identificacion del cliente
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [Column("cl_identificacion")]
        public string Identificacion { get; set; }

        /// <summary>
        /// Nombres del cliente
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("cl_nombres")]
        public string Nombres { get; set; }

        /// <summary>
        /// Edad del cliente en años
        /// </summary>
        [Required]
        [Column("cl_edad")]
        public int Edad { get; set; }

        /// <summary>
        /// Fecha de nacimiento del cliente
        /// </summary>
        [Required]
        [Column("cl_fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Apellidos del cliente
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("cl_apellidos")]
        public string Apellidos { get; set; }

        /// <summary>
        /// Direccion del cliente
        /// </summary>
        [Required]
        [StringLength(150)]
        [Column("cl_direccion")]
        public string Direccion { get; set; }

        /// <summary>
        /// Telefono del cliente
        /// </summary>
        [Required]
        [StringLength(10)]
        [Column("cl_telefono")]
        public string Telefono { get; set; }

        /// <summary>
        /// Estado Civil del cliente S => Soltero C => Casado D => Divorciado V => Viudo U => Union Libre
        /// </summary>
        [Required]
        [StringLength(1)]
        [Column("cl_estado_civil")]
        public string EstadoCivil { get; set; }

        /// <summary>
        /// Numero de identificacion dle conyuge
        /// </summary>
        [StringLength(10)]
        [Column("cl_identificacion_conyuge")]
        public string IdentificacionConyuge { get; set; }

        /// <summary>
        /// Nombres del conyuge
        /// </summary>
        [StringLength(150)]
        [Column("cl_nombre_conyuge")]
        public string NombresConyuge { get; set; }

        /// <summary>
        /// Indica si es sujeto de credito 1 => Si 0 => No
        /// </summary>
        [Required] 
        [Column("cl_es_sujeto_credito")]
        public bool EsSujetoCredito { get; set; }

        //public virtual ICollection<AsignacionCliente> AsignacionClientes { get; set; }
        //public virtual ICollection<SolicitudCredito> Solicitudes { get; set; }
    }
}
