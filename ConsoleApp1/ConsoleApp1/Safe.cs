using System;

namespace ConsoleApp1
{
    [Serializable]
    public class Safe
    {
        public string Name { get; set; }
        public int BankAccountNumber { get; set; }
        public int Value { get; set; }
        public int LastEntry { get; set; }
    }
}