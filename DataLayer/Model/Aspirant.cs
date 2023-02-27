using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataLayer
{
    /// <summary>
    /// Informacion personal del Aspirante.
    /// </summary>
    public partial class Aspirant
    {
        public Aspirant()
        {
            AdmissionRequests = new HashSet<AdmissionRequest>();
        }

        /// <summary>
        /// Primary key del Aspirante registrado.
        /// </summary>
        public int AspirantId { get; set; }
        /// <summary>
        /// Primer nombre del aspirante registrado.
        /// </summary>
        public string FirstName { get; set; } = null!;
        /// <summary>
        /// Apellido del aspirante registrado.
        /// </summary>
        public string LastName { get; set; } = null!;
        /// <summary>
        /// Genero: M = Masculino F = Femenino.
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Identificacion del aspirante registrado.
        /// </summary>
        public int Dni { get; set; }
        /// <summary>
        /// Fecha de nacimiento del aspirante registrado. Utilizada para obtener la edad del aspirante registrado.
        /// </summary>
        public int Age { get; set; }
        [JsonIgnore]
        public virtual ICollection<AdmissionRequest> AdmissionRequests { get; set; }
    }
}
