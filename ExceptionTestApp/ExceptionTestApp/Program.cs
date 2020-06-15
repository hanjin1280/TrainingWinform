using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 100, y = 5, value = 0;

            try
            {
                value = x / y;
                Console.WriteLine($"{x} / {y} = {value}");
                throw new Exception("사용자 에러");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Y의 값을 0보다 크게 입력하세요");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("프로그램이 종료했습니다.");
            }
        }
    }
}
