using SharedLibrary;
using static System.Console;

namespace AssembliesAndNamespaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a color value in hex: ");
            string hex = ReadLine();
            WriteLine("Is {0} a valid color value? {1}",
            arg0: hex, arg1: hex.IsValidHex());
            Write("Enter a XML tag: ");
            string xmlTag = ReadLine();
            WriteLine("Is {0} a valid XML tag? {1}",
            arg0: xmlTag, arg1: xmlTag.IsValidXmlTag());
            Write("Enter a password: ");
            string password = ReadLine();
            WriteLine("Is {0} a valid password? {1}",
            arg0: password, arg1: password.IsValidPassword());
        }
    }
}
