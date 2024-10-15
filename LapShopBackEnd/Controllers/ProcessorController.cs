using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProccessorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public ProccessorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific processor by its ID.
        /// </summary>
        /// <param name="id">The ID of the processor to retrieve.</param>
        /// <returns>A specific processor.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {

                var processor = await _unitOfWork.Processors.FindOneAsync(it => it.ProcessorId == id);

                if (processor == null)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Processors not found" }
                    };

                    return NotFound(errorResponse);  // Return 404 with error message

                }

                // If found, return the Processors wrapped in ApiResponse
                var successResponse = new ApiResponse(processor, ResponseStatus.Success);
                return Ok(successResponse);  // Return 200 with the processor data
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
        /// Get all Proccessor with pagination.
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
                var paginatedProccessors = await _unitOfWork.Processors
                    .FindAsync(null, skipValue, takeValue, orderBy: proc => proc.ProcessorName,
                    orderByDirection: OrderBy.Ascending, null);


                if (paginatedProccessors == null || paginatedProccessors.Count() == 0)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Proccessors not found" }
                    };

                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(paginatedProccessors, ResponseStatus.Success);
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
        public async Task<IActionResult> AddNewProccessor([FromBody] string processorName)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    TbProcessor newProccessor = new TbProcessor()
                    {
                        ProcessorName = processorName
                    };
                    newProccessor = await _unitOfWork.Processors.AddAsync(newProccessor);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse("New Proccessor Id is : " + newProccessor.ProcessorId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(processorName, ResponseStatus.NotValid));

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
        public async Task<IActionResult> UpdateProccessor([FromRoute] int id, [FromBody] string processorName)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingProccessor = await _unitOfWork.Processors.FindOneAsync(it => it.ProcessorId == id);

                    if (existingProccessor == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }
                    existingProccessor.ProcessorName = processorName;

                    _unitOfWork.Processors.Update(existingProccessor);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse(existingProccessor, ResponseStatus.Success));

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
        public async Task<IActionResult> DeleteProccessor([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingProccessor = await _unitOfWork.Processors.FindOneAsync(it => it.ProcessorId == id);


                if (existingProccessor == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.Processors.Delete(existingProccessor);


                await _unitOfWork.SaveAsync();


                return Ok(new ApiResponse($"Proccessor with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
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
