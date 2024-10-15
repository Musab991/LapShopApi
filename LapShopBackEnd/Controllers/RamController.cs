using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public RamController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific ram by its ID.
        /// </summary>
        /// <param name="id">The ID of the ram to retrieve.</param>
        /// <returns>A specific ram.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {

                var ram = await _unitOfWork.Rams.FindOneAsync(it => it.RamId == id);

                if (ram == null)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "ram not found" }
                    };

                    return NotFound(errorResponse);  // Return 404 with error message

                }

                // If found, return the ram wrapped in ApiResponse
                var successResponse = new ApiResponse(ram, ResponseStatus.Success);
                return Ok(successResponse);  // Return 200 with the ram data
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
        /// Get all Ram with pagination.
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
                var paginatedRams = await _unitOfWork.Rams
                    .FindAsync(null, skipValue, takeValue, orderBy: ram => ram.RamSize,
                    orderByDirection: OrderBy.Ascending, null);


                if (paginatedRams == null || paginatedRams.Count() == 0)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Rams not found" }
                    };

                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(paginatedRams, ResponseStatus.Success);
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
        public async Task<IActionResult> AddNewRam([FromBody] int ramSize)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    TbRam newRam = new TbRam()
                    {
                        RamSize = ramSize
                    };
                    newRam = await _unitOfWork.Rams.AddAsync(newRam);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse("New Ram Id is : " + newRam.RamId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(ramSize, ResponseStatus.NotValid));

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
        public async Task<IActionResult> UpdateRam([FromRoute] int id, [FromBody] int ramSize)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingRam = await _unitOfWork.Rams.FindOneAsync(it => it.RamId == id);

                    if (existingRam == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }
                    existingRam.RamSize = ramSize;

                    _unitOfWork.Rams.Update(existingRam);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse(existingRam, ResponseStatus.Success));

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
        public async Task<IActionResult> DeleteRam([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingRam = await _unitOfWork.Rams.FindOneAsync(it => it.RamId == id);


                if (existingRam == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.Rams.Delete(existingRam);


                await _unitOfWork.SaveAsync();


                return Ok(new ApiResponse($"Ram with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
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
