#pragma checksum "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9db061b26a25500f1e3db0d0e9d591786fd21b09"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserRole_Detail), @"mvc.1.0.view", @"/Views/UserRole/Detail.cshtml")]
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
#line 1 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\_ViewImports.cshtml"
using Assignment2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\_ViewImports.cshtml"
using Assignment2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9db061b26a25500f1e3db0d0e9d591786fd21b09", @"/Views/UserRole/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de2f008bb030c689fdd92cf0ac6c09ff2675aa17", @"/Views/_ViewImports.cshtml")]
    public class Views_UserRole_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Assignment2.ViewModels.RoleVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h3>");
#nullable restore
#line 3 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
Write(ViewBag.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n\r\n<p>\r\n    ");
#nullable restore
#line 6 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
Write(Html.ActionLink("Assign Roles", "Create",
                     new { userName = @ViewBag.UserName }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 13 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.RoleName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 21 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 25 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
               Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 28 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
               Write(Html.DisplayFor(modelItem => item.RoleName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 31 "C:\Users\Homer\source\repos\Assignment2\Assignment2\Views\UserRole\Detail.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Assignment2.ViewModels.RoleVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
