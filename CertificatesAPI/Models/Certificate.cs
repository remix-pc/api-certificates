using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CertificatesAPI.Models
{
    public class Certificate
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string? Name { get; set; }

        [Required]
        [StringLength(300)]
        public string? description { get; set; }


        public int CategoryId { get; set; }


        [JsonIgnore]
        public Category? Categories { get; set; }

    }
}
