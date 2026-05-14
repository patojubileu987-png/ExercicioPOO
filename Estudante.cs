namespace ProgramaClubeLeitura;

public class Estudante
{
    public string Nome { get; private set; }

    public Estudante(string nome)
    {
        Nome = Validacao.TextoObrigatorio(nome, nameof(nome));
    }

    public void AlterarNome(string novoNome)
    {
        Nome = Validacao.TextoObrigatorio(novoNome, nameof(novoNome));
    }
}
