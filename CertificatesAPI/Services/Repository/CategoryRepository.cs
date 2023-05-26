using CertificatesAPI.Data;
using CertificatesAPI.Models;
using CertificatesAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CertificatesAPI.Services.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoryCertificates()
        {
            return await _context.Categories.Include(c => c.Certificates).ToListAsync();
        }
    }
}
