using System.ComponentModel.DataAnnotations;

namespace CertificatesAPI.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
