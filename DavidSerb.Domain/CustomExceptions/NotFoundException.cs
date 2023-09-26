using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.CustomExceptions
{
    /// <summary>
    ///  Not Found - 404
    /// </summary>
    public class NotFoundException : CustomExceptionBase
    {
        public NotFoundException(string message) : base(message) { }
    }
}
