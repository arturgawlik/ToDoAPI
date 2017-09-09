using System;

namespace Core.Domain
{
    public class Entity
    {
        public Guid Id;
        
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}