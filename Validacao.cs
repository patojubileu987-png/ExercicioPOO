namespace ProgramaClubeLeitura;

internal static class Validacao
{
    public static string TextoObrigatorio(string? valor, string nomeCampo)
    {
        if (valor is null)
        {
            throw new ArgumentNullException(nomeCampo, $"O campo '{nomeCampo}' não pode ser nulo.");
        }

        var texto = valor.Trim();
        if (texto.Length == 0)
        {
            throw new ArgumentException($"O campo '{nomeCampo}' não pode estar vazio ou conter apenas espaços.", nomeCampo);
        }

        return texto;
    }
}
