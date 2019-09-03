using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AuthModel
    {
        public string State { get; set; }
        public string RedirectUri { get; set; }
        public string ResponseType { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string Code { get; set; }
    }

    ///response_type=code&client_id=123
}
