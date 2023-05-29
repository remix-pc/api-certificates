using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CertificatesAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredinImageCertificationPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageCertificatePath",
                table: "Certificates",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "ImageCertificatePath",
                keyValue: null,
                column: "ImageCertificatePath",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImageCertificatePath",
                table: "Certificates",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
