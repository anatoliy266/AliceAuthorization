#pragma checksum "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c221374daeb36c49dd0a7b3955f727b7ca2a766"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Auth), @"mvc.1.0.view", @"/Views/Shared/Auth.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Auth.cshtml", typeof(AspNetCore.Views_Shared_Auth))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c221374daeb36c49dd0a7b3955f727b7ca2a766", @"/Views/Shared/Auth.cshtml")]
    public class Views_Shared_Auth : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApplication1.Models.AuthModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(41, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(52, 10, true);
            WriteLiteral("\r\n    <h2>");
            EndContext();
            BeginContext(63, 17, false);
#line 7 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
   Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(80, 15, true);
            WriteLiteral("</h2>\r\n    <h2>");
            EndContext();
            BeginContext(96, 11, false);
#line 8 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
   Write(Model.State);

#line default
#line hidden
            EndContext();
            BeginContext(107, 15, true);
            WriteLiteral("</h2>\r\n    <h2>");
            EndContext();
            BeginContext(123, 14, false);
#line 9 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
   Write(Model.ClientId);

#line default
#line hidden
            EndContext();
            BeginContext(137, 15, true);
            WriteLiteral("</h2>\r\n    <h2>");
            EndContext();
            BeginContext(153, 11, false);
#line 10 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
   Write(Model.Scope);

#line default
#line hidden
            EndContext();
            BeginContext(164, 15, true);
            WriteLiteral("</h2>\r\n    <h2>");
            EndContext();
            BeginContext(180, 18, false);
#line 11 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
   Write(Model.ResponseType);

#line default
#line hidden
            EndContext();
            BeginContext(198, 102, true);
            WriteLiteral("</h2>\r\n\r\n    <p>\r\n        <!--<button id=\"btnReturn\" style=\"width: 100px\" onclick=\'window.location = \"");
            EndContext();
            BeginContext(301, 23, false);
#line 14 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
                                                                               Write(Url.Action("auth/back"));

#line default
#line hidden
            EndContext();
            BeginContext(324, 33, true);
            WriteLiteral("\"\' value=\"Some\" />-->\r\n        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\'", 357, "\'", 486, 7);
            WriteAttributeValue("", 364, "https://social.yandex.net/broker/redirect?code=\"12345678\",state=", 364, 64, true);
#line 15 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
WriteAttributeValue("", 428, Model.State, 428, 12, false);

#line default
#line hidden
            WriteAttributeValue("", 440, ",client_id=", 440, 11, true);
#line 15 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
WriteAttributeValue("", 451, Model.ClientId, 451, 15, false);

#line default
#line hidden
            WriteAttributeValue("", 466, ",scope=", 466, 7, true);
#line 15 "C:\Users\prog\source\repos\WebApplication1\WebApplication1\Views\Shared\Auth.cshtml"
WriteAttributeValue("", 473, Model.Scope, 473, 12, false);

#line default
#line hidden
            WriteAttributeValue("", 485, "\"", 485, 1, true);
            EndWriteAttribute();
            BeginContext(487, 25, true);
            WriteLiteral(">Back</a>\r\n    </p>\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication1.Models.AuthModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
