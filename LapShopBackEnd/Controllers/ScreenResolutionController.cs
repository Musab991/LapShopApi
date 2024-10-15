using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenResolutionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public ScreenResolutionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific screenResolution by its ID.
        /// </summary>
        /// <param name="id">The ID of the screenResolution to retrieve.</param>
        /// <returns>A specific screenResolution.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {

                var screenResolution = await _unitOfWork.ScreenResolutions.FindOneAsync(it => it.ScreenResolutionId == id);

                if (screenResolution == null)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "screenResolution not found" }
                    };

                    return NotFound(errorResponse);  // Return 404 with error message

                }

                // If found, return the screenResolution wrapped in ApiResponse
                var successResponse = new ApiResponse(screenResolution, ResponseStatus.Success);
                return Ok(successResponse);  // Return 200 with the screenResolution data
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
        /// Get all ScreenResolution with pagination.
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
                var paginatedScreenResolutions = await _unitOfWork.ScreenResolutions
                    .FindAsync(null, skipValue, takeValue, orderBy: cat => cat.ScreenResolutionName,
                    orderByDirection: OrderBy.Ascending, null);


                if (paginatedScreenResolutions == null || paginatedScreenResolutions.Count() == 0)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "ScreenResolutions not found" }
                    };

                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(paginatedScreenResolutions, ResponseStatus.Success);
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
        public async Task<IActionResult> AddNewScreenResolution([FromBody] string screenResolutionName)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    TbScreenResolution newScreenResolution = new TbScreenResolution()
                    {
                        ScreenResolutionName = screenResolutionName
                    };
                    newScreenResolution = await _unitOfWork.ScreenResolutions.AddAsync(newScreenResolution);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse("New ScreenResolution Id is : " + newScreenResolution.ScreenResolutionId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(screenResolutionName, ResponseStatus.NotValid));

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreenResolution([FromRoute] int id, [FromBody] string screenResolutionName)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingScreenResolution = await _unitOfWork.ScreenResolutions.FindOneAsync(it => it.ScreenResolutionId == id);

                    if (existingScreenResolution == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }
                    existingScreenResolution.ScreenResolutionName = screenResolutionName;

                    _unitOfWork.ScreenResolutions.Update(existingScreenResolution);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse(existingScreenResolution, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse("Failed to update", ResponseStatus.NotValid));

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
        public async Task<IActionResult> DeleteScreenResolution([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingScreenResolution = await _unitOfWork.ScreenResolutions.FindOneAsync(it => it.ScreenResolutionId == id);


                if (existingScreenResolution == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.ScreenResolutions.Delete(existingScreenResolution);


                await _unitOfWork.SaveAsync();


                return Ok(new ApiResponse($"ScreenResolution with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
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
