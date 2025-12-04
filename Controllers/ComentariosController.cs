using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jogos.Data;
using jogos.Models;

namespace jogos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComentariosController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("jogo/{jogoId}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosDoJogo(int jogoId)
        {
            return await _context.Comentarios
                .Include(c => c.Usuario)
                .Where(c => c.JogoId == jogoId)
                .OrderByDescending(c => c.DataComentario)
                .ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            comentario.DataComentario = DateTime.Now;

           
            var usuario = await _context.Usuarios.FindAsync(comentario.UsuarioId);
            var jogo = await _context.Jogos.FindAsync(comentario.JogoId);

            if (usuario == null || jogo == null) return BadRequest("Usuário ou Jogo inválido.");

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return Ok(comentario);
        }
    }
}