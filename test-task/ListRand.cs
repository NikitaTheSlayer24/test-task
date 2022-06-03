using System.Collections;
using System.IO;
using System;

namespace test_task
{
    public class ListRand : IEnumerable
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            Serializer.Serialize(s, this);
        }

        public void Deserialize(FileStream s)
        {
            Deserializer.Deserialize(s, this);
        }

        public ListRand()
        {

        }

        public ListRand(string data)
        {
            var item = new ListNode(data);
            Head = item;
            Tail = item;
            Count++;
        }

        public void Add(string data)
        {
            var item = new ListNode(data);

            if (Count == 0)
            {
                Head = item;
                Tail = item;
                Count++;
            }

            Tail.Next = item;
            item.Prev = Tail;
            Tail = item;
            Count++;
        }

        public void InstallRandValue(ListRand listRand)
        {
            foreach (ListNode item in listRand)
            {
                int count = 1;
                int randomValue = 0;
                Random rand = new Random();
                if (randomValue == 0) randomValue = rand.Next(1, listRand.Count);

                foreach (ListNode item1 in listRand)
                {
                    if (randomValue != count)
                    {
                        count++;
                        continue;
                    }
                    item.Rand = item1;
                    randomValue = 0;
                    break;
                }
            }
        }

        public void InstallRandValue(ListRand listRand, int randomValue)
        {
            foreach (ListNode item in listRand)
            {
                if (item.Rand != null)
                {
                    continue;
                }
                int count = 1;
                foreach (ListNode item1 in listRand)
                {
                    if (randomValue != count)
                    {
                        count++;
                        continue;
                    }
                    item.Rand = item1;
                    return;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}
