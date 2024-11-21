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

            public void PrintName ()
            {
                Console.WriteLine("Abuuuuu");
            }

        }

    }
}
