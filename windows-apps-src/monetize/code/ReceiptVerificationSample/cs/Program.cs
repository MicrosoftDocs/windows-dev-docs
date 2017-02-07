//<ReceiptVerificationSample>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Net;

namespace ReceiptVerificationSample
{
    public sealed class RSAPKCS1SHA256SignatureDescription : SignatureDescription
    {
        public RSAPKCS1SHA256SignatureDescription()
        {
            base.KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
            base.DigestAlgorithm = typeof(SHA256Managed).FullName;
            base.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).FullName;
            base.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).FullName;
        }

        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
            deformatter.SetHashAlgorithm("SHA256");
            return deformatter;
        }

        public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA256");
            return formatter;
        }
    }

    class Program
    {
        // Utility function to read the bytes from an HTTP response
        private static int ReadResponseBytes(byte[] responseBuffer, Stream resStream)
        {
            int count = 0;
            int numBytesRead = 0;
            int numBytesToRead = responseBuffer.Length;

            do
            {
                count = resStream.Read(responseBuffer, numBytesRead, numBytesToRead);
                numBytesRead += count;
                numBytesToRead -= count;
            } while (count > 0);

            return numBytesRead;
        }

        public static X509Certificate2 RetrieveCertificate(string certificateId)
        {
            const int MaxCertificateSize = 10000;

            // Retrieve the certificate URL.
            String certificateUrl = String.Format(
                "https://go.microsoft.com/fwlink/?LinkId=246509&cid={0}", certificateId);

            // Make an HTTP GET request for the certificate
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(certificateUrl);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Retrieve the certificate out of the response stream
            byte[] responseBuffer = new byte[MaxCertificateSize];
            Stream resStream = response.GetResponseStream();
            int bytesRead = ReadResponseBytes(responseBuffer, resStream);

            if (bytesRead < 1)
            {
                //TODO: Handle error here
            }

            return new X509Certificate2(responseBuffer);
        }

        static bool ValidateXml(XmlDocument receipt, X509Certificate2 certificate)
        {
            // Create the signed XML object.
            SignedXml sxml = new SignedXml(receipt);

            // Get the XML Signature node and load it into the signed XML object.
            XmlNode dsig = receipt.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)[0];
            if (dsig == null)
            {
                // If signature is not found return false
                System.Console.WriteLine("Signature not found.");
                return false;
            }

            sxml.LoadXml((XmlElement)dsig);

            // Check the signature
            bool isValid = sxml.CheckSignature(certificate, true);

            return isValid;
        }

        static void Main(string[] args)
        {
            // .NET does not support SHA256-RSA2048 signature verification by default, 
            // so register this algorithm for verification.
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), 
                "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");

            // Load the receipt that needs to be verified as an XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("..\\..\\receipt.xml");

            // The certificateId attribute is present in the document root, retrieve it
            XmlNode node = xmlDoc.DocumentElement;
            string certificateId = node.Attributes["CertificateId"].Value;

            // Retrieve the certificate from the official site.
            // NOTE: For sake of performance, you would want to cache this certificate locally.
            //       Otherwise, every single call will incur the delay of certificate retrieval.
            X509Certificate2 verificationCertificate = RetrieveCertificate(certificateId);

            try
            {
                // Validate the receipt with the certificate retrieved earlier
                bool isValid = ValidateXml(xmlDoc, verificationCertificate);
                System.Console.WriteLine("Certificate valid: " + isValid);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
//</ReceiptVerificationSample>