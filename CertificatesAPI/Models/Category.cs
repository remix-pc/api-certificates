using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CertificatesAPI.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<Certificate>? Certificates { get; set; }



    }
}
