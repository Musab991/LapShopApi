using DomainLibrary.Entities;
using DomainLibrary.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<TbCategory> _CategoryRepsitory;

        public CategoryController(IGenericRepository<TbCategory> categoryService)
        {
            _CategoryRepsitory = categoryService;
        }

        /// <summary>
        /// Get a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A specific category.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var category = 
                _CategoryRepsitory
                .FindOne(c => c.CategoryId == id);
            
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_CategoryRepsitory.GetAll());
        }
    }
}
