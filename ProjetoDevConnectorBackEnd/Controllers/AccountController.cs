using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DevConnectorBackEnd.Models;
using DevConnectorBackEnd.Models.ViewModels;

namespace DevConnectorBackEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UsuarioModel> userManager;
        private readonly SignInManager<UsuarioModel> signInManager;

        public AccountController(UserManager<UsuarioModel> _userManager, SignInManager<UsuarioModel> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                UsuarioModel usuario = await userManager.FindByEmailAsync(login.Email);
                await signInManager.SignOutAsync();

                var resultado = await signInManager.PasswordSignInAsync(usuario, login.Senha, false, false);
                if (resultado.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao tentar fazer login");
                    return View(login);
                }
            }
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistroViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                UsuarioModel usu = new UsuarioModel
                {
                    UserName = usuario.Nome,
                    Email = usuario.Email
                };

                if (await userManager.FindByNameAsync(usuario.Nome) == null && await userManager.FindByEmailAsync(usuario.Email) == null)
                {
                    var resultado = await userManager.CreateAsync(usu, usuario.Senha);

                    if (resultado.Succeeded)
                    {
                        await signInManager.SignInAsync(usu, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro ao criar novo usuario");
                        return View(usuario);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Não foi possível criar novo usuário.");
                    return View(usuario);
                }
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            UsuarioModel usuario = await userManager.FindByIdAsync(userManager.GetUserId(User));

            var resultado = await userManager.DeleteAsync(usuario);
            if (resultado.Succeeded)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao deletar conta");
                return RedirectToAction("Dashboard", "Profile");
            }
        }


    }
}