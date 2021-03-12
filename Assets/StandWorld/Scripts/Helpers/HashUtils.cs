namespace StandWorld.Helpers
{
    public class HashUtils 
    {
        public static int HashArrayValue<T>(T[] arr)
        {
            int hash = arr.Length;
            foreach (T v in arr)
            {
                hash ^= unchecked(hash * 212121 + v.GetHashCode());
            }

            return hash;
        }
    }
}
