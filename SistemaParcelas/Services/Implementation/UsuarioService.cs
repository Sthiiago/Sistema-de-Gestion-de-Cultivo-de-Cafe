using Microsoft.EntityFrameworkCore;
using SistemaParcelas.Logging;
using SistemaParcelas.Services.Contract;

namespace SistemaParcelas.Services.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AccesoGestionContext _dbContext;


        public UsuarioService(AccesoGestionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario foundUser = await _dbContext.Usuarios.Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefaultAsync();
            return foundUser;
        }

        public async Task<Usuario> SaveUsuario(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
    }
}
