namespace CertificatesAPI.DTOs
{
    public class CategoryDTO
    {


        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<CertificateDTO>? Certificates { get; set; }

    }
}
