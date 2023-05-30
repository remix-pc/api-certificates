using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesAPI.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthorizeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {            
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em: " + DateTime.Now.ToLongTimeString();
        }

    }
}
