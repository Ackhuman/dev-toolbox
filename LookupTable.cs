namespace DevToolbox.LookupTable {
    public static class EnumerableExtensions
    {
        public static LookupTable<TKey, TValue> ToLookupTable<T, TKey, TValue>(
            this IEnumerable<T> items,
            Func<T, TKey> keySelector, 
            Func<T, TValue> elementSelector)
        {
            var lookupTable = new LookupTable<TKey, TValue>();
            foreach (var item in items)
            {
                lookupTable.Add(keySelector(item), elementSelector(item));
            }
            return lookupTable;
        }
    }
    //A hash table that is more efficient than Dictionary when there are key misses
    public class LookupTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public LookupTable()
        {
            
        }

        public LookupTable(Dictionary<TKey, TValue> dict)
        {
            KeyValues = dict;
            KeySet = new HashSet<TKey>(dict.Keys);
        }

        public ICollection<TKey> Keys => KeySet;
        public ICollection<TValue> Values => KeyValues.Values;
        private HashSet<TKey> KeySet { get; } = new HashSet<TKey>();
        private Dictionary<TKey, TValue> KeyValues { get; } = new Dictionary<TKey, TValue>();

        public bool Add(TKey key, TValue value)
        {
            KeyValues.Add(key, value);
            return KeySet.Add(key);
        }

        public bool Contains(TKey key)
        {
            return KeySet.Contains(key);
        }

        public bool Remove(TKey key)
        {
            KeyValues.Remove(key);
            return KeySet.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            if (KeySet.Contains(key))
            {
                value = KeyValues[key];
                return true;
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get => KeyValues[key];
            set
            {
                if (KeySet.Add(key))
                {
                    KeyValues.Add(key, value);
                }
                else
                {
                    KeyValues[key] = value;
                }
            }
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return KeyValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            KeySet.Clear();
            KeyValues.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return KeySet.Contains(item.Key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
    }
}
