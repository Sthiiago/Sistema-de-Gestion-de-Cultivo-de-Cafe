using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SistemaParcelas.Logging;
using SistemaParcelas.Resources;
using SistemaParcelas.Services.Contract;
using System.Security.Claims;

namespace SistemaParcelas.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public InicioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario usuario)
        {
            usuario.Clave = Utils.EncryptPassword(usuario.Clave);

            Usuario createdUser = await _usuarioService.SaveUsuario(usuario);

            if (createdUser.IdUsuario > 0)
                return RedirectToAction("IniciarSesion","Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";

            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario foundUser = await _usuarioService.GetUsuario(correo, Utils.EncryptPassword(clave));

            if (foundUser == null)
            {
                ViewData["Mensaje"] = "No se pudo encontrar el usuario";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,foundUser.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh=true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
