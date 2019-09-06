using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliceController : ControllerBase
    {
        private AliceService service;

        public AliceController()
        {
            service = new AliceService();
        }
        
        [HttpPost]
        public object LunchRequest([FromBody] AliceRequest aliceRequest, [FromHeader] string Authorization = null)
        {
            if (Authorization != null)
            {
                new AliceLunchService().Execute("проверка токена авторизации");
                if (service.CheckAuthorization(Authorization, aliceRequest.Session.SessionId))
                {
                    new AliceLunchService().Execute("активация навыка");
                    var answer = service.ProcessLunchRequest(aliceRequest);
                    return aliceRequest.Reply(answer);
                }

                else
                {
                    new AliceLunchService().Execute("ОШИБКА АВТОРИЗАЦИИ");
                    return aliceRequest.Reply("Ошибка авторизации, попробуйте еще раз");
                }
            }
            else if (aliceRequest.Meta.Interfaces.AccountLinking != null)
            {
                new AliceLunchService().Execute("запрос авторизации");
                return aliceRequest.AuthReply();
            }
            else
            {
                new AliceLunchService().Execute("авторизация не поддерживается");
                return aliceRequest.Reply("авторизация недоступна");
            }
        }

        /*[HttpPost]
        public AliceResponse ClinicRequest([FromBody] AliceRequest aliceRequest)
        {
            var answer = service.ProcessClinicRequest(aliceRequest);
            return aliceRequest.Reply(answer);
        }*/
    }
}
