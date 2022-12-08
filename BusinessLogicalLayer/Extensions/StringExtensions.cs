using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Extensions
{
    //Regras para criar métodos de extensão
    //1 - Classe deve ser static
    //2 - Método deve ser static
    //3 - Primeiro parâmetro do método deve possuir a palavra "this" para "extender" os métodos de instância
    //do objeto que queremos aplicar este comportamento

    static class StringExtensions
    {
        public static string Normatize(this string name)
        {
            name = name.Trim();
            name = Regex.Replace(name, @"\s+", " ");
            TextInfo textInfo = new CultureInfo("pt-br", false).TextInfo;
            return textInfo.ToTitleCase(name);
        }

        public static string RemoveMask(this string field)
        {
            //Pode ser feito tbm por regex ou foreach!
            return field.Replace(".", "")
                         .Replace(",", "")
                         .Replace("-", "")
                         .Replace("(", "")
                         .Replace(")", "")
                         .Replace("_", "");
        }


        public static bool IsValidCPF(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

    }
}
