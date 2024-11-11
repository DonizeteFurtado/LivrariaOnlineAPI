using LivrariaOnlineAPI.Data;
using LivrariaOnlineAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult CriarLivro(LivroModel livro)
    {
        _context.Livros.Add(livro);
        _context.SaveChangesAsync();

        return Ok(_context.Livros);
    }

    [HttpGet]
    public IActionResult VerLivro()
    {
        return Ok(_context.Livros);
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
