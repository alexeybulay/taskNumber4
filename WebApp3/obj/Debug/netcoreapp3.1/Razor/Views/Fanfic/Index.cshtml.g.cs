#pragma checksum "C:\Users\Administrator\source\repos\WebApp3 — копия (2) — копия\WebApp3\Views\Fanfic\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "064e558106f44e4c38abf596088435d1b705a205"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Fanfic_Index), @"mvc.1.0.view", @"/Views/Fanfic/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Administrator\source\repos\WebApp3 — копия (2) — копия\WebApp3\Views\_ViewImports.cshtml"
using WebApp3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Administrator\source\repos\WebApp3 — копия (2) — копия\WebApp3\Views\_ViewImports.cshtml"
using WebApp3.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"064e558106f44e4c38abf596088435d1b705a205", @"/Views/Fanfic/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e0bcd4db72c0ab92d94eff75657b307fd4bd638", @"/Views/_ViewImports.cshtml")]
    public class Views_Fanfic_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApp3.ViewModels.FanficTagViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Administrator\source\repos\WebApp3 — копия (2) — копия\WebApp3\Views\Fanfic\Index.cshtml"
   ViewData["Title"] = "О фанфике2"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div id=\"view-all\">\r\n    ");
#nullable restore
#line 5 "C:\Users\Administrator\source\repos\WebApp3 — копия (2) — копия\WebApp3\Views\Fanfic\Index.cshtml"
Write(await Html.PartialAsync("FanficList", Model));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 9 "C:\Users\Administrator\source\repos\WebApp3 — копия (2) — копия\WebApp3\Views\Fanfic\Index.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApp3.ViewModels.FanficTagViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
