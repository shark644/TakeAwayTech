using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TakeAwayTech.Classes
{
    public class AmountParser : IAmountParser
    {
        private bool isStarted;
        private bool doTrim;
        const string currencyPrimary = " DOLLARS";
        const string currencySecondary = " CENTS";
        private readonly List<string> _textsLevelZero =

        new List<string>
        {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE","TEN",
            "ELEVEN","TWELVE","THIRTEEN","FOURTEEN","FIFTEEN",
            "SIXTEEN","SEVENTEEN","EIGHTEEN","NINETEEN"
        };
        private readonly List<string> _textsLevelOne =

        new List<string>
        {
            "", "","TWENTY","THIRTY","FORTY","FIFTY","SIXTY","SEVENTY","EIGHTY","NINETY"
        };

        private readonly List<string> _textsLevelTwo =

               new List<string>
               {
            "HUNDRED","THOUSAND", "MILLION"
               };

        private readonly List<int> _units =
            new List<int>
            {
                1,1000,1000000
            };

        /// <summary>
        /// Get amount in words
        /// </summary>
        /// <param name="amount">The Amount</param>
        /// <returns></returns>
        public string GetAmountInWords(double amount)
        {
            //Reset Values
            isStarted = false;
            doTrim = true;

            //rount to 2 decimal places
            amount = Math.Round(amount, 2);

            //Get dollars
            int dollars = (int)amount;

            //Get cents

            int cents = 0;
            var amountString = amount.ToString();
            if (amountString.Contains('.'))
            {
                string centsAmount = amountString.Split('.')[1];
                cents = Convert.ToInt16(centsAmount);
            }
            //process amount for words
            string text = ((dollars > 0) ? (DecimalToWords(dollars) + currencyPrimary) : "") + ((cents <= 0) ? "" : NumberToText(cents, -1) + currencySecondary);

            return text;
        }

        public string DecimalToWords(int number)
        {
            var text = new System.Text.StringBuilder();
            //Unit 2: Thousands
            text.Append(ProcessUnit(2, ref number, true));

            if (number > 0)
                text.Append(ProcessUnit(1, ref number));
            if (number > 0)
                text.Append(ProcessUnit(0, ref number));
            //{
            //    //Unit 1: Hundreds
            //    var num3 = number % 1000;
            //    text.Append(NumberToText(num3, 1, true));
            //    var num4 = number / 1000;
            //    if (num4 > 0)
            //        text.Append(NumberToText(num4, 1));
            //}
            return text.ToString();
        }

        private string ProcessUnit(int unit, ref int number, bool isStart = false)
        {
            string text = "";
            var num1 = number / _units[unit];
            if (num1 > 0)
                text = NumberToText(num1, unit, isStart);
            number = number % _units[unit];

            return text;
        }

        public string NumberToText(int number, int unit, bool isStart = false)
        {
            if (number <= 0)
                return string.Empty;

            //Separater
            string text = "";
            if (isStarted)
            {
                text += " AND";
            }
            isStarted = true;

            //first digit of 3 digits
            if (number > 100)
            {
                var num1 = number / 100;
                if (num1 > 0)
                    text += (unit > 0) ? _textsLevelZero[num1].AppendWithSpace() : _textsLevelZero[num1].AppendWithSpace() + _textsLevelTwo[unit].AppendWithSpace();
            }
            //Last two digits of 3 digits
            var num2 = number % 100;
            if (num2 > 0)
            {
                //When number is greater than 19
                if (num2 > 19)
                {
                    var num3 = num2 / 10;
                    var num4 = num2 % 10;
                    text += (unit == 0 && number > 100) ? _textsLevelOne[num3].AppendWithSpace().AppendWithAnd() : _textsLevelOne[num3].AppendWithSpace();
                    //And 3rd digit is not zero
                    if (num4 > 0)
                    {
                        text += _textsLevelZero[num4].AppendWithHyphen();
                    }
                }
                //When number is less than 20
                else
                {
                    text += (unit == 0 && number > 100) ? _textsLevelZero[num2].AppendWithSpace().AppendWithAnd() : _textsLevelZero[num2].AppendWithSpace();
                }
            }

            //add all standard units
            if (unit > 0)
                text += _textsLevelTwo[unit].AppendWithSpace();

            //trim for start
            if (doTrim)
            {
                text = text.Trim();
                doTrim = false;
            }

            return text;
        }

    }
}