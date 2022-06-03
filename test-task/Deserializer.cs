using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace test_task
{
    public struct ListNodeStruct
    {
        public ListNode ListNode;
        public string Prev;
        public string Next;
        public string Rand;

        public ListNodeStruct(ListNode listNode, string prev, string next, string data, string rand)
        {
            ListNode = listNode;
            Prev = prev;
            Next = next;
            listNode.Data = data;
            Rand = rand;
        }
    }

    public static class Deserializer
    {
        public static void Deserialize(FileStream fileStream, ListRand listRand)
        {
            ReadFile(fileStream, listRand);
        }

        private static void ReadFile(FileStream fileStream, ListRand listRand)
        {
            List<ListNodeStruct> listNodeStructData = new List<ListNodeStruct>();
            string text = "";
            char[] charsToTrim = { '\0' };
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            while (fileStream.Read(b, 0, b.Length) > 0)
            {
                text = temp.GetString(b).ToString().Trim(charsToTrim);
            }

            listNodeStructData = ReadText(text);
            RestoreList(listNodeStructData, listRand);
        }

        private static List<ListNodeStruct> ReadText(string text)
        {
            char[] charsToTrim = { '\n', ' ' };
            string tmpText = "";
            List<ListNodeStruct> tmpListNodeStructData = new List<ListNodeStruct>();
            Dictionary<string, string> fields = new Dictionary<string, string>();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].ToString() == "[" ||
                    text[i].ToString() == "'" ||
                    text[i].ToString() == "{")
                {
                    continue;
                }
                else if (text[i].ToString() == ";" ||
                         text[i].ToString() == "]") 
                {
                    tmpListNodeStructData.Add(CreateListNodeStruct(fields));
                    fields.Clear();
                }
                else if (text[i].ToString() == "}" ||
                         text[i].ToString() == ",")
                {
                    string[] parts = tmpText.Split(new[] { '=' }, 2);
                    fields.Add(parts[0].Trim(charsToTrim), parts[1]);
                    tmpText = "";
                }
                else
                {
                    tmpText += text[i].ToString();
                }
            }

            return tmpListNodeStructData;
        }

        private static void RestoreList(List<ListNodeStruct> listNodeStructData, ListRand listRand)
        {
            foreach (var data in listNodeStructData)
            {
                listRand.Add(data.ListNode.ToString());
            }
            RestoreRandValue(listNodeStructData, listRand);
        }

        private static void RestoreRandValue(List<ListNodeStruct> listNodeStructData, ListRand listRand)
        {
            foreach (var data in listNodeStructData)
            {
                listRand.InstallRandValue(listRand, Convert.ToInt32(data.Rand));
            }
        }

        private static ListNodeStruct CreateListNodeStruct(Dictionary<string, string> fields)
        {
            ListNode listNode = new ListNode();
            return new ListNodeStruct(listNode, fields["Prev"], fields["Next"], fields["Data"], fields["Rand"]);
        }
    }
}
