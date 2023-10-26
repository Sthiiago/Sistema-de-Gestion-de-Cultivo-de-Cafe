using SistemaParcelas.Logging;

namespace SistemaParcelas.Services.Contract
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario usuario);

    }
}
