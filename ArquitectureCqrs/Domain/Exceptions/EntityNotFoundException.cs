using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {

        public string Entity { get; set; }
        public object Key { get; set; }
        public EntityNotFoundException() { }
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public EntityNotFoundException(string entity, object key) => (Entity, Key) = (entity, key);

    }
}
