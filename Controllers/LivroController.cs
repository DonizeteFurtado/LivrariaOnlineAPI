using LivrariaOnlineAPI.Comunicacao.Request;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaOnlineAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LivroController : ControllerBase
{
    [HttpPost]
    public IActionResult CriarLivro()
    {
        //var Response = new RequestLivro();

        return Ok();
    }

    [HttpGet]
    public IActionResult VerLivro()
    {
        return Ok();
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
