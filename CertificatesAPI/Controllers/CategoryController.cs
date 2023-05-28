using AutoMapper;
using CertificatesAPI.DTOs;
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
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var category = await _uof.CategoryRepository.Get().ToListAsync();
            var categoryDTO =  _mapper.Map<List<CategoryDTO>>(category);
            return categoryDTO;

        }

        [HttpGet("certificate")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryCertificates()
        {
            var certificates = await _uof.CategoryRepository.GetCategoryCertificates();

            var certificatesDTO = _mapper.Map<List<CategoryDTO>>(certificates);

            return Ok(certificatesDTO);
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _uof.CategoryRepository.GetById(c => c.Id == id);

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return category == null ? NotFound("Categoria não encontrada") : categoryDTO;

        }


        [HttpPost]

        public async Task<ActionResult> Post(CategoryDTO categoryDTO)
        {

            var category = _mapper.Map<Category>(categoryDTO);

            if(category is null)
            {
                return BadRequest();
            }

            _uof.CategoryRepository.Add(category);
            await _uof.Commit();


            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, category);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoryDTO categoryDTO)
        {

            var category = _mapper.Map<Category>(categoryDTO);

            if(id != category.Id)
            {
                return BadRequest();
            }

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

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return Ok("Categoria deletada!" + categoryDTO);

        }

    }
}
