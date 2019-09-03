using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        public AuthModel Model;

        [Route("/[controller]")]
        //[HttpGet("state={state},redirect_uri={redirectUri},response_type={responseType},client_id={clientId},scope={scope}")]
        public IActionResult Auth([FromQuery] string state, [FromQuery] string redirect_uri, [FromQuery] string response_type, [FromQuery] string client_id, [FromQuery] string scope)
        {
            Model = new AuthModel {
                State = state,
                RedirectUri = redirect_uri,
                ResponseType = response_type,
                ClientId = client_id,
                Scope = "read"
            };
            ViewData["Title"] = "Auth";
            return View(Model);
        }

        [HttpGet]
        public IActionResult Back()
        {
            /*WebClient caller = new WebClient();
            try
            {
                //caller.DownloadString("https://https://social.yandex.net/broker/redirect?code=\"12345678\",state={Model.State},client_id={Model.ClientId},scope={Model.Scope}");
                ViewData["Title"] = "Done";
            } catch (Exception e)
            {
                ViewData["Title"] = e.Message;
            }*/
            //Response.Redirect("https://https://social.yandex.net/broker/redirect?code=\"12345678\",state={Model.State},client_id={Model.ClientId},scope={Model.Scope}");
            //ViewData["Redirect"] = "https://https://social.yandex.net/broker/redirect?code=\"12345678\",state={Model.State},client_id={Model.ClientId},scope={Model.Scope}";
            //return View(Model);
            return Redirect("https://social.yandex.net/broker/redirect?code=\"12345678\",state={Model.State},client_id={Model.ClientId},scope={Model.Scope}");
        }
    }
}