namespace EtoDesignerHost
{
    using System.Collections;

    /// This is just a special stack to handle the transaction descriptions.
    /// It functions like a normal stack, except it looks for the first
    /// non-null (and non "") string.
    internal class StringStack : Stack
    {
        internal StringStack()
        {
        }

        internal string GetNonNull()
        {
            int items = this.Count;
            object item;
            object[] itemArr = this.ToArray();
            for (int i = items - 1; i >= 0; i--)
            {
                item = itemArr[i];
                if (item != null && item is string && ((string)item).Length > 0)
                {
                    return (string)item;
                }
            }
            return "";
        }
    }
}