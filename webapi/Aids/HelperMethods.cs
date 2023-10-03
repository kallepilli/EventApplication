namespace webapi.Aids
{
    public class HelperMethods
    {
        public bool ValidateIdCode(string idCode)
        {
            if (string.IsNullOrWhiteSpace(idCode) || idCode.Length != 11)
                return false;

            if (!idCode.All(char.IsDigit))
                return false;

            int[] firstWeights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int[] secondWeights = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };

            int sumI = 0;
            int sumII = 0;

            for (int i = 0; i < 10; i++)
            {
                sumI += (idCode[i] - '0') * firstWeights[i];
                sumII += (idCode[i] - '0') * secondWeights[i];
            }

            int remainderI = sumI % 11;
            int controlNumber;

            if (remainderI != 10)
            {
                controlNumber = remainderI;
            }
            else
            {
                int remainderII = sumII % 11;
                controlNumber = (remainderII != 10) ? remainderII : 0;
            }

            int firstDigit = idCode[0] - '0';
            int lastDigit = idCode[10] - '0';

            switch (firstDigit)
            {
                case 3:
                case 4:
                    break;
                case 5:
                case 6:
                    break;
                case 7:
                case 8:
                    break;
                default:
                    return false;
            }

            return lastDigit == controlNumber;
        }


    }
}
