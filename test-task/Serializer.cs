using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace test_task
{
    public static class Serializer
    {
        public static void Serialize(FileStream fileStream, ListRand listRand)
        {
            Dictionary<ListNode, int> listNodeIndex = GetListNodeIndex(listRand);
            string jsonText = GetJsonText(listNodeIndex, listRand);
            byte[] jsonInBytes = new UTF8Encoding(true).GetBytes(jsonText);
            fileStream.Write(jsonInBytes, 0, jsonInBytes.Length);
        }

        private static string GetJsonText(Dictionary<ListNode, int> listNodeIndex, ListRand listRand)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ListNode listNode = listRand.Head;
            stringBuilder.Append("[");
            while (listNode != null)
            {
                stringBuilder.Append("{");
                stringBuilder.Append($"Data='{listNode.Data}',");
                stringBuilder.Append($"Prev={(listNode.Prev == null ? "null" : listNodeIndex[listNode.Prev].ToString())},");
                stringBuilder.Append($"Next={(listNode.Next == null ? "null" : listNodeIndex[listNode.Next].ToString())},");
                stringBuilder.Append($"Rand={(listNode.Rand == null ? "null" : listNodeIndex[listNode.Rand].ToString())}");
                stringBuilder.Append("}");
                if (listNode.Next == null)
                {
                    break;
                }
                stringBuilder.Append(";");
                stringBuilder.Append("\n ");

                listNode = listNode.Next;
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        private static Dictionary<ListNode, int> GetListNodeIndex(ListRand listRand)
        {
            int index = 0;
            Dictionary<ListNode, int> nodeIndex = new Dictionary<ListNode, int>();
            ListNode item = listRand.Head;
            while (item != null && !nodeIndex.ContainsKey(item))
            {
                nodeIndex.Add(item, index++);
                item = item.Next;
            }

            return nodeIndex;
        }
    }
}
