using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataLayer
{
    /// <summary>
    /// Tabla de busqueda que contiene las distintas casas de Hogwarts.
    /// </summary>
    public partial class House
    {
        public House()
        {
            AdmissionRequests = new HashSet<AdmissionRequest>();
        }

        /// <summary>
        /// Primary key de la casa registrada.
        /// </summary>
        public int HouseId { get; set; }
        /// <summary>
        /// Nombre de la casa registrada.
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Nombre completo del fundador de la casa registrada.
        /// </summary>
        public string Founder { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<AdmissionRequest> AdmissionRequests { get; set; }
    }
}
