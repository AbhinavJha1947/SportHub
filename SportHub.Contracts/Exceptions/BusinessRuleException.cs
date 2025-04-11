using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportHub.Contracts.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException() : base("A business rule was violated.")
        {
        }

        public BusinessRuleException(string message) : base(message)
        {
        }

        public BusinessRuleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
