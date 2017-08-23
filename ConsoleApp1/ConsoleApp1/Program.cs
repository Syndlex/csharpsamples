using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public bool output { get; set; }

        static void Main(string[] args)
        {
            var program = new Program();
            Console.WriteLine("Pls press the any key to load the list in background");
            Console.WriteLine("To see it in foreground press i");
            Console.WriteLine("To put it back in Background o");
            Console.ReadKey();
            var intTask = program.IniValues();


            Console.WriteLine("While we count up pls ");
            program.switchoutput();
        }

        private async void switchoutput()
        {
            //Async ForeGround And Background showing 
            await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    //Reads i and o and Sets Properties
                    if (Console.ReadKey().KeyChar == 'i')
                        output = false;
                    if (Console.ReadKey().KeyChar == 'o')
                        output = true;
                }
            });
        }

        private async Task<List<int>> IniValues()
        {
            //A Do work method initialization of a huge List
            var list = new List<int>(new int[2000000000]);
            await Task.Factory.StartNew(() =>
            {
                var i = 1;
                //Setting ob Values in the list.
                list.ForEach(d =>
                {
                    d = i++;
                    if (i % 200 == 0 && output)
                    {
                        Console.WriteLine("The Momentary value of I = " + i);
                        System.Threading.Thread.Sleep(1000);
                    }
                });
            });
            return list;
        }
    }
}