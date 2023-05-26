namespace CertificatesAPI.Services.Interface
{
    public interface IUnitOfWork
    {

        ICertificateRepository CertificateRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task Commit();

    }
}
