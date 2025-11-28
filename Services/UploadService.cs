using Microsoft.AspNetCore.Hosting;

namespace jogos.Services
{
    public class UploadService
    {
        private readonly IWebHostEnvironment _env;
        public UploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveCapaAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) 
                throw new ArgumentException("Arquivo inválido.");

            var folder = Path.Combine(_env.WebRootPath ?? "wwwroot", "capas");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            // retorna caminho relativo para ser salvo no banco
            return $"/capas/{fileName}";
        }
    }
}

