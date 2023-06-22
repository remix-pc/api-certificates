using CertificatesAPI.Data;
using CertificatesAPI.Models;
using CertificatesAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CertificatesAPI.Services.Repository
{
    public class CertificateRepository : Repository<Certificate>, ICertificateRepository
    {
        public CertificateRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Certificate>> GetCertificatesCategory(int id)
        {
            return await _context.Certificates.Where(x => x.CategoryId == id).ToListAsync();
        }
    }
}
