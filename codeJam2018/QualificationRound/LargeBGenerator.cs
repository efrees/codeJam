using System.Text;

namespace QualificationRound
{
    public class LargeBGenerator
    {
        public string GetListOfNumbersAsString(int listSize = 100000)
        {
            var stringBuilder = new StringBuilder(listSize*4);

            for (int i = 0; i < listSize; i++)
            {
                stringBuilder.Append(i);
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
