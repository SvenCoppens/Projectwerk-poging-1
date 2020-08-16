using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    public class IncorrectParameterException : Exception    
    {
        public IncorrectParameterException(string message) : base(message)
        {

        }
    }
}
