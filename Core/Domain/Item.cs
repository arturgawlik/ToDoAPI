using System;

namespace Core.Domain
{
    public class Item : Entity
    {
        public string Name { get; protected set; }
        public string Content { get; protected set; }
        public DateTime CreateDate { get; protected set; }
        public DateTime UpdateDate { get; protected set; }
        public bool IsDone { get; protected set; }
        public DateTime? DoneDate { get; protected set; }

        protected Item()
        {
        }

        public Item(Guid id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
            CreateDate = DateTime.UtcNow;
            Update();
            IsDone = false;
            DoneDate = null;
        }

        private void Update()
        {
            UpdateDate = DateTime.UtcNow;
        }
    }
}