using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Win32.SafeHandles;
using static Advanced_C_.Program;

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

            //EnryptExample();
            //OperatorOverloadingExample();
            LINQ();
        }

        public static void LINQ()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    Name = "CountryA",
                    Continent = "Asia",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "CityA1",
                            Population = 5000,
                            IsCapital = false
                        },
                        new City
                        {
                            Name = "CityA2",
                            Population = 25000,
                            IsCapital = true
                        },
                        new City
                        {
                            Name = "CityA3",
                            Population = 8000,
                            IsCapital = false
                        }
                    }
                },
                new Country
                {
                    Name = "CountryB",
                    Continent = "Europe",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "CityB1",
                            Population = 15000,
                            IsCapital = false
                        },
                        new City
                        {
                            Name = "CityB2",
                            Population = 35000,
                            IsCapital = true
                        },
                        new City
                        {
                            Name = "CityB3",
                            Population = 4000,
                            IsCapital = false
                        }
                    }
                },
                new Country
                {
                    Name = "CountryC",
                    Continent = "Africa",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "CityC1",
                            Population = 12000,
                            IsCapital = false
                        },
                        new City
                        {
                            Name = "CityC2",
                            Population = 22000,
                            IsCapital = true
                        }
                    }
                }
            };

            var citiesQuery =
                from country in countries
                group country by country.Name[0];

            foreach (var item in citiesQuery)
            {
                Console.WriteLine(item);
            }
        }

        public class City
        {
            public string Name { get; set; }
            public int Population { get; set; }
            public bool IsCapital { get; set; } // Example of an additional property
        }

        public class Country
        {
            public string Name { get; set; }
            public string Continent { get; set; } // Example of a country-level property
            public List<City> Cities { get; set; }
        }

        public static void OperatorOverloadingExample()
        {
            Point p1 = new Point(3, 3);
            Point p2 = new Point(3, 3);

            Point p3 = p1 + p2;

            Console.WriteLine(p1 == p2);
        }

        class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.y = y;
                this.x = x;
            }

            public static Point operator +(Point left, Point right)
            {
                return new Point(left.x + right.x, left.y + right.y);
            }

            public static bool operator ==(Point left, Point right)
            {
                return left.y == right.y && left.x == right.x;
            }

            public static bool operator !=(Point left, Point right)
            {
                return left.y != right.y || left.x != right.x;
            }

            public static bool operator >(Point left, Point right)
            {
                return left.y > right.y && left.x > right.x;
            }

            public static bool operator <(Point left, Point right)
            {
                return left.y < right.y && left.x < right.x;
            }
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
                using (
                    CryptoStream cryptoStream = new CryptoStream(
                        fsOutput,
                        encryptor,
                        CryptoStreamMode.Write
                    )
                )
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
                using (
                    CryptoStream cryptoStream = new CryptoStream(
                        fsOutput,
                        decryptor,
                        CryptoStreamMode.Write
                    )
                )
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
