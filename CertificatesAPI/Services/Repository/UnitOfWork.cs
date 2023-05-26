using CertificatesAPI.Data;
using CertificatesAPI.Services.Interface;

namespace CertificatesAPI.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private CertificateRepository _certificateRepository;
        private CategoryRepository _categoryRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICertificateRepository CertificateRepository
        {
            get
            {
                return _certificateRepository = _certificateRepository ?? new CertificateRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose()
        {

            _context.Dispose();


        }
    }
}
