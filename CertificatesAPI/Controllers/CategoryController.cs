using CertificatesAPI.Models;
using CertificatesAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificatesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IUnitOfWork _uof;

        public CategoryController(IUnitOfWork context)
        {
            _uof = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _uof.CategoryRepository.Get().ToListAsync();

        }

        [HttpGet("certificate")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryCertificates()
        {
            var certificates = await _uof.CategoryRepository.GetCategoryCertificates();

            return Ok(certificates);
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _uof.CategoryRepository.GetById(c => c.Id == id);

            return category == null ? NotFound("Categoria não encontrada") : category;

        }


        [HttpPost]

        public async Task<ActionResult> Post(Category category)
        {

            _uof.CategoryRepository.Add(category);
            await _uof.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, category);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Category category)
        {

            _uof.CategoryRepository.Update(category);
            await _uof.Commit();
            return Ok(category);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _uof.CategoryRepository.GetById(c => c.Id == id);

            if (category is null) return BadRequest();

            _uof.CategoryRepository.Delete(category);
            await _uof.Commit();

            return Ok("Categoria deletada!" + category);

        }

    }
}
