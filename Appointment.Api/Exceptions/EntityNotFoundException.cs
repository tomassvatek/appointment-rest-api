using System;

namespace AppointmentWebApp.Exceptions
{
    public class EntityNotFoundException : AppointmentAppException
    {
        public Guid EntityId { get; }

        public EntityNotFoundException(Guid entityId, string message) : base(message)
        {
            EntityId = entityId;
        }
    }
}