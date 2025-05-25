namespace MunicipalityTax.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
            //Why call base(message) ?
            //The Exception base class has built-in support for error messages.
            //By passing message to the base class, you get standard exception behavior like:
            //ex.Message returns your message.

            //what else I cna do in this constructor?
            //You can also log the error, set additional properties, or perform other initialization tasks.

            //example:
            //public int ErrorCode { get; }
            //public BadRequestException(string message, int errorCode) : base(message)
            //{
            //    ErrorCode = errorCode;
            //}
        }
    }
}
