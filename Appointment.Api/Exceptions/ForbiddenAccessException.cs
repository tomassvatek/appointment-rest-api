using System;

namespace AppointmentWebApp.Exceptions
{
    public class ForbiddenAccessException : AppointmentAppException
    {
        public ForbiddenAccessException(string message) : base(message) 
        {
            
        }
    }
}