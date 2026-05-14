namespace ProgramaClubeLeitura;

public class EncontroLeitura
{
    private readonly List<ContribuicaoLeitura> _contribuicoes = new();

    public string Tema { get; init; }
    public string Descricao { get; init; }
    public bool Obrigatorio { get; init; }
    public IReadOnlyCollection<ContribuicaoLeitura> Contribuicoes => _contribuicoes.AsReadOnly();

    public EncontroLeitura(string tema, bool obrigatorio, string descricao)
    {
        Tema = Validacao.TextoObrigatorio(tema, nameof(tema));
        Obrigatorio = obrigatorio;
        Descricao = Validacao.TextoObrigatorio(descricao, nameof(descricao));
    }

    public void AdicionarContribuicao(ContribuicaoLeitura contribuicao)
    {
        if (contribuicao is null)
        {
            throw new ArgumentNullException(nameof(contribuicao));
        }

        _contribuicoes.Add(contribuicao);
    }
}
