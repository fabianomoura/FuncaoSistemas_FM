namespace Infra_FM.BLL
{
    public static class Util
    {
        public static string FormataCPF(string Cpf)
        {
            string valor = Cpf;
            valor = valor.Replace(".", "");
            valor = valor.Replace("-", "");
            return valor;
        }
    }
}