namespace Lab1
{
    internal class Program
    {
        public void PrintBases(int dec)
        {
            string bin = Convert.ToString(dec, 2);
            string hex = Convert.ToString(dec, 16);

            bin = bin.PadLeft(9, '0');
            hex = hex.PadLeft(9, '0');
            string spaceDec = new string(' ', 4 - dec.ToString().Length);
            Console.WriteLine($" {dec}{spaceDec}| {bin} | {hex} ");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hi, I'm Juan Diego and this is my programming assignment where we'll see different numerical systems!\n");
            Thread.Sleep(2000);
            Console.WriteLine("DEC: short for Decimal, is a system where the digits used are from 0 to 9. Is the system we normally use");
            Thread.Sleep(2000);
            Console.WriteLine("BIN: short for Binary, is a system where the only digits used are 0 and 1. Each 1 represents a power of 2 from right to left");
            Thread.Sleep(2000);
            Console.WriteLine("HEX: short for Hexadecimal, is a system where the digits used are from 0 to 9 and then A to F. Each digit represents a power of 16 from right to left\n");
            Thread.Sleep(2000);

            Console.WriteLine("Here's a table with some examples:\n");
            Thread.Sleep(1500);

            Console.WriteLine(" DEC |    BIN    |    HEX    ");
            Console.WriteLine("-----+-----------+-----------");
            Program program = new Program();
            program.PrintBases(0);
            program.PrintBases(1);
            program.PrintBases(2);
            program.PrintBases(4);
            program.PrintBases(8);
            program.PrintBases(16);
            program.PrintBases(32);
            program.PrintBases(64);
            program.PrintBases(100);
            program.PrintBases(255);
            Console.WriteLine("-----+-----------+-----------");
        }
    }
}
