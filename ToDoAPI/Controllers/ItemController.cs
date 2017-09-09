using System;
using Infrastructure.Commands.Items;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet]
        public IActionResult Get(string name)
        {
            var items = _itemService.GetAll(name);

            return Json(items);
        }

        [HttpGet("{itemId}")]
        public IActionResult Get(Guid itemId)
        {
            var item = _itemService.Get(itemId);

            return Json(item);
        }
        [HttpPost]
        public IActionResult Post([FromBody]CreateItem command)
        {
            command.ItemId = Guid.NewGuid();
            _itemService.Post(command.ItemId, command.Name, command.Content);

            return Created($"/item/{command.ItemId}", null);
        }

        [HttpDelete("{itemId}")]
        public IActionResult Delete(Guid itemId)
        {
            _itemService.Delete(itemId);

            return NoContent();
        }
    }
}