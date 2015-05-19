using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Site.Models;
using Crosscutting.EntityTenant;
using System.Threading;
using BusinessLogicLayer.TenantInterfaces;
using BusinessLogicLayer.TenantControllers;




namespace Site.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        IBLUsuario _bl=new BLUsuario();

        //[ThreadStatic]  
        public string valor_tenant;
        //[ThreadStatic]
        public LocalDataStoreSlot local;
        
        private UsuarioSite user_sitio;
        private Usuario user;
         
        public AccountController()
        {
        }


        [AllowAnonymous]
        public ActionResult Otro()
        {
            System.Diagnostics.Debug.WriteLine("Entro a logout");
            //ViewBag.ReturnUrl = returnUrl;
            Session.Clear();
            //return View();
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Logout
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            System.Diagnostics.Debug.WriteLine("Entro a logout");
            //ViewBag.ReturnUrl = returnUrl;
            var user = Session["usuario"] as UsuarioSite;
            user.Nombre = null;
            user.Password = null;
            user.Email = null;
            //return View();
            return RedirectToAction("Index", "Home");
        }



        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
               //var user = Session["usuario"] as UsuarioSite;
                //if (user.Dominio != null) valor_tenant = user.Dominio;

               /* local = Thread.GetNamedDataSlot("tenant");
                if (local == null) System.Diagnostics.Debug.WriteLine("Valor tenant nulo");
                else
                {
                    System.Diagnostics.Debug.WriteLine("Tenant " + Thread.GetData(local));
                    valor_tenant = (String)Thread.GetData(local);
                }*/

            }
            catch (Exception)
            {
                
                throw;
            }
           return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Intento de inicio de sesión no válido.");

            }

            else {
                    local = System.Threading.Thread.GetNamedDataSlot("tenant");
                    //System.Diagnostics.Debug.WriteLine("Login : " + valor_tenant);
                    //valor_tenant = (String)Thread.GetData(local);
                   // System.Diagnostics.Debug.WriteLine("Login de usuario en tenant :" + valor_tenant);

                    user_sitio = Session["usuario"] as UsuarioSite;

                    if (user_sitio.Dominio != null)
                    {
                        System.Diagnostics.Debug.WriteLine(" Dominio en sesion Login " + user_sitio.Dominio.ToString());
                        valor_tenant = user_sitio.Dominio.ToString();
                    }
                    //_bl.ExisteUsuario(valor_tenant, model.Nick);
                    if(_bl.ExisteUsuario(valor_tenant, model.Email)) {

                        Usuario usr = _bl.LoginUsuario(valor_tenant, model.Email, model.Password);
                        if (usr != null) {

                            var user_Session = Session["usuario"] as UsuarioSite;
                            user_Session.Email = usr.email;
                            user_Session.Nombre = usr.nombre;
                            user_Session.Password = usr.password;
                            
                           // Session["usuario"] = new UsuarioSite { Nombre = usr.nombre, Email = usr.email, Password = usr.password, Dominio = valor_tenant };
                            return RedirectToAction("Index", "Tenant");

                        }
                        else {
                            ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                        }

                    }
                    else{
                        ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                    }
            }

            return View(model);
        }

        
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            try
            {

               
                
            

            //System.Threading.
           // var user = Session["usuario"] as UsuarioSite;
            //if (user.Dominio != null) valor_tenant = user.Dominio;

           /* local = Thread.GetNamedDataSlot("tenant");
            if (local == null) System.Diagnostics.Debug.WriteLine("Valor tenant nulo");
            else System.Diagnostics.Debug.WriteLine("Tenant " + Thread.GetData(local));
            valor_tenant = System.Threading.Thread.GetData(local).ToString();*/
            //Sitio sitio = Session["sitio"] as Sitio;
            }
            catch (Exception)
            {
                throw;
            }
            
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //_bl = new BLUsuario();
               // String valor_tenant=null;
                try
                {
                    //Thread t = System.Threading.Thread.CurrentThread;
                    /*local =Thread.GetNamedDataSlot("tenant");
                    if (local == null)
                    {
                        System.Diagnostics.Debug.WriteLine("Valor tenant nulo");
                        return View("Error");
                    }
                    else
                    {*/

                        user_sitio = Session["usuario"] as UsuarioSite;

                        if (user_sitio.Dominio != null)
                        {
                            System.Diagnostics.Debug.WriteLine(" Dominio en sesion Registro " + user_sitio.Dominio.ToString());
                            valor_tenant = user_sitio.Dominio.ToString();
                        }
                        //System.Threading.
                        //System.Diagnostics.Debug.WriteLine("Registrando : " + valor_tenant);
                        //valor_tenant = (String)Thread.GetData(local);
                        //System.Diagnostics.Debug.WriteLine("Registro de usuario en tenant :" + valor_tenant);
                        
                       // _bl.ExisteUsuario(valor_tenant, model.Email);
                        //System.Diagnostics.Debug.WriteLine("Registrando : " + valor_tenant + "Usuario : " + model.Email.ToString());
                        if (!(_bl.ExisteUsuario(valor_tenant, model.Email)))
                        {
                            
                            user = new Usuario { email = model.Email, nick = model.Nick, password = model.Password, fecha_Alta = DateTime.Now };

                            System.Diagnostics.Debug.WriteLine("Registrando : " + valor_tenant + "Usuario : " + user.ToString());
                            _bl.RegistrarUsuario(valor_tenant, user);
                            //Session["usuario"] = new UsuarioSite { Nombre = model.Nombre, Email = model.Email, Password = model.Password, Dominio = valor_tenant };

                            var user_Session = Session["usuario"] as UsuarioSite;
                            user_Session.Email = model.Email;
                            user_Session.Nombre = model.Nombre;
                            user_Session.Password = model.Password;

                            return RedirectToAction("Index", "Tenant");
                        }
                        else
                        {
                            return View("Error");
                        }
                    

                }
                catch (Exception)
                {
                    
                    throw;
                }
               
                    
                    // Para obtener más información sobre cómo habilitar la confirmación de cuenta y el restablecimiento de contraseña, visite http://go.microsoft.com/fwlink/?LinkID=320771
                    // Enviar correo electrónico con este vínculo
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");

                   // return RedirectToAction("Index", "Home");
               // }
               
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        /*[AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }*/

        //
        // GET: /Account/ForgotPassword
        /*[AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }*/

        //
        // POST: /Account/ForgotPassword
        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // No revelar que el usuario no existe o que no está confirmado
                    return View("ForgotPasswordConfirmation");
                }

                // Para obtener más información sobre cómo habilitar la confirmación de cuenta y el restablecimiento de contraseña, visite http://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar correo electrónico con este vínculo
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Restablecer contraseña", "Para restablecer la contraseña, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }*/

        //
        // GET: /Account/ForgotPasswordConfirmation
       /* [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }*/

        //
        // POST: /Account/ResetPassword
      /*  [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // No revelar que el usuario no existe
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }*/

        //
        // GET: /Account/ResetPasswordConfirmation
       /* [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }*/

        //
        // POST: /Account/ExternalLogin
       /* [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Solicitar redireccionamiento al proveedor de inicio de sesión externo
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }*/

        //
        // POST: /Account/SendCode
       /* [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generar el token y enviarlo
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }*/

        //
        // GET: /Account/ExternalLoginCallback
        /*[AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Si el usuario ya tiene un inicio de sesión, iniciar sesión del usuario con este proveedor de inicio de sesión externo
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Si el usuario no tiene ninguna cuenta, solicitar que cree una
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }*/

        //
        // POST: /Account/ExternalLoginConfirmation
        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Obtener datos del usuario del proveedor de inicio de sesión externo
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }*/

       
      

        //
        // GET: /Account/ExternalLoginFailure
        /*[AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }*/

       /* protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }*/

      /*  #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }*/

       /* private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }*/

       /* private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }*/

       /* internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion*/
    }
}