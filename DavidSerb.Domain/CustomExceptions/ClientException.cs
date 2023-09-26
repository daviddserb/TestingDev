using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.CustomExceptions
{
    /// <summary>
    /// Bad Request - 400
    /// </summary>
    public class ClientException : CustomExceptionBase
    {
        public ClientException(string message) : base(message) { }
    }
}
