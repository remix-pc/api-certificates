using CertificatesAPI.Models;

namespace CertificatesAPI.Services.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {

        Task<IEnumerable<Category>> GetCategoryCertificates();

    }
}
