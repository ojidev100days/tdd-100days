using System;

namespace FizzBazz
{
    internal class FizzBuzzConverter
    {
        private const int FIZZ_NUMBER = 3;
        private const string FIZZ_STRING = "Fizz";

        private const int BUZZ_NUMBER = 5;
        private const string BUZZ_STRING = "Buzz";

        internal string Convert(int number)
        {
            if (number % (FIZZ_NUMBER * BUZZ_NUMBER) == 0) return FIZZ_STRING + BUZZ_STRING;
            if (number % FIZZ_NUMBER == 0) return FIZZ_STRING;
            if (number % BUZZ_NUMBER == 0) return BUZZ_STRING;
            return number.ToString();
        }
    }
}