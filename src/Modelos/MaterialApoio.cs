namespace ProgramaClubeLeitura;

public record MaterialApoio
{
    public string Descricao { get; init; }

    public MaterialApoio(string descricao)
    {
        Descricao = Validacao.TextoObrigatorio(descricao, nameof(descricao));
    }
}
