using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace InlineCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Answer is: {StringCalculator(Console.ReadLine().Replace("-", "+-"))}");//we don't calculate miunus so replace it with '+-'

        }
        static double StringCalculator(string inlineStr)
        {
            char[] operators = { '*', '/', '+' };

            if (CheckString(inlineStr, "()".ToCharArray()) && inlineStr.IndexOf('(') != -1)//If string contains brackets 
            {
                int tmpfirst = inlineStr.IndexOf('(');
                int tmpSecond = FindClosingBracket(inlineStr, tmpfirst);
                var substr = inlineStr.Substring(tmpfirst + 1, tmpSecond - tmpfirst - 1);
                inlineStr = inlineStr.Remove(tmpfirst, tmpSecond - tmpfirst+1).Insert(tmpfirst, StringCalculator(substr).ToString());

            }
            for (int i = 0; i < operators.Length; i++)//for all operators
            {
                if (inlineStr.IndexOf(operators[i]) != -1)//if we found where operator is
                {
                    var z = inlineStr.Count(x => x == operators[i]);
                    for (int j = 0; j != z; j++)
                    {
                        int firstNumberlength;
                        int secondNumberlength;
                        StringBuilder tmpString = new StringBuilder();
                        int operatorPos = inlineStr.IndexOf(operators[i]);//check the position of operator

                        if ((operatorPos - 1 >= 0) && (operatorPos + 1 < inlineStr.Length))
                        {
                            int counter = operatorPos - 1;
                            while (Char.IsDigit(inlineStr[counter]) || inlineStr[counter] == '-' || inlineStr[counter] == ',')//read whole number backward
                            {
                                tmpString.Insert(0, inlineStr[counter]);
                                if (--counter < 0)
                                {
                                    break;
                                }
                            }
                            string firstNumber = tmpString.ToString();
                            firstNumberlength = tmpString.Length;
                            tmpString.Clear();
                            counter = operatorPos + 1;
                            while (Char.IsDigit(inlineStr[counter]) || inlineStr[counter] == '-' || inlineStr[counter] == ',')//read whole number forward
                            {
                                tmpString.Append(inlineStr[counter]);
                                if (++counter >= inlineStr.Length)
                                {
                                    break;
                                }
                            }
                            string secondNumber = tmpString.ToString();
                            secondNumberlength = tmpString.Length;
                            tmpString.Clear();
                            //
                            double calculatedData = Calculate(operators[i], Double.Parse(firstNumber), Double.Parse(secondNumber));//make calculation

                            inlineStr = inlineStr//replace old string with the brand new
                                .Remove(operatorPos - firstNumberlength, 1 + firstNumberlength + secondNumberlength)
                                .Insert(operatorPos - firstNumberlength, calculatedData.ToString());
                            //Console.WriteLine($"{ inlineStr} Processed operation: {firstNumber} {operators[i]} {secondNumber}");//Debug data
                        }
                    }
                }
            }
            double resultNumber;
            if (Double.TryParse(inlineStr, out resultNumber))
                return resultNumber;
            else
                Console.WriteLine("Wrong line");
            System.Environment.Exit(-1);
            return -1;
        }
        static double Calculate(char choperator, double first, double second)//just returns processed number
        {

            switch (choperator)
            {
                case '*':
                    return first * second;
                case '/':
                    return Math.Round((first / second), 2);
                case '+':
                    return first + second;
                default:
                    break;
            }
            return 0;
        }
        static bool CheckString(string bracketstring, char[] bracket)
        {
            char left = bracket.First();
            char right = bracket.Last();
            int bracketcount = bracketstring.Count(x => x == left);
            if (bracketstring.Count(x => x == right) == bracketcount)//If number of brackets isn't even=> skip all logic
            {
                for (int i = 0; i < bracketcount; i++)
                {
                    int tmpfirst = bracketstring.IndexOf(left);
                    int tmpsecond = bracketstring.IndexOf(right);
                    if ((tmpsecond > tmpfirst) && (tmpfirst != -1) && (tmpsecond != -1))// If close-bracket goes after open-one=> delete both of them
                    {
                        bracketstring = bracketstring.Remove(tmpfirst, 1).Remove(tmpsecond - 1, 1);
                    }
                    else
                        break;
                }
            }
            if (bracketstring.IndexOfAny(bracket) == -1)//if we dont find any bracket then this string is right 
                return true;
            else
                return false;
        }

        static int FindClosingBracket(String expression, int index)
        {
            int i;
            if (expression[index] != '(')
            {
                return -1;
            }
            Stack st = new Stack();
            for (i = index; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    st.Push((int)expression[i]);
                }
                else if (expression[i] == ')')
                {
                    st.Pop();
                    if (st.Count == 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
