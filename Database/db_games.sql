create database db_games;
use db_games;


CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome LONGTEXT NOT NULL,
    Email LONGTEXT NOT NULL,
    Senha LONGTEXT NOT NULL
);

CREATE TABLE Jogos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome LONGTEXT NOT NULL,
    Genero LONGTEXT NOT NULL,
    Plataforma LONGTEXT NOT NULL,
    Descricao LONGTEXT NOT NULL,
    Nota DOUBLE NOT NULL,
    Valor DECIMAL(65,30) NOT NULL, 
    CapaUrl LONGTEXT NOT NULL,    
    UsuarioId INT NOT NULL,   
    
    CONSTRAINT FK_Jogos_Usuarios_UsuarioId 
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios (Id) ON DELETE CASCADE
);


USE db_games;

UPDATE Jogos SET CapaUrl = '/capas/ac4.png' WHERE Nome LIKE '%Black Flag%';
UPDATE Jogos SET CapaUrl = '/capas/Black Myth Wukong.png' WHERE Nome LIKE '%Wukong%';
UPDATE Jogos SET CapaUrl = '/capas/Call of Juarez Gunslinger.png' WHERE Nome LIKE '%Juarez%';
UPDATE Jogos SET CapaUrl = '/capas/Dark Souls 2.png' WHERE Nome LIKE '%Dark Souls 2%';
UPDATE Jogos SET CapaUrl = '/capas/Elden Ring.png' WHERE Nome LIKE '%Elden Ring%';
UPDATE Jogos SET CapaUrl = '/capas/Fabledom.png' WHERE Nome LIKE '%Fabledom%';
UPDATE Jogos SET CapaUrl = '/capas/Ghost of Tsushima.png' WHERE Nome LIKE '%Tsushima%';
UPDATE Jogos SET CapaUrl = '/capas/It Takes Two.png' WHERE Nome LIKE '%It Takes Two%';
UPDATE Jogos SET CapaUrl = '/capas/Lies of P.png' WHERE Nome LIKE '%Lies of P%';
UPDATE Jogos SET CapaUrl = '/capas/Monster Hunter Wilds.png' WHERE Nome LIKE '%Wilds%';
UPDATE Jogos SET CapaUrl = '/capas/Monster Hunter World.png' WHERE Nome LIKE '%Monster Hunter: World%';
UPDATE Jogos SET CapaUrl = '/capas/Palworld.png' WHERE Nome LIKE '%Palworld%';
UPDATE Jogos SET CapaUrl = '/capas/Resident Evil 4 Remake.png' WHERE Nome LIKE '%Resident Evil 4%';
UPDATE Jogos SET CapaUrl = '/capas/Resident Evil Village.png' WHERE Nome LIKE '%Village%';
UPDATE Jogos SET CapaUrl = '/capas/Sekiro Shadows Die Twice.png' WHERE Nome LIKE '%Sekiro%';
UPDATE Jogos SET CapaUrl = '/capas/Sons of the Forest.png' WHERE Nome LIKE '%Sons of the Forest%';
UPDATE Jogos SET CapaUrl = '/capas/Star Wars Battlefront 2.png' WHERE Nome LIKE '%Battlefront%';
UPDATE Jogos SET CapaUrl = '/capas/Stardew Valley.png' WHERE Nome LIKE '%Stardew%';
UPDATE Jogos SET CapaUrl = '/capas/Tales of Arise.png' WHERE Nome LIKE '%Tales of Arise%';
UPDATE Jogos SET CapaUrl = '/capas/Where Winds Meet.png' WHERE Nome LIKE '%Winds Meet%';
