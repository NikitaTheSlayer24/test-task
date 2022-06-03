using System;
using System.IO;

namespace test_task
{
    class Program
    {
        static void Main(string[] args)
        {
            var listRand = new ListRand();
            //listRand.Add("2222");
            //listRand.Add("3333");
            //listRand.Add("4444");
            //listRand.Add("5555");

            //listRand.InstallRandValue(listRand);



          //  Serialize(listRand);

            Deserialize(listRand);

            foreach (var item in listRand)
            {
                Console.WriteLine(item);
            }

        }

        private static void Serialize(ListRand listRand)
        {
            using (FileStream fileStream = new FileStream("test1.json", FileMode.OpenOrCreate))
            {
                listRand.Serialize(fileStream);
            }
        }

        private static void Deserialize(ListRand listRand)
        {
            using (FileStream fileStream = new FileStream("test1.json", FileMode.Open))
            {
                listRand.Deserialize(fileStream);
            }
        }
    }
}
