using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Abstract.Models
{
    public record IntegrationEvent
    {
        public IntegrationEvent(string message)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            this.Message = message;
        }
     
     
        public Guid Id { get; private init; }

      
        public DateTime CreationDate { get; private init; }

        public string Message { get; private set; }
    }
}
