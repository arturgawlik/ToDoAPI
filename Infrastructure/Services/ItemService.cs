using System;
using System.Collections.Generic;
using Core.Domain;
using Core.Repositories;
using Infrastructure.Dto;

namespace Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ItemDto> GetAll(string name = "")
        {
            var items = _repository.GetAllItems(name);
            IList<ItemDto> itemsDto = new List<ItemDto>();
            foreach(var item in items)
            {
                itemsDto.Add(new ItemDto
                {
                    Name = item.Name,
                    Content = item.Content,
                    IsDone = item.IsDone,
                    DoneDate = item.DoneDate
                });
            }

            return itemsDto;
        }

        public ItemDto Get(Guid itemId)
        {
            var item = _repository.GetItem(itemId);
            if(item == null)
            {
                throw new Exception($"There is no item with id: '{itemId}'.");
            }

            return new ItemDto
            {
                Name = item.Name,
                Content = item.Content,
                IsDone = item.IsDone,
                DoneDate = item.DoneDate
            };
        }
        public ItemDto Get(string itemName)
        {
            var item = _repository.GetItem(itemName);
            if(item == null)
            {
                throw new Exception($"There is no item with name: '{itemName}'.");
            }

            return new ItemDto
            {
                Name = item.Name,
                Content = item.Content,
                IsDone = item.IsDone,
                DoneDate = item.DoneDate
            };
        }

        public void Post(Guid ItemId, string name, string content)
        {
            var item = _repository.GetItem(ItemId);
            if(item != null)
            {
                throw new Exception($"Item with Id: '{ItemId}' already exist.");
            }
            item = new Item(ItemId, name, content);
            _repository.PostItem(item);
        }

        public void Put(Guid ItemId, Item item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid itemId)
        {
            _repository.DelteItem(itemId);
        }
    }
}