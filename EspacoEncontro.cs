namespace ProgramaClubeLeitura;

public class EspacoEncontro
{
    public string Identificacao { get; init; }

    public EspacoEncontro(string identificacao)
    {
        Identificacao = Validacao.TextoObrigatorio(identificacao, nameof(identificacao));
    }
}
