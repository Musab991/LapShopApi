
using BuisnessLibrary.Dto.Os;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public OsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific Os by its ID.
        /// </summary>
        /// <param name="id">The ID of the Os to retrieve.</param>
        /// <returns>A specific Os.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {

                var Os = await _unitOfWork.Os.FindOneAsync(it => it.OsId == id);

                if (Os == null)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Os not found" }
                    };

                    return NotFound(errorResponse);  // Return 404 with error message

                }

                // If found, return the Os wrapped in ApiResponse
                var successResponse = new ApiResponse(Os, ResponseStatus.Success);
                return Ok(successResponse);  // Return 200 with the Os data
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

        /// <summary>
        /// Get all Os with pagination.
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
                var paginatedOss = await _unitOfWork.Os
                    .FindAsync(null, skipValue, takeValue, orderBy: cat => cat.OsName,
                    orderByDirection: OrderBy.Ascending, null);


                IEnumerable<OsDto> listPaginatedOssDto =
                    OsDtoMethods.convertOssToListOfOssDto(paginatedOss);

                if (listPaginatedOssDto == null || listPaginatedOssDto.Count() == 0)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Oss not found" }
                    };

                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(listPaginatedOssDto, ResponseStatus.Success);
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
        public async Task<IActionResult> AddNewOs([FromBody] OsAddDto OsDto)
        {

            try
            {


                if (ModelState.IsValid)
                {
                    // Map OsAddDto to TbOs
                    var Os = _Mapper.Map<TbO>(OsDto);

                    await _unitOfWork.Os.AddAsync(Os);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse("New Os Id is : " + Os.OsId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(OsDto, ResponseStatus.NotValid));

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
        public async Task<IActionResult> UpdateOs([FromBody] OsUpdateDto updateOsDto)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingOs = await _unitOfWork.Os.FindOneAsync(it => it.OsId == updateOsDto.OsId);

                    if (existingOs == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }

                    // Map updateOsDto to existingOs
                    _Mapper.Map(updateOsDto, existingOs);

                    _unitOfWork.Os.Update(existingOs);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse(updateOsDto, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(updateOsDto, ResponseStatus.NotValid));

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
        public async Task<IActionResult> DeleteOs([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingOs = await _unitOfWork.Os.FindOneAsync(it => it.OsId == id);


                if (existingOs == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.Os.Delete(existingOs);


                await _unitOfWork.SaveAsync();


                return Ok(new ApiResponse($"Os with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
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
