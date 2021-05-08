using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DnSvc
{
    public enum MessageType
    {
        Echo,
        Test,
    }

    public class Message
    {
        [JsonProperty("type")]
        public MessageType Type { get; set; }
    }

    public class Message<T>
    {

    }
}
