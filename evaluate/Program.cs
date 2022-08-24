using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evaluate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("계산 식을 입력해주세요");
            string formula = Console.ReadLine();
            string[] formulaSplit = formula.Split(' ');
            for (int a = 0 ; a < formulaSplit.Length; a++)
            {
                Console.WriteLine(formulaSplit[a]);
            }
            
        }
    }
}
