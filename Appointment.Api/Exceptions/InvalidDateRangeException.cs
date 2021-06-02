
namespace AppointmentWebApp.Exceptions
{
    public class InvalidDateRangeException : AppointmentAppException
    {
        public InvalidDateRangeException(string message) : base(message)
        {
        }
    }
}