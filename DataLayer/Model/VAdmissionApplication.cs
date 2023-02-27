using System;
using System.Collections.Generic;

namespace DataLayer
{
    /// <summary>
    /// Datos completos de la solicitud de ingreso.
    /// </summary>
    public partial class VAdmissionApplication
    {
        public int AdmissionApplicationId { get; set; }
        public DateTime AdmissionApplicationStartDate { get; set; }
        public int HouseId { get; set; }
        public string HouseName { get; set; } = null!;
        public int AspirantId { get; set; }
        public string AspirantFirstName { get; set; } = null!;
        public string AspirantLastName { get; set; } = null!;
        public DateTime AspirantBirthDate { get; set; }
        public int? AspirantAge { get; set; }
        public string Gender { get; set; } = null!;
        public int AspirantDni { get; set; }
    }
}
