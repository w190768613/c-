using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace calculator
{
    public class Calculate
    {
        public string getResult(string str)
        {
            str = str + "+";
            char[] expArry = str.ToCharArray();  //分割字符串，并存入数组expArry
            string[] numArry = str.Split('(', ')', '+', '-', '×', '÷', '^'); //分割字符串，提取表达式中的数字存入numArry  
            string operations = "()+-×÷^";
            Hashtable signs = new Hashtable();  //定义操作符的优先级并存入哈希表  
            signs.Add("(", 0);
            signs.Add(")", 0);
            signs.Add("+", 1);
            signs.Add("-", 1);
            signs.Add("×", 2);
            signs.Add("÷", 2);
            signs.Add("^", 3);

            string[] resultArry = new string[99];
            int resultIndex = 0;
            int numIndex = 0;
            for (int i = 0; i < str.Length - 1; i++)
            {
                //遇到符号
                if (operations.Contains(expArry[i]))
                {
                    if (expArry[i] == '-')  //如果是"-"，判断是作为减号还是作为负号
                    {
                        //如果"-"不在首位，且前一个字符是右括号或数字
                        if (i != 0 && (expArry[i - 1] == ')' || operations.Contains(expArry[i-1]) == false))
                            resultArry[resultIndex] = "-";  //作为减号使用，仍用"-"表示
                        else resultArry[resultIndex] = "#";  //否则，作为负号使用，并用"#"表示
                    }
                    else resultArry[resultIndex] = expArry[i].ToString();  //遇到其它符号直接存入resultArry
                    resultIndex++;
                }

                //遇到连续非符号字符
                else if (operations.Contains(expArry[i + 1]))
                {
                    while (numArry[numIndex] == "") numIndex++; //去除numArry中的空元素  
                    resultArry[resultIndex] = numArry[numIndex];//从numArry取出一个元素放入resultArry  
                    numIndex++;
                    resultIndex++;
                }
            }

            Stack<char> signStack = new Stack<char>();   //定义符号栈  
            Stack<string> numStack = new Stack<string>();  //定义操作数栈  
            char sign;  //存储当前操作符

            try
            {
                for (int i = 0; i < resultIndex + 1; i++)  //遍历resultArry数组
                {
                    //如果resultIndex所有元素都已检索完毕
                    if (i == resultIndex)
                    {
                        if (signStack.Count != 0)  //如果符号栈不为空
                        {
                            sign = signStack.Pop();  //弹出符号栈栈顶操作符
                            Calcu(ref signStack, ref numStack, ref resultArry[i], ref sign);
                            while (numStack.Count() > 1)
                            //计算到栈中只剩一个操作数为止  
                            {
                                sign = signStack.Pop();
                                Calcu(ref signStack, ref numStack, ref resultArry[i], ref sign);
                            }
                        }
                    }

                    //如果遇到"#",#后一个元素必然是数字，给这个数字添加负号(#不进栈）
                    else if (resultArry[i] == "#") resultArry[i + 1] = "-" + resultArry[i + 1];

                    //遍历过程中，如果遇到"#"以外的操作符
                    else if (operations.Contains(resultArry[i]))
                    {
                        //若操作符不是左括号
                        if (resultArry[i] != "(")
                        {
                            if (signStack.Count() == 0 || Convert.ToInt32(signs[resultArry[i]]) > Convert.ToInt32(signs[signStack.Peek().ToString()]))
                                //第一个操作符，或者操作符优先级大于符号栈栈顶元素优先级时，操作符入栈  
                                signStack.Push(resultArry[i].ToCharArray()[0]);
                            else
                            {
                                //否则，符号栈栈顶元素出栈，调用Calcu方法进行计算  
                                sign = signStack.Pop();
                                Calcu(ref signStack, ref numStack, ref resultArry[i], ref sign);
                            }
                        }

                        //若操作符是左括号，左括号进入符号栈
                        else signStack.Push(resultArry[i].ToCharArray()[0]);
                    }

                    //遍历过程中，如果遇到数字，将数字放入操作数栈
                    else numStack.Push(resultArry[i]);
                }
                //结果出栈  
                return numStack.Pop();
            }
            catch (Exception e)
            {
                MessageBox.Show("表达式格式不正确，请检查！ " + e.Message);
                return null;
            }
        }

        //CalculateResult方法进行具体的加减乘除运算
        public string CalculateResult(string num1, string num2, char sign)
        {
            double result = 0;
            double oper1 = Convert.ToDouble(num1);
            double oper2 = Convert.ToDouble(num2);
            switch (sign)
            {                                             // 先出栈的oper2放在运算符右边，后出栈的oper1放左边
                case '+': result = oper1 + oper2; ; break;
                case '-': result = oper1 - oper2; ; break;
                case '×': result = oper1 * oper2; break;
                case '÷':
                    if (oper2 != 0) result = oper1 / oper2;
                    else MessageBox.Show("表达式中有除0操作，请检查！"); break;
                case '^': result = Math.Pow(oper1, oper2); break;

            }
            return result.ToString();
        }

        //Calcu方法确定一次运算的操作数和操作符
        public void Calcu(ref Stack<char> stacksign, ref Stack<string> stacknum, ref string str, ref char sign)
        {
            string num2 = "";
            string num1 = "";

            //若resultArry当前元素是右括号
            if (str == ")")
            {
                while (sign != '(')   //当sign不是左括号时
                {
                    num2 = stacknum.Pop();//从操作数栈中弹出两个数字，num2存储右操作数
                    num1 = stacknum.Pop();  //num1存储左操作数
                    stacknum.Push(CalculateResult(num1, num2, sign)); //调用CalculateResult方法计算，并将结果存入操作数栈
                    sign = stacksign.Pop(); //弹出符号栈栈顶元素存入sign
                }
            }

            //若resultArry当前元素不是右括号
            else
            {
                num2 = stacknum.Pop();//从操作数栈中弹出两个数字
                num1 = stacknum.Pop();
                stacknum.Push(CalculateResult(num1, num2, sign));//调用CalculateResult方法计算，并将结果存入操作数栈
                if (str != null && "()+-×÷^".Contains(str))  //若resultArry当前元素是操作符
                    stacksign.Push(str.ToCharArray()[0]); //resultArry当前元素进栈
            }
        }
    }

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Calculator());
        }
    }
}
