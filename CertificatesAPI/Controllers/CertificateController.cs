using CertificatesAPI.Data;
using CertificatesAPI.Models;
using CertificatesAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificatesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {

        private readonly IUnitOfWork _uof;

        public CertificateController(IUnitOfWork context)
        {
            _uof = context;
        }



        [HttpGet]

        public async Task<ActionResult<IEnumerable<Certificate>>> Get()
        {

            return await _uof.CertificateRepository.Get().ToListAsync();
       
        }

        [HttpGet("{id}", Name = "ObterCertificado")]

        public async Task<ActionResult<Certificate>> GetById(int id)
        {

            var certificate = await _uof.CertificateRepository.GetById(c => c.Id == id);


            return certificate is null ? NotFound() : certificate;


        }

        [HttpPost]
        public async Task<ActionResult> Post(Certificate certificate)
        {
            _uof.CertificateRepository.Add(certificate);
            await _uof.Commit();
            return new CreatedAtRouteResult("ObterCertificado", new { id = certificate.Id }, certificate);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Certificate certificate)
        {

            if(id != certificate.Id)
            {
                return BadRequest();
            }

            _uof.CertificateRepository.Update(certificate);
            await _uof.Commit();
            return Ok(certificate);

        }

        [HttpDelete("")]

        public async Task<ActionResult> Delete(int id)
        {

            var certificate = await _uof.CertificateRepository.GetById(c => c.Id == id);

            if(certificate is null)
            {
                return NotFound("Nenhum certificado foi encontrado");
            }

            _uof.CertificateRepository.Delete(certificate);
            await _uof.Commit(); 

            return Ok(certificate);
        }



    }
}
