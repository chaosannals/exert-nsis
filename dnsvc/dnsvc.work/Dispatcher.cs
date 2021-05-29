using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DnSvc.Work
{
    public class Dispatcher : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Request request = new Request(e.Data);
            object actor = request.NewActor();
            object result = request.Action.Invoke(actor, new object[] { request });
            if (result is IEnumerable<Response>)
            {
                IEnumerable<Response> responses = result as IEnumerable<Response>;
                foreach (Response response in responses)
                {
                    Send(response.ToJson());
                }
            }
            else if (result is Response)
            {
                Response response = result as Response;
                Send(response.ToJson());
            }
        }
    }
}
