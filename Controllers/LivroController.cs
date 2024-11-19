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

    [HttpPut]
    public IActionResult AtualizaLivro()
    {
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult DeletaLivro()
    {
        return BadRequest();
    }
}
