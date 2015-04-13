using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ticketing_System.Core
{
    public class CustomResponse
    {
        public CustomResponseStatus Status;
        public Object Response;
        public string Message;
    }
    public enum CustomResponseStatus
    {
        Successful,
        UnSuccessful,
        Exception
    }
}