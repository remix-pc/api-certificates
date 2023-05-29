using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CertificatesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImagesCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageCertificatePath",
                table: "Certificates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCertificatePath",
                table: "Certificates");
        }
    }
}
