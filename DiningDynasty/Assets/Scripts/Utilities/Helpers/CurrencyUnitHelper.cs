namespace Utilities.Helpers
{
    public static class CurrencyUnitHelper
    {
        public static string IntToK(int cost)
        {
            var costString = "";
            if (cost >= 1000000)
            {
                costString = ((float)cost / 1000000).ToString("0.0").Replace(",", ".") + "m";
            }
            else if (cost >= 1000)
            {
                costString = ((float)cost / 1000).ToString("0.0").Replace(",", ".") + "k";
            }
            else
            {
                costString = cost.ToString("0");
            }

            return costString;
        }

        public static string IntToKDollar(int cost)
        {
            var key = IntToK(cost);
            return  "$" + key;
        }

        public static string FloatToK(float cost)
        {
            var costString = "";
            if (cost >= 1000000)
            {
                costString = (cost / 1000000).ToString("0.0").Replace(",", ".") + "m";
            }
            else if (cost >= 1000)
            {
                costString = (cost / 1000).ToString("0.0").Replace(",", ".") + "k";
            }
            else
            {
                costString = cost.ToString("0");
            }

            return costString;
        }
        
        public static string FloatToKDollar(float cost)
        {
            return "$" + FloatToK(cost);
        }

    }
}