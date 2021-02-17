namespace EventFinder.Domain.Core.Utils
{
    public class StringHelper
    {

        public static string RemoverCaracteresEspeciais(string param)
        {
            return param.Replace("-", "")
                .Replace("/", "")
                .Replace("_", "")
                .Replace(")", "")
                .Replace("(", "")
                .Replace(".", "")
                .Replace(" ", "")
                .Trim();
        }


    }
}
