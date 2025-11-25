using jogos.Data;
using jogos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jogos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacoesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AvaliacoesController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult> PostAvaliacao(Avaliacao a)
        {
            var existente = await _context.Avaliacoes
                .FirstOrDefaultAsync(x => x.UsuarioId == a.UsuarioId && x.JogoId == a.JogoId);

            if (existente != null)
            {
                existente.Nota = a.Nota;
                existente.Comentario = a.Comentario;
                existente.Data = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            _context.Avaliacoes.Add(a);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAvaliacao), new { id = a.Id }, a);
        }

        [HttpGet("jogo/{jogoId}")]
        public async Task<ActionResult> GetAvaliacoesDoJogo(int jogoId)
        {
            var avals = await _context.Avaliacoes
                .Where(x => x.JogoId == jogoId)
                .Include(x => x.Usuario)
                .ToListAsync();

            var media = avals.Any() ? avals.Average(x => x.Nota) : 0;
            return Ok(new { media, total = avals.Count, avaliacoes = avals });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(int id)
        {
            var a = await _context.Avaliacoes.FindAsync(id);
            if (a == null) return NotFound();
            return a;
        }
    }
}
