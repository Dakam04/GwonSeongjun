using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("계산 식을 입력해주세요");
            evaluate(Console.ReadLine());
            Console.ReadLine(); // pause
        }
        static int evaluate(string formula)
        {
            string[] formulaArray = formula.Split(' ');
            List<string> formulaList = formulaArray.ToList();

            for (int a = 0; a < formulaList.Count; a++) // 괄호 연산
            {

            }

            while (true) //곱하기 나누기 연산
            {
                int a = 0;
                if (0 <= formulaList.FindIndex(x => x.Contains("*"))) //만약 *가 없으면 -1이 반환되서 false
                {
                    a = formulaList.FindIndex(x => x.Contains("*"));
                    formulaList[a - 1] = (int.Parse(formulaList[a - 1]) * int.Parse(formulaList[a + 1])).ToString();// n1 + n2 일때 n1에 계산된 값 넣고 +, n2 요소 삭제
                    formulaList.RemoveAt(a);
                    formulaList.RemoveAt(a);
                }
                else if (0 <= formulaList.FindIndex(x => x.Contains("/")))
                {
                    a = formulaList.FindIndex(x => x.Contains("/"));    
                    formulaList[a - 1] = (int.Parse(formulaList[a - 1]) / int.Parse(formulaList[a + 1])).ToString();
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
                        formulaList[a - 1] = (int.Parse(formulaList[a - 1]) - int.Parse(formulaList[a + 1])).ToString();
                        formulaList.RemoveAt(a);
                        formulaList.RemoveAt(a);
                        break;

                    case "+":
                        formulaList[a - 1] = (int.Parse(formulaList[a - 1]) + int.Parse(formulaList[a + 1])).ToString();
                        formulaList.RemoveAt(a);
                        formulaList.RemoveAt(a);
                        break;

                    default:
                        a++;
                        break;
                }
            }

            for(int a = 0; a < formulaList.Count; a++)
            {
                Console.WriteLine(formulaList[a]);
            }
            return 1;
        }
    }
}
