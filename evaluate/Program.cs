using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluate
{
    class Program
    {

        static List<string> CreateList(string formula)
        {
            formula = formula.Replace(@"\s", "");
            List<string> formulaList = new List<string>();
            string strPiece = "";
            foreach (char tmp in formula)
            {
                switch (tmp)
                {
                    case ' ': //만약 공백 문자면 그냥 넘긴다.
                        break;

                    case '*':
                        formulaList.Add(strPiece);
                        formulaList.Add("*");
                        strPiece = null;
                        break;

                    case '/':
                        formulaList.Add(strPiece);
                        formulaList.Add("/");
                        strPiece = null;
                        break;

                    case '+':
                        formulaList.Add(strPiece);
                        formulaList.Add("+");
                        strPiece = null;
                        break;

                    case '-':
                        if ((strPiece == " ") | (strPiece == null) | (strPiece == "")) //숫자가 음수 일 경우를 대비해서 전 루틴에서 기호가 나오고 - 가 한번 더 나오면 음수로 저장
                        {
                            strPiece += tmp.ToString();
                        }
                        else
                        {
                            formulaList.Add(strPiece);
                            formulaList.Add("-");
                            strPiece = null;
                        }
                        break;

                    default:
                        strPiece += tmp.ToString();
                        break;

                }
            }
            formulaList.Add(strPiece);
            formulaList.RemoveAll(x => String.IsNullOrWhiteSpace(x));
            return formulaList;
        }

        static double Calculation(List<string> formulaList)
        {
            /*
            while (true) //제곱 연산자
            {
                int a = 0;
                if (0 <= formulaList.FindIndex(x => x.Contains(" ^ ")))
                {
                    a = formulaList.FindIndex(x => x.Contains("^"));
                    formulaList[a - 1] = (int.Parse(formulaList[a - 1]) ^ int.Parse(formulaList[a + 1])).ToString();
                    formulaList.RemoveAt(a);
                    formulaList.RemoveAt(a);
                }
            }
            */

            while (true) //곱하기 나누기 연산
            {
                int a = 0;
                if (0 <= formulaList.FindIndex(x => x.Contains("*"))) //만약 *가 없으면 -1이 반환되서 false
                {
                    a = formulaList.FindIndex(x => x.Contains("*"));
                    formulaList[a - 1] = (double.Parse(formulaList[a - 1]) * double.Parse(formulaList[a + 1])).ToString();// n1 + n2 일때 n1에 계산된 값 넣고 +, n2 요소 삭제
                    formulaList.RemoveAt(a);
                    formulaList.RemoveAt(a);
                }
                else if (0 <= formulaList.FindIndex(x => x.Contains("/")))
                {
                    a = formulaList.FindIndex(x => x.Contains("/"));
                    formulaList[a - 1] = (double.Parse(formulaList[a - 1]) / double.Parse(formulaList[a + 1])).ToString();
                    formulaList.RemoveAt(a);
                    formulaList.RemoveAt(a);
                }
                else
                {
                    break;
                }
            }

            for (int a = 0; a < formulaList.Count;) //더하기 빼기 연산
            {
                switch (formulaList[a])
                {
                    case "-":
                        formulaList[a - 1] = (double.Parse(formulaList[a - 1]) - double.Parse(formulaList[a + 1])).ToString();
                        formulaList.RemoveAt(a);
                        formulaList.RemoveAt(a);
                        break;

                    case "+":
                        formulaList[a - 1] = (double.Parse(formulaList[a - 1]) + double.Parse(formulaList[a + 1])).ToString();
                        formulaList.RemoveAt(a);
                        formulaList.RemoveAt(a);
                        break;

                    default:
                        a++;
                        break;
                }
            }

            return double.Parse(formulaList[0]);
        }

        static double evaluate(string formula)
        {
            while (true)  //sin cos 연산
            {
                if (formula.Contains("sin") || formula.Contains("cos"))
                {
                    double tmpNum = 0;
                    int fstFlag = 0;
                    int secFlag = 0;
                    int sinNum = 0;
                    int cosNum = 0;
                    int typeNum = 0; // 0은 sin 1은 cos
                    sinNum = formula.LastIndexOf("sin");
                    cosNum = formula.LastIndexOf("cos");
                    typeNum = (sinNum > cosNum) ? 1 : 0;

                    if (typeNum == 1 & sinNum != -1)
                    {
                        fstFlag = sinNum;
                        secFlag = fstFlag;
                        while (formula[secFlag] != ')')
                        {
                            secFlag++;
                        }
                        tmpNum = Math.Sin(double.Parse(formula.Substring(fstFlag + 4, secFlag - fstFlag - 4)));
                        formula = formula.Remove(fstFlag, secFlag - fstFlag + 1);
                        formula = formula.Insert(fstFlag, tmpNum.ToString());

                    }
                    else if (typeNum == 0 & cosNum != -1)
                    {
                        fstFlag = cosNum;
                        secFlag = fstFlag;
                        while (formula[secFlag] != ')')
                        {
                            secFlag++;
                        }
                        tmpNum = Math.Cos(double.Parse(formula.Substring(fstFlag + 4, secFlag - fstFlag - 4)));
                        formula = formula.Remove(fstFlag, secFlag - fstFlag + 1);
                        formula = formula.Insert(fstFlag, tmpNum.ToString());
                    }
                }
                else
                {
                    break;
                }
            }

            for (int a = 0; a < formula.Length; a++)  // 괄호 연산
            {
                int fstFlag = 0;
                int secFlag = 0;
                string tmpFormula;
                if (formula.Split('(').Length == formula.Split(')').Length) //괄호의 갯수가 정상인지 판단하고 실행
                {
                    if (formula.Contains(")"))
                    {
                        if (formula[a] == '(')
                        {
                            fstFlag = a;
                            while (formula[a] != ')')
                            {
                                if (formula[a] == '(')
                                {
                                    fstFlag = a;
                                }
                                a++;
                            }
                            secFlag = a;
                            tmpFormula = Calculation(CreateList(formula.Substring(fstFlag + 1, secFlag - fstFlag - 1))).ToString(); //괄호 내부 계산
                            formula = formula.Remove(fstFlag, secFlag - fstFlag + 1); // 사용된 식은 지우고
                            formula = formula.Insert(fstFlag, tmpFormula); // 계산된 값 저장
                            a = fstFlag; // a를 마지막 세이브 위치로 복구
                        }
                    }
                }

            }

            List<string> formulaList = CreateList(formula);

            return Calculation(formulaList);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("계산식을 입력해주세요");
            Console.WriteLine(evaluate(Console.ReadLine()));
            Console.ReadLine(); // pause
        }
    }
}
