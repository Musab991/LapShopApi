using AutoMapper;
using BuisnessLibrary.Bl.UnitOfWork.Interface;
using BuisnessLibrary.Dto.Category;
using BuisnessLibrary.Dto.Category;
using BuisnessLibrary.Dto.Category;
using DomainLibrary.Constants;
using DomainLibrary.Entities;
using DomainLibrary.Generic;
using LapShop.Model.Api;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A specific category.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.Categories.FindOneAsync(it => it.CategoryId == id);

            if (category == null)
            {
                var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                {
                    Errors = new List<string> { "category not found" }
                };

                return NotFound(errorResponse);  // Return 404 with error message

            }



            // If found, return the category wrapped in ApiResponse
            var successResponse = new ApiResponse(category, ResponseStatus.Success);
            return Ok(successResponse);  // Return 200 with the category data

        }

        /// <summary>
        /// Get all categories with pagination.
        /// </summary>
        /// <param name="skip">The number of records to skip (for pagination).</param>
        /// <param name="take">The number of records to take (for pagination).</param>
        /// <returns>A paginated list of categories.</returns>
        /// 
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromHeader] int? skip, [FromHeader] int? take)
        {
            try
            {

                // Check if skip and take have values, otherwise set default values
                int skipValue = skip ?? 0;   // Default to 0 if not provided
                int takeValue = take ?? 10;  // Default to 10 if not provided

                // Call the pagination function from your repository, passing the skip and take values
                var paginatedCategories = await _unitOfWork.Categories.FindAsync(null, skipValue, takeValue, orderBy: cat => cat.CategoryName,
                    orderByDirection: OrderBy.Ascending, null);


                IEnumerable<CategoryDto> listPaginatedCategoriesDto =
                    CategoryMapper.convertCategoriesToListOfCategoriesDto(paginatedCategories);

                if (listPaginatedCategoriesDto == null || listPaginatedCategoriesDto.Count() == 0)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Categories not found" }
                    };

                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(listPaginatedCategoriesDto, ResponseStatus.Success);
                return Ok(response);

            }
            catch (Exception ex)
            {

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };


                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryAddDto categoryDto)
        {

            try
            {
             

                if (ModelState.IsValid)
                {
                    // Map CategoryAddDto to TbCategory
                    var category = _Mapper.Map<TbCategory>(categoryDto);

                    await _unitOfWork.Categories.AddAsync(category);
                   
                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse("NewCategoryId is : " + category.CategoryId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(categoryDto, ResponseStatus.NotValid));

            }

            catch (Exception ex)
            {
                // Log the exception and rollback the transaction

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);

            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto updateCategoryDto)
        {
            try
            {
             

                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingCategory = await _unitOfWork.Categories.FindOneAsync(it => it.CategoryId == updateCategoryDto.CategoryId);

                    if (existingCategory == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }

                    // Map updateCategoryDto to existingCategory
                    _Mapper.Map(updateCategoryDto, existingCategory);

                    _unitOfWork.Categories.Update(existingCategory);

                       await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse(updateCategoryDto, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(updateCategoryDto, ResponseStatus.NotValid));

            }

            catch (Exception ex)
            {
                // Log the exception and rollback the transaction

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingCategory = await _unitOfWork.Categories.FindOneAsync(it => it.CategoryId == id);


                if (existingCategory == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.Categories.Delete(existingCategory);

         
                await _unitOfWork.SaveAsync();


                return Ok(new ApiResponse($"Category with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
            }
            catch (Exception ex)
            {
            

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }
    }
}
