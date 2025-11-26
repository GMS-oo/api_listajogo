using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jogos.Data;
using jogos.Models;

namespace jogos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // DTO de entrada para login
        public class LoginDto
        {
            public string Email { get; set; } = string.Empty;
            public string Senha { get; set; } = string.Empty;
        }

        // ============================
        // Registrar usuário
        // POST: api/usuario/registrar
        // ============================
        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> Registrar([FromBody] Usuario usuario)
        {
            // validações básicas
            if (string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                return BadRequest("Email e senha são obrigatórios.");

            // verificar email existente
            var existente = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == usuario.Email);

            if (existente != null)
                return Conflict("Este email já está cadastrado.");

            // OBS: em produção, hash da senha (BCrypt). Aqui usamos plain text só para a disciplina.
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // esconde a senha no retorno
            usuario.Senha = string.Empty;

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // ============================
        // Login
        // POST: api/usuario/login
        // Corpo JSON: { "email": "...", "senha": "..." }
        // ============================
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginDto login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return BadRequest("Email e senha são obrigatórios.");

            var user = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

            if (user == null)
                return Unauthorized("Email ou senha incorretos.");

            // Não retorne a senha para o cliente
            user.Senha = string.Empty;
            return Ok(user);
        }

        // ============================
        // Pegar usuário por id
        // GET: api/usuario/{id}
        // ============================
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null) return NotFound();
            usuario.Senha = string.Empty;
            return usuario;
        }

        // ============================
        // Jogos do usuário
        // GET: api/usuario/{id}/jogos
        // ============================
        [HttpGet("{id:int}/jogos")]
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogosDoUsuario(int id)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Id == id);
            if (!existe) return NotFound("Usuário não encontrado.");

            var jogos = await _context.Jogos
                .Where(j => j.UsuarioId == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(jogos);
        }

        // ============================
        // Avaliações do usuário
        // GET: api/usuario/{id}/avaliacoes
        // ============================
        [HttpGet("{id:int}/avaliacoes")]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> GetAvaliacoesDoUsuario(int id)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Id == id);
            if (!existe) return NotFound("Usuário não encontrado.");

            var avals = await _context.Avaliacoes
                .Where(a => a.UsuarioId == id)
                .Include(a => a.Jogo) // opcional: incluir nome do jogo
                .AsNoTracking()
                .ToListAsync();

            return Ok(avals);
        }
    }
}
