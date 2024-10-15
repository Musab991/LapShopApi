using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardDiskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public HardDiskController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific gpu by its ID.
        /// </summary>
        /// <param name="id">The ID of the gpu to retrieve.</param>
        /// <returns>A specific gpu.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {

                var gpu = await _unitOfWork.HardDisks.FindOneAsync(it => it.HardDiskId == id);

                if (gpu == null)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "gpu not found" }
                    };

                    return NotFound(errorResponse);  // Return 404 with error message

                }

                // If found, return the gpu wrapped in ApiResponse
                var successResponse = new ApiResponse(gpu, ResponseStatus.Success);
                return Ok(successResponse);  // Return 200 with the gpu data
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
        /// Get all HardDisk with pagination.
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
                var paginatedHardDisks = await _unitOfWork.HardDisks
                    .FindAsync(null, skipValue, takeValue, orderBy: cat => cat.HardDiskName,
                    orderByDirection: OrderBy.Ascending, null);


                if (paginatedHardDisks == null || paginatedHardDisks.Count() == 0)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "HardDisks not found" }
                    };

                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(paginatedHardDisks, ResponseStatus.Success);
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
        public async Task<IActionResult> AddNewHardDisk([FromBody] string gpuName)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    TbHardDisk newHardDisk = new TbHardDisk()
                    {
                        HardDiskName = gpuName
                    };
                    newHardDisk = await _unitOfWork.HardDisks.AddAsync(newHardDisk);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse("New HardDisk Id is : " + newHardDisk.HardDiskId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(gpuName, ResponseStatus.NotValid));

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
        public async Task<IActionResult> UpdateHardDisk([FromRoute] int id, [FromBody] string gpuName)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingHardDisk = await _unitOfWork.HardDisks.FindOneAsync(it => it.HardDiskId == id);

                    if (existingHardDisk == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }
                    existingHardDisk.HardDiskName = gpuName;

                    _unitOfWork.HardDisks.Update(existingHardDisk);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse(existingHardDisk, ResponseStatus.Success));

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
        public async Task<IActionResult> DeleteHardDisk([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingHardDisk = await _unitOfWork.HardDisks.FindOneAsync(it => it.HardDiskId == id);


                if (existingHardDisk == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.HardDisks.Delete(existingHardDisk);


                await _unitOfWork.SaveAsync();


                return Ok(new ApiResponse($"HardDisk with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
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
