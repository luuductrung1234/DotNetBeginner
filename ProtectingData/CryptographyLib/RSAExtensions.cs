using System;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CryptographyLib
{
    /// <summary>
    /// <see cref="ToXmlString"/> and <see cref="FromXmlString"/>, serialize and deserialize 
    /// the <see cref="RSAParatermers"/> structure (which contains public and private keys)
    /// 
    /// The implementation of these methods on MacOS throws <see cref="PlatformNotSupportedException"/>
    /// 
    /// So we have had to re-implement them
    /// </summary>
    public static class RSAExtensions
    {
        public static string ToXmlString(this RSA rsa, bool includePrivateParameters)
        {
            var p = rsa.ExportParameters(includePrivateParameters);

            XElement xml;
            if (includePrivateParameters)
            {
                xml = new XElement("RSAKeyValue",
                    new XElement($"{nameof(p.Modulus)}", Convert.ToBase64String(p.Modulus)),
                    new XElement($"{nameof(p.Exponent)}", Convert.ToBase64String(p.Exponent)),
                    new XElement($"{nameof(p.P)}", Convert.ToBase64String(p.P)),
                    new XElement($"{nameof(p.Q)}", Convert.ToBase64String(p.Q)),
                    new XElement($"{nameof(p.DP)}", Convert.ToBase64String(p.DP)),
                    new XElement($"{nameof(p.DQ)}", Convert.ToBase64String(p.DQ)),
                    new XElement($"{nameof(p.InverseQ)}", Convert.ToBase64String(p.InverseQ)));
            }

            xml = new XElement("RSAKeyValue",
                new XElement($"{nameof(p.Modulus)}", Convert.ToBase64String(p.Modulus)),
                new XElement($"{nameof(p.Exponent)}", Convert.ToBase64String(p.Exponent)));

            return xml?.ToString();
        }

        public static void FromXmlString(this RSA rsa, string parametersAsXml)
        {
            var xml = XDocument.Parse(parametersAsXml);
            var root = xml.Element("RSAKeyValue");
            var p = new RSAParameters();
            p.Modulus = Convert.FromBase64String(root.Element($"{nameof(p.Modulus)}").Value);
            p.Exponent = Convert.FromBase64String(root.Element($"{nameof(p.Exponent)}").Value);

            if (root.Element("P") != null)
            {
                p.P = Convert.FromBase64String(root.Element($"{nameof(p.P)}").Value);
                p.Q = Convert.FromBase64String(root.Element($"{nameof(p.Q)}").Value);
                p.DP = Convert.FromBase64String(root.Element($"{nameof(p.DP)}").Value);
                p.DQ = Convert.FromBase64String(root.Element($"{nameof(p.DQ)}").Value);
                p.InverseQ = Convert.FromBase64String(root.Element($"{nameof(p.InverseQ)}").Value);
            }
            rsa.ImportParameters(p);
        }
    }
}