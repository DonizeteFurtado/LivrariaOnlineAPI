namespace LivrariaOnlineAPI.Comunicacao.Request;

public class RequestLivro
{
    public required string Id {  get; set; }
    public required string Titulo { get; set; }
    public required string Autor {  get; set; }
    public required string Genero { get; set; }
    public required decimal Preco { get; set; }  
    public required int QtdEstoque { get; set; }

}
