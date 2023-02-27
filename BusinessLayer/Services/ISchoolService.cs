using DataLayer;

namespace BusinessLayer.Services
{
    public interface ISchoolService
    {
        // AdmissionRequest Service
        Task<List<AdmissionRequest>?> GetAdmissionRequestsAsync();
        Task<AdmissionRequest?> GetAdmissionRequestAsync(int id);
        Task<AdmissionRequest?> AddAdmissionRequestAsync(AdmissionRequest admissionRequest);
        Task<AdmissionRequest?> UpdateAdmissionRequestAsync(AdmissionRequest admissionRquest);
        Task<(bool status, string message)> DeleteAdmissionRquestAsync(int id);

        // Aspirant Service
        Task<List<Aspirant>?> GetAspirantsAsync();
        Task<Aspirant?> GetAspirantAsync(int id);
        Task<Aspirant?> AddAspirantAsync(Aspirant aspirant);
        Task<Aspirant?> UpdateAspirantAsync(Aspirant aspirant);
        Task<(bool status, string message)> DeleteAspirantAsync(int id);

        // House Service
        Task<List<House>?> GetHousesAsync();
        Task<House?> GetHouseAsync(int id);
    }
}
