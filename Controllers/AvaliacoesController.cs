using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jogos.Data;
using jogos.Models;

namespace jogos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacaoController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/avaliacao
        [HttpPost]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            return Ok(avaliacao);
        }

        // GET /api/avaliacao/jogo/5
        [HttpGet("jogo/{jogoId}")]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> GetAvaliacoesPorJogo(int jogoId)
        {
            return await _context.Avaliacoes
                .Where(a => a.JogoId == jogoId)
                .Include(a => a.Usuario)
                .ToListAsync();
        }
    }
}
