using System.Text;

namespace WebAppCoreMVC.Utility
{
    public class Utilities
    {
        // method for First Letter in UpperCase
        public static string EveryFirstCharacterCapital(string input)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(input))
            {
                var data = input.Split(' ');
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].First().ToString().ToUpper() + data[i].Substring(1) + " ");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}
