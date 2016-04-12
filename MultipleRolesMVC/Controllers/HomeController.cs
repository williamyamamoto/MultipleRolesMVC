using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MultipleRolesMVC.Controllers
{
    public class HomeController : Controller
    {
        // forma extramamente simplificada de autenticar usuários
        // esta logica deve ser subtituída em seus projetos
        // o que foi feito aqui é apenas para alimentar
        // alguns usuários com roles
        public string Login(string user)
        {
            FormsAuthentication.SignOut();

            var roles = "";

            if (user.Equals("fulano", StringComparison.InvariantCultureIgnoreCase))
                roles = "Admin,Manager";

            if (user.Equals("sicrano", StringComparison.InvariantCultureIgnoreCase))
                roles = "Admin";

            if (user.Equals("Beltrano", StringComparison.InvariantCultureIgnoreCase))
                roles = "Admin,User";

            if (user.Equals("Outrano", StringComparison.InvariantCultureIgnoreCase))
                roles = "User";

            var authTicket = new FormsAuthenticationTicket(
              1,
              user,
              DateTime.Now,
              DateTime.Now.AddDays(1),
              false,
              roles,
              "/");
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            Response.Cookies.Add(cookie);

            return "Ok";
        }

        // qualquer usuário autenticado pode acessar
        [Authorize]
        public string ActionProtegida()
        {
            return "Ok";
        }

        // usuários autenticados com a Role Admin
        // podem acessar
        [Authorize(Roles = "Admin")]
        public string ActionProtegidaParaAdmin()
        {
            return "Ok";
        }

        // usuário autenticados com a Role Admin OU
        // com a Role Manager podem acessar
        [Authorize(Roles = "Admin,Manager")]
        public string ActionProtegidaParaAdminOuManager()
        {
            return "Ok";
        }

        // usuários autenticado com a Role Admin E
        // com a Role Manager podem acessar
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        public string ActionProtegidaParaAdminEManager()
        {
            return "Ok";
        }

        // usuários autenticados com a Role Admin
        // E TAMBEM pelo menos uma das Roles Manager Ou User
        // podem acessar
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager,User")]
        public string ActionProtegidaParaAdminManagerOuAdminUser()
        {
            return "Ok";
        }
    }
}