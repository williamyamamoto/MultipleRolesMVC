using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MultipleRolesMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        // recriando o usuário com as roles, com base no conteúdo do cookie de autenticação
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var roles = authTicket.UserData.Split(",".ToCharArray());
            var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
            Context.User = userPrincipal;
        }


    }
}
