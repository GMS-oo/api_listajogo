using jogos.Data;
using jogos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jogos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JogosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogos()
        {
            return await _context.Jogos
                .Include(j => j.Avaliacoes)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Jogo>> GetJogo(int id)
        {
            var jogo = await _context.Jogos
                .Include(j => j.Avaliacoes)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jogo == null) return NotFound();
            return jogo;
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogosDoUsuario(int usuarioId)
        {
            return await _context.Jogos
                .Where(j => j.UsuarioId == usuarioId)
                .Include(j => j.Avaliacoes)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Jogo>> PostJogo(Jogo jogo)
        {
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJogo), new { id = jogo.Id }, jogo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogo(int id, Jogo jogo)
        {
            if (id != jogo.Id) return BadRequest();
            _context.Entry(jogo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogo(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();
            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
