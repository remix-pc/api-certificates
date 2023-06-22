using CertificatesAPI.Models;

namespace CertificatesAPI.Services.Interface
{
    public interface ICertificateRepository : IRepository<Certificate>
    {
        Task<IEnumerable<Certificate>> GetCertificatesCategory(int id);
    }
}
