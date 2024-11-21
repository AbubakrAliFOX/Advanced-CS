using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using static Advanced_C_.Program;
using Microsoft.Win32.SafeHandles;
using System.Security.Cryptography;

namespace Advanced_C_
{
    public class Program
    {
        static void Main(string[] args)
        {
            //int? Age = null;

            //string PersonAge = Age?.ToString() ?? "Hi";

            //Console.WriteLine("Age is " + PersonAge);

            //Person User = new Person { Name = "خالد عبدالله", Age = 28 };

            //User.Age = 10;

            //XmlSerializer serializer = new XmlSerializer(typeof(Person));

            //using (TextWriter writer = new StreamWriter("Person.xml"))
            //{
            //    serializer.Serialize(writer, User);
            //}

            ////Json

            //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Person));
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    jsonSerializer.WriteObject(stream, User);
            //    string jsonString = System.Text.Encoding.UTF8.GetString(stream.ToArray());


            //    // Save the JSON string to a file (optional)
            //    File.WriteAllText("person.json", jsonString);
            //}

            //BinaryFormatter formatter = new BinaryFormatter();
            //using (FileStream stream = new FileStream("person.bin", FileMode.Create))
            //{
            //    formatter.Serialize(stream, User);
            //}


            //Type type = typeof(Person);

            //Console.WriteLine(type.FullName);
            //Console.WriteLine(type.Name);
            //Console.WriteLine(type.IsClass);
            ////var methods = type.GetMethods();
            ////foreach (var method in methods)
            ////{
            ////    Console.WriteLine($"Name: {method.Name}, Type: {method.GetType()}, Return Type: {method.ReturnType}");

            ////}

            //object classInstance = Activator.CreateInstance(type);
            //type.GetMethod("PrintName").Invoke(classInstance, null);
            EnryptExample();

        }

        public static void EnryptExample()
        {
            string inputFile = "E:\\MyImage.jpg";
            string encryptedFile = "E:\\encrypted.jpg";
            string decryptedFile = "E:\\decrypted.jpg";


            // Generate a random IV for each encryption operation
            byte[] iv;
            using (Aes aesAlg = Aes.Create())
            {
                iv = aesAlg.IV;
            }


            string key = "1234567890123456";


            EncryptFile(inputFile, encryptedFile, key, iv);
            DecryptFile(encryptedFile, decryptedFile, key, iv);
        }

        static void EncryptFile(string inputFile, string outputFile, string key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                aesAlg.IV = iv;


                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                using (CryptoStream cryptoStream = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    // Write the IV to the beginning of the file
                    fsOutput.Write(iv, 0, iv.Length);
                    fsInput.CopyTo(cryptoStream);
                }
            }
        }


        static void DecryptFile(string inputFile, string outputFile, string key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                aesAlg.IV = iv;


                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                using (CryptoStream cryptoStream = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                {
                    // Skip the IV at the beginning of the file
                    fsInput.Seek(iv.Length, SeekOrigin.Begin);
                    fsInput.CopyTo(cryptoStream);
                }
            }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class LengthAttribute : Attribute
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public string ErrMsg { get; set; }
            public LengthAttribute(int Min, int Max)
            {
                this.Min = Min;
                this.Max = Max;
            }

            public class Person
            {
                public string Name { get; set; }

                [Length(20, 100, ErrMsg = "Age must be between 20 and 100")]
                public int Age { get; set; }

                public void PrintName()
                {
                    Console.WriteLine("Abuuuuu");
                }

            }

        }
    }
}
