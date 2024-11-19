using LivrariaOnlineAPI.Data;
using LivrariaOnlineAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace LivrariaOnlineAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LivroController : ControllerBase
{
    private readonly DataContext _context;
    public LivroController(DataContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> CriarLivro(LivroModel livro)
    {
        await _context.Livros.AddAsync(livro);
        await _context.SaveChangesAsync();

        return Ok(await _context.Livros.ToListAsync());
    }

    [HttpGet]
    public IActionResult VerLivro()
    {
        var livrosDto = _context.Livros
            .Include(l => l.Genero)
            .Select(l => new LivroModel()
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                QtdEstoque = l.QtdEstoque,
                GeneroId = l.GeneroId,
                Preco = l.Preco,
                Genero = new GeneroModel()
                {
                    IdGenero = l.Genero.IdGenero,
                    Nome = l.Genero.Nome
                }
            })
            .ToList();

        return Ok(livrosDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizaLivro(int id, [FromBody] LivroModel livroAtualizado)
    {
        if (id != livroAtualizado.Id)
        {
            return BadRequest("O ID do livro na URL e no corpo não coincidem.");
        }

        var LivroExistente = await _context.Livros
            .Include(l => l.Genero)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (LivroExistente == null)
        {
            return NotFound("Livro não encontrado");
        }
        
        LivroExistente.Titulo = livroAtualizado.Titulo;
        LivroExistente.Autor = livroAtualizado.Autor;
        LivroExistente.GeneroId = livroAtualizado.GeneroId;
        LivroExistente.QtdEstoque = livroAtualizado.QtdEstoque;
        LivroExistente.Preco = livroAtualizado.Preco;
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>DeletaLivro(int id)
    {
        var livro = await _context.Livros
            .Include(l => l.Genero)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (livro == null)
        {
            return NotFound("Livro não encontrado.");
        }

        _context.Livros.Remove(livro);
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("genero{id}")]
    public async Task<IActionResult> DeletaGenero(int id)
    {
        var genero = await _context.Generos
            .FirstOrDefaultAsync(g => g.IdGenero == id);

        if (genero == null)
        {
            return NotFound("Gênero não encontrado.");
        }

        var livroUsandoGenero = await _context.Livros
            .Where(l => l.GeneroId == id)
            .ToListAsync();

        if (livroUsandoGenero.Any())
        {
            return BadRequest("\"Não é possível excluir um gênero que está sendo utilizado em livros.\"");
        }
        
        _context.Generos.Remove(genero);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
