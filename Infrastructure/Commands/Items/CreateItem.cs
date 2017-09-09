using System;

namespace Infrastructure.Commands.Items
{
    public class CreateItem
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}