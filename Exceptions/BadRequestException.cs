using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

namespace MunicipalityTax.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
            //Why call base(message) ?
            //The Exception base class has built-in support for error messages.
            //By passing message to the base class, you get standard exception behavior like:
            //ex.Message returns your message.

            //what else I cna do in this constructor?
            //You can also log the error, set additional properties, or perform other initialization tasks.
        }

    }
}
