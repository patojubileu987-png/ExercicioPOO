namespace ProgramaClubeLeitura;

public record ContribuicaoLeitura
{
    public string Texto { get; init; }

    public ContribuicaoLeitura(string texto)
    {
        Texto = Validacao.TextoObrigatorio(texto, nameof(texto));
    }
}
