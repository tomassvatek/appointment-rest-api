using System;

namespace Appointment.DataAccess.Exceptions
{
    public class EntityNotFoundException : RepositoryException
    {
        public Guid EntityId { get; }

        public EntityNotFoundException(Guid entityId, string message, Exception innerException) : base(message,
            innerException)
        {
            EntityId = entityId;
        }

        public EntityNotFoundException(Guid entityId, string message) : base(message)
        {
            EntityId = entityId;
        }
    }
}