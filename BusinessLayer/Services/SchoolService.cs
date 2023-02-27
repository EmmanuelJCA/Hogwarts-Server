using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly HogwartsContext _db;

        public SchoolService(HogwartsContext db)
        {
            _db = db;
        }

        #region AdmissionRequest

        public async Task<List<AdmissionRequest>?> GetAdmissionRequestsAsync()
        {
            try
            {
                // Retornar solo las solicitudes abiertas
                return await _db.AdmissionRequests
                .Where(x => x.EndingDate == null)
                .Include(admissionRequest => admissionRequest.Aspirant)
                .Include(admissionRequest => admissionRequest.House)
                .ToListAsync();
            }
            catch (Exception)
            {
                return null; // Error
            }

        }

        public async Task<AdmissionRequest?> GetAdmissionRequestAsync(int id)
        {
            try
            {
                // Retornar solo las solicitudes abiertas
                return await _db.AdmissionRequests
                .Where(x => x.EndingDate == null)
                .Include(admissionRequest => admissionRequest.Aspirant)
                .Include(admissionRequest => admissionRequest.House)
                .FirstOrDefaultAsync(i=> i.AdmissionRequestId == id);
            }
            catch (Exception)
            {
                return null; // Error
            }
        }

        public async Task<AdmissionRequest?> AddAdmissionRequestAsync(AdmissionRequest admissionRequest)
        {
            try
            {
                // Verifica si ya el aspirante esta registrado, si es asi solo toma su id y envia la nueva solicitud con el aspirante ya existente,
                // de lo contrario se registra al nuevo aspirante
                Aspirant? dbAspirant = await _db.Aspirants.FirstOrDefaultAsync(x => x.Dni == admissionRequest.Aspirant.Dni);
                if (dbAspirant != null)
                {
                    admissionRequest.AspirantId = dbAspirant.AspirantId;
                    admissionRequest.Aspirant = null;
                } 
                else
                {
                    Aspirant aspirant = admissionRequest.Aspirant;
                    await _db.Aspirants.AddAsync(aspirant);
                    await _db.SaveChangesAsync();
                    admissionRequest.AspirantId = aspirant.AspirantId;
                    admissionRequest.Aspirant = null;
                }
                await _db.AdmissionRequests.AddAsync(admissionRequest);
                await _db.SaveChangesAsync();

                // Retorna la solicitud ya procesada desde la bdd
                return await _db.AdmissionRequests
                    .Where(x => x.EndingDate == null)
                    .Include(admissionRequest => admissionRequest.Aspirant)
                    .Include(admissionRequest => admissionRequest.House)
                    .FirstOrDefaultAsync(x => x.AdmissionRequestId == admissionRequest.AdmissionRequestId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null; // Error
            }            
        }

        public async Task<AdmissionRequest?> UpdateAdmissionRequestAsync(AdmissionRequest admissionRequest)
        {
            try
            {
                // Actualiza una solicitud y a su aspirante para retornar la solicitud ya actualizada
                var aspirant = admissionRequest.Aspirant;
                aspirant.AspirantId = (int)admissionRequest.AspirantId;
                _db.Aspirants.Update(admissionRequest.Aspirant);
                await _db.SaveChangesAsync();
                admissionRequest.Aspirant = null;

                _db.AdmissionRequests.Update(admissionRequest);
                await _db.SaveChangesAsync();

                return await _db.AdmissionRequests
                     .Where(x => x.EndingDate == null)
                     .Include(admissionRequest => admissionRequest.Aspirant)
                     .Include(admissionRequest => admissionRequest.House)
                     .FirstOrDefaultAsync(x => x.AdmissionRequestId == admissionRequest.AdmissionRequestId);
            }
            catch
            {
                return null; // Error
            }
        }

        public async Task<(bool status, string message)> DeleteAdmissionRquestAsync(int id)
        {
            try
            {
                // Busca la solicitud, si no existe retorna un estado en flaso 
                var dbAdmissionRequest = await _db.AdmissionRequests.FindAsync(id);
                if (dbAdmissionRequest == null)
                {
                    return (false, "Admission request could not be found");
                }

                // Cierra la fecha final dando asi por concluida la solicitud. Es importante tener un historico de solicitudes
                dbAdmissionRequest.EndingDate = DateTime.Now;
                _db.AdmissionRequests.Update(dbAdmissionRequest);
                await _db.SaveChangesAsync();

                return (true, "Admission request got deleted");
            } 
            catch(Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion AdmissionRequest

        #region Aspirant
        public async Task<List<Aspirant>?> GetAspirantsAsync()
        {
            try
            {
                return await _db.Aspirants.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<Aspirant?> GetAspirantAsync(int id)
        {
            try
            {
                return await _db.Aspirants.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Aspirant?> AddAspirantAsync(Aspirant aspirant)
        {
            try
            {
                await _db.Aspirants.AddAsync(aspirant);
                await _db.SaveChangesAsync();

                return await _db.Aspirants.FindAsync(aspirant.AspirantId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Aspirant?> UpdateAspirantAsync(Aspirant aspirant)
        {
            try
            {
                _db.Aspirants.Update(aspirant);
                await _db.SaveChangesAsync();

                return await _db.Aspirants.FindAsync(aspirant.AspirantId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<(bool status, string message)> DeleteAspirantAsync(int id)
        {
            try
            {
            var dbAspirant = await _db.Aspirants.FindAsync(id); 
            if (dbAspirant == null) 
            {
                return (false, "Aspirant could not be found");
            }

            _db.Aspirants.Remove(dbAspirant);
            await _db.SaveChangesAsync();

            return (true, "Aspirant got deleted");
            }
            catch(Exception ex)
            {
                return (false, $"An error ocurred. Error: {ex.Message}");
            }
        }


        #endregion Aspirant

        #region House

        public async Task<List<House>> GetHousesAsync()
        {
            try
            {
                return await _db.Houses.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<House> GetHouseAsync(int id)
        {
            try
            {
                return await _db.Houses.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion House
    }
}