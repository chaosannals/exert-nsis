using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using Newtonsoft.Json.Linq;

namespace DnSvc
{
    public delegate void MessageHandler(MessageClient client, JObject data);

    public class MessageClient : IDisposable
    {
        private List<string> requests;
        private Dictionary<string, MessageHandler> responses;

        public WebSocket Socket { get; private set; }

        public MessageClient()
        {
            requests = new List<string>();
            responses = new Dictionary<string, MessageHandler>();
            Socket = new WebSocket("ws://127.0.0.1:12310");
            Socket.OnOpen += (o, e) => {
                lock (requests)
                {
                    foreach (string request in requests)
                    {
                        Socket.Send(request);
                    }
                    requests.Clear();
                }
            };
            Socket.OnMessage += (o, e) => {
                JObject data = JObject.Parse(e.Data);
                string rid = data["requestId"].ToString();
                lock(responses)
                {
                    if (responses.ContainsKey(rid))
                    {
                        MessageHandler handler = responses[rid];
                        handler.Invoke(this, data);
                        responses.Remove(rid);
                    }
                }
            };
        }
        public void Send<T>(T data, MessageHandler handler)
        {
            string rid = Guid.NewGuid().ToString("N");
            JObject pack = new JObject();
            pack["id"] = rid;
            pack["data"] = JToken.FromObject(data);
            lock (responses)
            {
                responses.Add(rid, handler);
            }
            if (Socket.ReadyState != WebSocketState.Open)
            {
                lock (requests)
                {
                    requests.Add(pack.ToString());
                }
            }
            else
            {
                Socket.Send(pack.ToString());
            }
        }

        public void Dispose()
        {
            Socket.Close();
        }
    }
}
