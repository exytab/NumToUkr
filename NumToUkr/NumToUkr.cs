using System;
using System.Text;

namespace Exytab.Ukrainian
{
    /// <summary>
    /// Клас перетворює числове значення суми в прописне.
    /// </summary>
    public static class NumToUkr
    {
        /// <summary>
        /// Переводить <paramref name="val"/> в прописне значення
        /// </summary>
        /// <example>
        /// NumToUkr.FromString("  987654321,3  ") // "дев'ятсот вісімдесят сім мільйонів шістсот п'ятдесят чотири тисячі триста двадцять одна гривня 30 копійок"
        /// </example>
        public static String FromString(string val)
        {
            StringBuilder result = new StringBuilder();
            val = val.Trim().Replace(" ", string.Empty);
            if (val.IndexOf(",") != -1)
            {
                val += "00";
                int floats = Convert.ToInt32(val.Substring(val.IndexOf(",") + 1, 2));
                int integers = 0;
                if (val.IndexOf(",") == 0) 
                { 
                    integers = 0; 
                }
                else
                {
                    integers = Convert.ToInt32(val.Substring(0, val.IndexOf(",")));
                }
                result.Append(Dollars(integers)).Append(Cents(floats));
            }
            else if (val.IndexOf(".") != -1)
            {
                val += "00";
                int floats = Convert.ToInt32(val.Substring(val.IndexOf(".") + 1, 2));
                int integers = 0;
                if (val.IndexOf(".") == 0)
                {
                    integers = 0;
                }
                else
                {
                    integers = Convert.ToInt32(val.Substring(0, val.IndexOf(".")));
                }
                result.Append(Dollars(integers)).Append(Cents(floats));
            }
            else
            {
                result.Append(Dollars(Convert.ToInt32(val))).Append(Cents(0));
            }
            return result.ToString();
        }

        #region
        
        private static StringBuilder Dollars(int val)
        {
            StringBuilder result = new StringBuilder();
            int o = val % 1000;
            int t = (val % 1000000 - o) / 1000;
            int m = (val % 1000000000 - t * 1000 - o) / 1000000;
            if (m != 0)
            {
                result.Append(Million(m));
            }
            if (t != 0)
            {
                result.Append(Thousand(t));
            }
            result.Append(Unit(o, val));
            return result;
        }
        
        private static StringBuilder Cents(int val)
        {
            StringBuilder result = new StringBuilder("");
            int last = val % 10;
            int first = (val - last) / 10;
            if (val < 10)
            {
                result.Append("0");
            }
            result.Append(val.ToString()).Append(" ");
            if (last == 0)
            {
                result.Append("копійок");
            }
            else if (last == 1 && first != 1)
            {
                result.Append("копійка");
            }
            else if (last <= 4 && last >= 2 && first != 1)
            {
                result.Append("копійки");
            }
            else if (last == 1 && first == 1)
            {
                result.Append("копійок");
            }
            else if (last <= 4 && last >= 2 && first == 1)
            {
                result.Append("копійок");
            }
            else if (last >= 5 && last <= 9)
            {
                result.Append("копійок");
            }
            return result;
        }

        private static StringBuilder Unit(int val, int integers)
        {
            StringBuilder result = new StringBuilder();
            int uni = val % 10;
            int ten = (val % 100 - uni) / 10;
            int hun = (val - ten * 10 - uni) / 100;
            if (hun != 0)
            {
                result.Append(Hundred(hun));
            }
            if (ten == 1)
            {
                result.Append(Ten_b(uni));
                result.Append("гривень ");
            }
            else
            {
                if (ten != 0)
                {
                    result.Append(Ten_a(ten));
                }
                result.Append(Letters(uni, 0));
                if (uni == 1)
                {
                    result.Append("гривня ");
                }
                else if (uni > 1 && uni < 5)
                {
                    result.Append("гривні ");
                }
                else if (uni > 4 && uni < 10)
                {
                    result.Append("гривень ");
                }
                else if (uni == 0)
                {
                    if (integers == 0) result.Append("нуль ");
                    result.Append("гривень ");
                }
            }
            return result;
        }

        private static StringBuilder Thousand(int val)
        {
            StringBuilder result = new StringBuilder();
            int uni = val % 10;
            int ten = (val % 100 - uni) / 10;
            int hun = (val - ten * 10 - uni) / 100;
            if (hun != 0)
            {
                result.Append(Hundred(hun));
            }
            if (ten == 1)
            {
                result.Append(Ten_b(uni));
                result.Append("тисяч ");
            }
            else
            {
                if (ten != 0)
                {
                    result.Append(Ten_a(ten));
                }
                result.Append(Letters(uni, 0));
                if (uni == 1)
                {
                    result.Append("тисяча ");
                }
                if (uni > 1 && uni < 5)
                {
                    result.Append("тисячі ");
                }
                if (uni > 4 && uni < 10)
                {
                    result.Append("тисяч ");
                }
                if (uni == 0)
                {
                    result.Append("тисяч ");
                }
            }
            return result;
        }

        private static StringBuilder Million(int val)
        {
            StringBuilder result = new StringBuilder();
            int uni = val % 10;
            int ten = (val % 100 - uni) / 10;
            int hun = (val - ten * 10 - uni) / 100;

            if (hun != 0)
            {
                result.Append(Hundred(hun));
            }

            if (ten == 1)
            {
                result.Append(Ten_b(uni));
                result.Append("мільйонів ");
            }
            else
            {
                if (ten != 0)
                {
                    result.Append(Ten_a(ten));
                }
                result.Append(Letters(uni, 1));
                if (uni == 1)
                {
                    result.Append("мільйон ");
                }
                if (uni > 1 && uni < 5)
                {
                    result.Append("мільйони ");
                }
                if (uni > 4 && uni < 10)
                {
                    result.Append("мільйонів ");
                }
                if (uni == 0)
                {
                    result.Append("мільйонів ");
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val">from 0 to 9</param>
        /// <param name="flag">1 (for Million) or 0 (for Other)</param>
        /// <returns></returns>
        private static StringBuilder Letters(int val, int flag)
        {
            StringBuilder result = new StringBuilder();
            switch (val)
            {
                case 1:
                    if (flag == 1)
                    {
                        result.Append("один ");
                    }
                    else
                    {
                        result.Append("одна ");
                    }
                    break;
                case 2:
                    if (flag == 1)
                    {
                        result.Append("два ");
                    }
                    else
                    {
                        result.Append("дві ");
                    }
                    break;
                case 3:
                    result.Append("три ");
                    break;
                case 4:
                    result.Append("чотири ");
                    break;
                case 5:
                    result.Append("п'ять ");
                    break;
                case 6:
                    result.Append("шість ");
                    break;
                case 7:
                    result.Append("сім ");
                    break;
                case 8:
                    result.Append("вісім ");
                    break;
                case 9:
                    result.Append("дев'ять ");
                    break;
                case 0:
                    result.Append("");
                    break;
            }
            return result;
        }

        private static StringBuilder Ten_b(int val)
        {
            StringBuilder result = new StringBuilder();
            switch (val)
            {
                case 1:
                    result.Append("одинадцять ");
                    break;
                case 2:
                    result.Append("дванадцять ");
                    break;
                case 3:
                    result.Append("тринадцять ");
                    break;
                case 4:
                    result.Append("чотирнадцять ");
                    break;
                case 5:
                    result.Append("п'ятнадцять ");
                    break;
                case 6:
                    result.Append("шістнадцять ");
                    break;
                case 7:
                    result.Append("сімнадцять ");
                    break;
                case 8:
                    result.Append("вісімнадцять ");
                    break;
                case 9:
                    result.Append("дев'ятнадцять ");
                    break;
                case 0:
                    result.Append("десять ");
                    break;
            }
            return result;
        }

        private static StringBuilder Ten_a(int val)
        {
            StringBuilder result = new StringBuilder();
            switch (val)
            {
                case 1:
                    result.Append("десять ");
                    break;
                case 2:
                    result.Append("двадцять ");
                    break;
                case 3:
                    result.Append("тридцять ");
                    break;
                case 4:
                    result.Append("сорок ");
                    break;
                case 5:
                    result.Append("п'ятдесят ");
                    break;
                case 6:
                    result.Append("шістдесят ");
                    break;
                case 7:
                    result.Append("сімдесят ");
                    break;
                case 8:
                    result.Append("вісімдесят ");
                    break;
                case 9:
                    result.Append("дев'яносто ");
                    break;
                case 0:
                    result.Append("");
                    break;
            }
            return result;
        }

        private static StringBuilder Hundred(int val)
        {
            StringBuilder result = new StringBuilder();
            switch (val)
            {
                case 1:
                    result.Append("сто ");
                    break;
                case 2:
                    result.Append("двісті ");
                    break;
                case 3:
                    result.Append("триста ");
                    break;
                case 4:
                    result.Append("чотириста ");
                    break;
                case 5:
                    result.Append("п'ятсот ");
                    break;
                case 6:
                    result.Append("шістсот ");
                    break;
                case 7:
                    result.Append("сімсот ");
                    break;
                case 8:
                    result.Append("вісімсот ");
                    break;
                case 9:
                    result.Append("дев'ятсот ");
                    break;
                case 0:
                    result.Append("");
                    break;
            }

            return result;
        }

        #endregion
    }
}
