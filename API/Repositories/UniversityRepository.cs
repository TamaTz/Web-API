using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : IUniversityRepository
{
    private readonly BookingManagementDbContext _context;
    public UniversityRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    /*
     * <summary>
     * Create a new university
     * </summary>
     * <param name="university">University object</param>
     * <returns>University object</returns>
     */
    public University Create(University university)
    {
        try {
            _context.Set<University>().Add(university);
            _context.SaveChanges();
            return university;
        }
        catch {
            return new University();
        }
    }

    /*
     * <summary>
     * Update a university
     * </summary>
     * <param name="university">University object</param>
     * <returns>true if data updated</returns>
     * <returns>false if data not updated</returns>
     */
    public bool Update(University university)
    {
        try {
            _context.Set<University>().Update(university);
            _context.SaveChanges();
            return true;
        }
        catch {
            return false;
        }
    }

    /*
     * <summary>
     * Delete a university
     * </summary>
     * <param name="guid">University guid</param>
     * <returns>true if data deleted</returns>
     * <returns>false if data not deleted</returns>
     */
    public bool Delete(Guid guid)
    {
        try {
            var university = GetByGuid(guid);
            if (university == null) {
                return false;
            }

            _context.Set<University>().Remove(university);
            _context.SaveChanges();
            return true;
        }
        catch {
            return false;
        }
    }

    /*
     * <summary>
     * Get all universities
     * </summary>
     * <returns>List of universities</returns>
     * <returns>Empty list if no data found</returns>
     */
    public IEnumerable<University> GetAll()
    {
        return _context.Set<University>().ToList();
    }

    /*
     * <summary>
     * Get a university by guid
     * </summary>
     * <param name="guid">University guid</param>
     * <returns>University object</returns>
     * <returns>null if no data found</returns>
     */
    public University? GetByGuid(Guid guid)
    {
        return _context.Set<University>().Find(guid);
    }
}
