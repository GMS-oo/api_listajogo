using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jogos.Data;
using jogos.Models;

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

        // GET lista todos os jogos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogos()
        {
            return await _context.Jogos.Include(j => j.Avaliacoes).ToListAsync();
        }

        // GET um jogo específico
        [HttpGet("{id}")]
        public async Task<ActionResult<Jogo>> GetJogo(int id)
        {
            var jogo = await _context.Jogos
                .Include(j => j.Avaliacoes)
                .ThenInclude(a => a.Usuario)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jogo == null) return NotFound();
            return jogo;
        }

        // POST criar jogo
        [HttpPost]
        public async Task<ActionResult<Jogo>> PostJogo(Jogo jogo)
        {
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJogo), new { id = jogo.Id }, jogo);
        }

        // POST upload da capa
        [HttpPost("upload-capa")]
        public async Task<IActionResult> UploadCapa(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Arquivo inválido");

            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/capas");

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var caminhoArquivo = Path.Combine(pasta, arquivo.FileName);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var url = $"/capas/{arquivo.FileName}";
            return Ok(new { url });
        }
    }
}
