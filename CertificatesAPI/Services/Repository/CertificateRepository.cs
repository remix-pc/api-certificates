using CertificatesAPI.Data;
using CertificatesAPI.Models;
using CertificatesAPI.Services.Interface;

namespace CertificatesAPI.Services.Repository
{
    public class CertificateRepository : Repository<Certificate>, ICertificateRepository
    {
        public CertificateRepository(AppDbContext context) : base(context)
        {
        }
    }
}
