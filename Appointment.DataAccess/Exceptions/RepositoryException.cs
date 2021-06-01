﻿using System;

namespace Appointment.DataAccess.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }
    }
}