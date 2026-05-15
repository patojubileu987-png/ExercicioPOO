namespace ProgramaClubeLeitura;

public class Professor
{
    public string Nome { get; private set; }

    public Professor(string nome)
    {
        Nome = Validacao.TextoObrigatorio(nome, nameof(nome));
    }

    public void AlterarNome(string novoNome)
    {
        Nome = Validacao.TextoObrigatorio(novoNome, nameof(novoNome));
    }
}
