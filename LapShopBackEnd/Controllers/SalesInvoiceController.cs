using BuisnessLibrary.Bl.Repository;
using BuisnessLibrary.Dto.Item;
using BuisnessLibrary.Dto.SalesInvoice;
using DomainLibrary.Entities;


namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public SalesInvoiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        /// <summary>
        /// Get a specific Invoice by its ID.
        /// </summary>
        /// <param name="id">The ID of the Invoice to retrieve.</param>
        /// <returns>A specific Invoice.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {

                var findOptions = new FindOptions
                {
                    IsAsNoTracking = true,
                    IsIgnoreAutoIncludes = false // or true depending on your needs
                };

                var invoice = await _unitOfWork.SalesInvoices.FindOneAsync(si => si.InvoiceId == id, 
                    new string[] { "TbSalesInvoiceItems" , "TbSalesInvoiceItems.Item" });

                if (invoice == null)
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Item not found" }
                    };

                    return NotFound(errorResponse);  // Return 404 with error message

                }

                SalesInvoiceDto invoiceDto = InvoiceMapper.ConvertToDto(invoice);


                // If found, return the invoice wrapped in ApiResponse
                var successResponse = new ApiResponse(invoiceDto, ResponseStatus.Success);
                return Ok(successResponse);  // Return 200 with the invoice data
            }
            catch (Exception ex)
            {
                {

                    var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                    {
                        Errors = new List<string> { ex.Message }
                    };


                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);

                }
            }
        }

        /// <summary>
        /// Get all Items with pagination.
        /// </summary>
        /// <param name="skip">The number of records to skip (for pagination).</param>
        /// <param name="take">The number of records to take (for pagination).</param>
        /// <returns>A paginated list of Items.</returns>
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
                var listInvoicesDto = await _unitOfWork.SalesInvoices.FindAsync(null, skipValue, takeValue, orderBy: it => it.InvoiceDate,
                    orderByDirection: OrderBy.Ascending, new[] { "TbSalesInvoiceItems" , "TbSalesInvoiceItems.Item" });


               IEnumerable<SalesInvoiceDto> listPaginatedInvoicesDto = InvoiceMapper.ConvertToDto(listInvoicesDto);

                if (listPaginatedInvoicesDto == null || !listPaginatedInvoicesDto.Any() )
                {
                    var errorResponse = new ApiResponse(null, ResponseStatus.NotFound)
                    {
                        Errors = new List<string> { "Invoices not found" }
                    };


                }


                // Return the paginated result wrapped in an API response
                var response = new ApiResponse(listPaginatedInvoicesDto, ResponseStatus.Success);
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
        public async Task<IActionResult> AddNewItem([FromBody] ItemAddDto itemDto)
        {

            try
            {
                //First, Begin the Transaction
                _unitOfWork.CreateTransaction();

                if (ModelState.IsValid)
                {
                    // Map ItemAddDto to TbItem
                    var item = _Mapper.Map<TbItem>(itemDto);

                    await _unitOfWork.Items.AddAsync(item);
                    await _unitOfWork.SaveAsync();

                    // Handle the images separately
                    await _unitOfWork.ItemImages.UploadImages(item.ItemId, itemDto.ListImages);

                    await _unitOfWork.SaveAsync();

                    // Commit the transaction
                    await _unitOfWork.CommitAsync();


                    return Ok(new ApiResponse("NewItemId is : " + item.ItemId, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(itemDto, ResponseStatus.NotValid));

            }

            catch (Exception ex)
            {
                // Log the exception and rollback the transaction
                _unitOfWork.Rollback();

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);

            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] ItemUpadtDto updateItemDto)
        {
            try
            {
                //First, Begin the Transaction
                _unitOfWork.CreateTransaction();

                if (ModelState.IsValid)
                {

                    // Retrieve existing item from the database
                    var existingItem = await _unitOfWork.Items.FindOneAsync(it => it.ItemId == updateItemDto.ItemId);

                    if (existingItem == null)
                    {

                        return NotFound(new ApiResponse(null, ResponseStatus.NotFound));

                    }

                    // Map updateItemDto to existingItem
                    _Mapper.Map(updateItemDto, existingItem);

                    _unitOfWork.Items.Update(existingItem);

                    await _unitOfWork.SaveAsync();

                    // Handle the images separately
                    await _unitOfWork.ItemImages.UploadImages(updateItemDto.ItemId, updateItemDto.ListImages);

                    await _unitOfWork.SaveAsync();

                    // Commit the transaction
                    await _unitOfWork.CommitAsync();


                    return Ok(new ApiResponse(updateItemDto, ResponseStatus.Success));

                }

                return BadRequest(new ApiResponse(updateItemDto, ResponseStatus.NotValid));

            }

            catch (Exception ex)
            {
                // Log the exception and rollback the transaction
                _unitOfWork.Rollback();

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {

            //check item is exists 
            try
            {

                var existingItem = await _unitOfWork.Items.FindOneAsync(it => it.ItemId == id);


                if (existingItem == null)
                {
                    return NotFound(new ApiResponse(null, ResponseStatus.NotFound));
                }

                _unitOfWork.Items.Delete(existingItem);


                // Delete associated images
                _unitOfWork.ItemImages.DeleteRange(img => img.ItemId == id);

                await _unitOfWork.SaveAsync();

                // Commit the transaction
                await _unitOfWork.CommitAsync();

                return Ok(new ApiResponse($"Item with Id:[{id}] Was deleted succesfully", ResponseStatus.Success)); // Return 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                _unitOfWork.Rollback();

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }



    }


}
