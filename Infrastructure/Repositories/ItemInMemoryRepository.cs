using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class ItemInMemoryRepository : IItemRepository
    {
        private static readonly ISet<Item> _items = new HashSet<Item>();
        
        public IEnumerable<Item> GetAllItems(string name)
        {
            var items = _items.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                items = items.Where(item => item.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
            }

            return items;
        }
        
        public Item GetItem(Guid itemId)
            => _items.FirstOrDefault(p => p.Id == itemId);

        public Item GetItem(string itemName)
            => _items.FirstOrDefault(p => p.Name == itemName);
            
        public void PostItem(Item item)
        {
            _items.Add(item);
        }

        public void PutItem(Guid itemId, Item item)
        {
            throw new NotImplementedException();
        }

        public void DelteItem(Guid itemId)
        {
            var localItem = _items.FirstOrDefault(p => p.Id == itemId);
            if (localItem == null)
            {
                 return;
            }

            _items.Remove(localItem);
        }
    }
}