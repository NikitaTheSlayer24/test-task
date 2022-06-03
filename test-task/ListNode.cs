namespace test_task
{
    public class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand;
        public string Data;

        public ListNode()
        {
        }

        public ListNode(string data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
