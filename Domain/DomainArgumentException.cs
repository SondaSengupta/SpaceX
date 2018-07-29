using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DomainArgumentException : Exception
    {
        public DomainArgumentException()
        {
        }

        public DomainArgumentException(string message) : base(message)
        {
        }
    }
}
