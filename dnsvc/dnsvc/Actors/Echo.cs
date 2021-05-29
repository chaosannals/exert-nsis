using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnSvc.Actors
{
    public class Echo
    {
        public Response Reply(Request request)
        {
            return new Response(request.Id);
        }
    }
}
