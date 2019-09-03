using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Net.Http;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliceController : ControllerBase
    {

        // POST: api/Alice
        [HttpPost]
        public object Post([FromBody] AliceRequest aliceRequest)
        {
            if (aliceRequest.Meta.Interfaces.AccountLinking != null)
            {

                return aliceRequest.AuthReply();
            }
            else
            {
                return aliceRequest.Reply("авторизация недоступна");
            }
        }

        [HttpGet()]
        public string Token([FromQuery]string code)
        {
            if (code == "12345678")
            {
                return "87654321";
            } else
            {
                return "error";
            }
        }

    }
}
