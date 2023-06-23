using CertificatesAPI.Models;

namespace CertificatesAPI.DTOs
{
    public class CertificateDTO
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        public string? ImageCertificatePath { get; set; }

        public int CategoryId { get; set; }

    }
}
