namespace ProgramaClubeLeitura;

public class ObraLiteraria
{
    public string Titulo { get; init; }

    public ObraLiteraria(string titulo)
    {
        Titulo = Validacao.TextoObrigatorio(titulo, nameof(titulo));
    }
}
