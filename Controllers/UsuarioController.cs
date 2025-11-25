using jogos.Data;
using jogos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jogos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> Registrar([FromBody] Usuario usuario)
        {
            // Em ambiente real: validar email único, hash de senha etc.
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) return NotFound();
            return u;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] Usuario login)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

            if (user == null) return Unauthorized("Login inválido.");
            // Remova senha no retorno para segurança:
            user.Senha = string.Empty;
            return user;
        }

        [HttpGet("{id}/jogos")]
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogosDoUsuario(int id)
        {
            return await _context.Jogos.Where(j => j.UsuarioId == id).ToListAsync();
        }
    }
}
