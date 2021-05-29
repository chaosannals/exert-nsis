using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DnSvc.Work
{
    /// <summary>
    /// 响应结构。
    /// </summary>
    public class Response
    {
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("requestInvalid")]
        public bool RequestInvalid { get; set; }

        [JsonProperty("responseKeep")]
        public bool ResponseKeep { get; set; }

        [JsonProperty("responseTip")]
        public string ResponseTip { get; set; }

        public Response(string rid, string tip = null, bool keep=false, bool invalid=false)
        {
            RequestId = rid;
            RequestInvalid = invalid;
            ResponseTip = tip;
            ResponseKeep = keep;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// 带数据的响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        [JsonProperty("responseData")]
        public T ResponseData { get; set; }

        /// <summary>
        /// 构造。
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="data"></param>
        /// <param name="keep"></param>
        public Response(string rid, T data, bool keep = false) : base(rid, null, keep, false)
        {
            ResponseData = data;
        }

        public Response(string rid, T data, string tip, bool keep = false): base(rid, tip, keep, false)
        {
            ResponseData = data;
        }
    }
}
