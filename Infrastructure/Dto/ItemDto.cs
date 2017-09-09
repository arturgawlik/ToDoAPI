using System;

namespace Infrastructure.Dto
{
    public class ItemDto
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}