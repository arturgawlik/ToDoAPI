using System;
using System.Collections.Generic;
using Core.Domain;
using Infrastructure.Dto;

namespace Infrastructure.Services
{
    public interface IItemService
    {
        IEnumerable<ItemDto> GetAll(string name = "");
        ItemDto Get(Guid itemId);
        ItemDto Get(string itemName);
        void Post(Guid ItemId, string name, string content);
        void Put(Guid ItemId, Item item);
        void Delete(Guid itemId);
    }
}