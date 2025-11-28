using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jogos.Data;
using jogos.Models;
using jogos.DTOs;
using jogos.Services;

namespace jogos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UploadService _uploadService;

        public JogosController(AppDbContext context, UploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        // GET api/jogos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogos()
        {
            return await _context.Jogos.Include(j => j.Usuario).ToListAsync();
        }

        // GET api/jogos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jogo>> GetJogo(int id)
        {
            var jogo = await _context.Jogos.Include(j => j.Usuario).FirstOrDefaultAsync(j => j.Id == id);
            if (jogo == null) return NotFound();
            return jogo;
        }

        // POST api/jogos
        [HttpPost]
        public async Task<ActionResult<Jogo>> PostJogo([FromBody] JogoDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null) return BadRequest("UsuarioId inválido.");

            var jogo = new Jogo
            {
                Nome = dto.Nome,
                Genero = dto.Genero,
                Plataforma = dto.Plataforma,
                Descricao = dto.Descricao,
                Nota = dto.Nota,
                Valor = dto.Valor,
                UsuarioId = dto.UsuarioId
            };

            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJogo), new { id = jogo.Id }, jogo);
        }

        // PUT api/jogos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogo(int id, [FromBody] JogoDto dto)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();

            jogo.Nome = dto.Nome;
            jogo.Genero = dto.Genero;
            jogo.Plataforma = dto.Plataforma;
            jogo.Descricao = dto.Descricao;
            jogo.Nota = dto.Nota;
            jogo.Valor = dto.Valor;
            jogo.UsuarioId = dto.UsuarioId;

            _context.Entry(jogo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/jogos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogo(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST api/jogos/{id}/upload-capa
        [HttpPost("{id}/upload-capa")]
        public async Task<IActionResult> UploadCapa(int id, [FromForm] UploadCapaDto dto)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();

            if (dto.Capa == null) return BadRequest("Arquivo não enviado.");

            var caminho = await _uploadService.SaveCapaAsync(dto.Capa);
            jogo.CapaUrl = caminho;
            await _context.SaveChangesAsync();

            return Ok(new { capa = caminho });
        }
    }
}
