using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems(string name);
        Item GetItem(Guid itemId);
        Item GetItem(string itemName);
        void PostItem(Item item);
        void PutItem(Guid itemId, Item item);
        void DelteItem(Guid itemId);
    }
}