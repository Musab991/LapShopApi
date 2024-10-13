
using BuisnessLibrary.Dto.ItempType;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public ItemTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific itemType by its ID.
        /// </summary>
        /// <param name="id">The ID of the itemType to retrieve.</param>
        /// <returns>A specific itemType.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {

            var itemType = await _unitOfWork.ItemTypes.FindOneAsync(it => it.ItemTypeId == id);

            if (itemType == null)
            {
                var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                {
                    Errors = new List<string> { "itemType not found" }
                };

                return NotFound(errorResponse);  // Return 404 with error message

            }

            // If found, return the itemType wrapped in ApiResponse
            var successResponse = new ApiResponse(itemType, ResponseStatus.Success);
            return Ok(successResponse);  // Return 200 with the itemType data
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
        /// Get all ItemType with pagination.
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
                var paginatedItemTypes = await _unitOfWork.ItemTypes
                    .FindAsync(null, skipValue, takeValue, orderBy: cat => cat.ItemTypeName,
                    orderByDirection: OrderBy.Ascending, null);


                IEnumerable<ItemTypeDto> listPaginatedItemTypesDto =
                    ItemTypeDtoMethods.convertItemTypesToListOfItemTypesDto(paginatedItemTypes);

                if (listPaginatedItemTypesDto == null || listPaginatedItemTypesDto.Count() == 0)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "ItemTypes not found" }
                    };

                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(listPaginatedItemTypesDto, ResponseStatus.Success);
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
        public async Task<IActionResult> AddNewItemType([FromBody] ItemTypeAddDto itemTypeDto)
        {

            try
            {


                if (ModelState.IsValid)
                {
                    // Map ItemTypeAddDto to TbItemType
                    var itemType = _Mapper.Map<TbItemType>(itemTypeDto);

                    await _unitOfWork.ItemTypes.AddAsync(itemType);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse("NewItemTypeId is : " + itemType.ItemTypeId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(itemTypeDto, ResponseStatus.NotValid));

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
        public async Task<IActionResult> UpdateItemType([FromBody] ItemTypeUpdateDto updateItemTypeDto)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingItemType = await _unitOfWork.ItemTypes.FindOneAsync(it => it.ItemTypeId == updateItemTypeDto.ItemTypeId);

                    if (existingItemType == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }

                    // Map updateItemTypeDto to existingItemType
                    _Mapper.Map(updateItemTypeDto, existingItemType);

                    _unitOfWork.ItemTypes.Update(existingItemType);

                    await _unitOfWork.SaveAsync();


                    return Ok(new ApiResponse(updateItemTypeDto, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(updateItemTypeDto, ResponseStatus.NotValid));

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
        public async Task<IActionResult> DeleteItemType([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingItemType = await _unitOfWork.ItemTypes.FindOneAsync(it => it.ItemTypeId == id);


                if (existingItemType == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.ItemTypes.Delete(existingItemType);


                await _unitOfWork.SaveAsync();


                return Ok(new ApiResponse($"ItemType with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
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
