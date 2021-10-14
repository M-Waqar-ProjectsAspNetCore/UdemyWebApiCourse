using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentityJwt
{
    public class GlobalErrorVM
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string path { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
