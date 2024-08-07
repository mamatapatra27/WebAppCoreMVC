using System.Text;

namespace WebAppCoreMVC.Utility
{
    public class Utilities
    {
        // method for First Letter in UpperCase
        public static string EveryFirstCharacterCapital(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            StringBuilder sb = new StringBuilder();

            var data = input.Split(' ');
            for(int i=0; i<data.Length; i++)
            {
                if (!string.IsNullOrEmpty(data[i]))
                {
                    sb.Append(data[i].First().ToString().ToUpper() + data[i].Substring(1));
                }
                if(i < data.Length - 1)
                {
                    sb.Append(" ");
                }
                //sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}
