namespace LivrariaOnlineAPI.Models;

public class LivroModel
{
    public int Id {  get; set; }
    public string Titulo { get; set; }
    public string Autor {  get; set; }
    public decimal Preco { get; set; }  
    public int QtdEstoque { get; set; }
    
    public int GeneroId { get; set; }
    public GeneroModel? Genero { get; set; }
}
