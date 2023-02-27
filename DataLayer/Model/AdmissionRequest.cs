using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataLayer
{
    /// <summary>
    /// Tabla de referencia cruzada que asigna un aspirante y una casa para su solicitud de ingreso.
    /// </summary>
    public partial class AdmissionRequest
    {
        /// <summary>
        /// Primary key de la Solicitud de ingreso registrada.
        /// </summary>
        public int AdmissionRequestId { get; set; }
        /// <summary>
        /// Identificador del Aspirante. Foreign Key to Aspirant.AspirantID.
        /// </summary>
        public int? AspirantId { get; set; }
        /// <summary>
        /// Identificador de la casa a la que se aspira. Foreign Key to House.HouseID.
        /// </summary>
        public int? HouseId { get; set; }
        /// <summary>
        /// Fecha en la que fue hecha la solicitud.
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Fecha en la que finaliza la solicitud.
        /// </summary>
        public DateTime? EndingDate { get; set; }

        public virtual Aspirant? Aspirant { get; set; }
        public virtual House? House { get; set; }
    }
}
