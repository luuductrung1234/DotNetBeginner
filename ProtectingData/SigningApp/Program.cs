using CryptographyLib;
using static System.Console;

namespace SigningApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // happy case
            SimulateSendMessageWithValidSignature();

            // bad case
            SimulateSignatureModifiedByMiddleMan();
        }

        /// <summary>
        /// Alice and Bob communicate successfully with correct signature
        /// </summary>
        private static void SimulateSendMessageWithValidSignature()
        {
            WriteLine("Alice joined!");
            WriteLine("Bob joined!");

            Write("Alice enter some text to sign:");
            string data = ReadLine();

            var signature = SignatureProtector.GenerateSignature(data);

            WriteLine($"Alice send to Bob a public key {SignatureProtector.PublicKey}");
            WriteLine(". . . . .");
            WriteLine("Bob received");

            WriteLine($"Alice send to Bob signature {signature}");
            WriteLine(". . . . .");
            WriteLine("Bob received");

            WriteLine($"Alice send to Bob a text message {data}");
            WriteLine(". . . . .");
            WriteLine("Bob received");

            WriteLine($"Bob use Alice's generated signature to validate the message");
            if (SignatureProtector.ValidateSignature(data, signature))
            {
                WriteLine("Correct! Signature is valid.");
                WriteLine("This message is sent by Alice.");
            }
            else
            {
                WriteLine("Invalid signature.");
            }
        }

        /// <summary>
        /// Alice and Bob communicate fail because Middle-Man modified the signature
        /// </summary>
        private static void SimulateSignatureModifiedByMiddleMan()
        {
            WriteLine("Alice joined!");
            WriteLine("Bob joined!");

            Write("Alice enter some text to sign:");
            string data = ReadLine();

            var signature = SignatureProtector.GenerateSignature(data);

            WriteLine($"Alice send to Bob a public key {SignatureProtector.PublicKey}");
            WriteLine(". . . . .");
            WriteLine("Bob received");

            WriteLine($"Alice send to Bob signature {signature}");
            WriteLine(". . . . .");
            WriteLine("Middle Man joined!");
            WriteLine("Middle Man modify the signature!");
            var fakeSignature = signature.Replace(signature[0], 'X');
            WriteLine($"Middle Man re-send to Bob fake signature");
            WriteLine(". . . . .");
            WriteLine("Bob received");

            WriteLine($"Alice send to Bob a text message {data}");
            WriteLine(". . . . .");
            WriteLine("Bob received");

            WriteLine($"Bob use Alice's generated signature to validate the message");
            if (SignatureProtector.ValidateSignature(data, fakeSignature))
            {
                WriteLine("Correct! Signature is valid.");
                WriteLine("This message is sent by Alice.");
            }
            else
            {
                WriteLine($"Invalid signature {fakeSignature}.");
                WriteLine("This message is sent by Alice but was modifed by somebody from the Internet.");
            }
        }
    }
}
