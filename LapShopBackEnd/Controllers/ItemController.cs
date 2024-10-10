using DomainLibrary.Generic;
using DomainLibrary.Constants;
using DomainLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using BuisnessLibrary.Data;
using BuisnessLibrary.Bl.UnitOfWork;
using BuisnessLibrary.Bl.UnitOfWork.Interface;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IUnitOfWork _UnitOfWork;

        //While Creating an Instance of UnitOfWork, we need to specify the Actual Context Object
        public ItemController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
            
        }

        /// <summary>
        /// Get a specific item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>A specific item.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var item = _UnitOfWork.Items.Find(it=>it.ItemId==id,orderBy:it=>it.SalesPrice
            , orderByDirection: OrderBy.Ascending, null,null,new[] {"Processor"});
         
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {

          



            return Ok();
        }
    }
}
