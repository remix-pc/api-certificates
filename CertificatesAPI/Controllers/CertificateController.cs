using AutoMapper;
using CertificatesAPI.Data;
using CertificatesAPI.DTOs;
using CertificatesAPI.Models;
using CertificatesAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificatesAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [EnableCors("PermissionApiRequest")]
    [ApiController]
    public class CertificateController : ControllerBase
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly string urlPath = @"Static/Images";

        public CertificateController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }



        [HttpGet]

        public async Task<ActionResult<IEnumerable<CertificateDTO>>> Get()
        {

            var certificate = await _uof.CertificateRepository.Get().ToListAsync();
            var certificateDTO = _mapper.Map<List<CertificateDTO>>(certificate);
            return certificateDTO;
       
        }

        [HttpGet("{id}", Name = "ObterCertificado")]

        public async Task<ActionResult<CertificateDTO>> GetById(int id)
        {

            var certificate = await _uof.CertificateRepository.GetById(c => c.Id == id);

            var certificateDTO = _mapper.Map<CertificateDTO>(certificate);


            return certificate is null ? NotFound() : certificateDTO;


        }

        [HttpPost]
        public async Task<ActionResult> Post(CertificateDTO certificateDTO)
        {
            var certificate = _mapper.Map<Certificate>(certificateDTO);

            _uof.CertificateRepository.Add(certificate);
            await _uof.Commit();
            var certificateDto = _mapper.Map<CertificateDTO>(certificate);
            return new CreatedAtRouteResult("ObterCertificado", new { id = certificate.Id }, certificateDto);
        }


        [HttpPost("image/{id}")]
        public async Task<ActionResult> UploadCertificateImage(IFormFile objFile, int id)
        {

            if (objFile.Length < 0)
            {
                return BadRequest("Invalid image for certificate");
            }

            var certificate = await _uof.CertificateRepository.GetById(c => c.Id == id);

            if(certificate is null)
            {
                return BadRequest("Not found");
            }

            string guid = Guid.NewGuid().ToString();

            using (var stream = System.IO.File.Create(urlPath + "\\" + guid + objFile.FileName))
            {

                await objFile.CopyToAsync(stream);
                stream.Flush();
                certificate.ImageCertificatePath = urlPath + "\\" + guid + objFile.FileName;
            }

            _uof.CertificateRepository.Update(certificate);

            await _uof.Commit();

            return Ok("Image certificate uploaded with success");


        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CertificateDTO certificateDTO)
        {


            var certificate = _mapper.Map<Certificate>(certificateDTO);

            if(id != certificate.Id)
            {
                return BadRequest();
            }

            _uof.CertificateRepository.Update(certificate);
            await _uof.Commit();
            var certificateDto = _mapper.Map<CertificateDTO>(certificate);
            return Ok(certificateDto);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {

            var certificate = await _uof.CertificateRepository.GetById(c => c.Id == id);


            if(certificate is null)
            {
                return NotFound("Nenhum certificado foi encontrado");
            }

            _uof.CertificateRepository.Delete(certificate);
            await _uof.Commit(); 

            var certificateDTO = _mapper.Map<CertificateDTO>(certificate);

            return Ok(certificateDTO);
        }



    }
}
