using System;

namespace AppointmentWebApp.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message) : base(message) 
        {
            
        }
    }
}