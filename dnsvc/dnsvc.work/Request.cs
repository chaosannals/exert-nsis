using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace DnSvc.Work
{
    /// <summary>
    /// 请求
    /// </summary>
    public class Request
    {
        public string Id { get; private set; }
        public Type Actor { get; private set; }
        public MethodInfo Action { get; private set; }
        public JToken Data { get; private set; }
        public JObject AllInfo { get; private set; }

        public Request(string message)
        {
            AllInfo = JObject.Parse(message);
            Id = AllInfo["id"].ToString();
            Actor = Type.GetType("DnSvc.Actors." + AllInfo["actor"].ToString());
            Action = Actor.GetMethod(AllInfo["action"].ToString());
            Data = AllInfo["data"];
        }

        public object NewActor()
        {
            return Actor.Assembly.CreateInstance(Actor.FullName);
        }
    }
}
