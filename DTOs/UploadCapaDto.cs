using Microsoft.AspNetCore.Http;

namespace jogos.DTOs
{
    public class UploadCapaDto
    {
        public IFormFile Capa { get; set; } = default!;
    }
}
