using System;

namespace AppointmentWebApp.Exceptions
{
    public class AppointmentAppException : Exception
    {
        public AppointmentAppException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public AppointmentAppException(string message) : base(message)
        {
        }
    }
}