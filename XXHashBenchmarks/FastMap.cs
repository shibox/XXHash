//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace XXHashBenchmarks
//{

//    public class LinearProbingHashMap<V>
//    {
//        private int num; //散列表中的键值对数目
//        private uint capacity;
//        private uint[] keys;
//        private V[] values;

//        public LinearProbingHashMap(int capacity)
//        {
//            keys = new uint[capacity];
//            values = new V[capacity];
//            this.capacity = (uint)capacity;
//        }

//        //public V Get(byte[] key)
//        //{
//        //    ulong index = FastHash.Hash(key) % capacity;

//        //    while (keys[index] != null && !key.equals(keys[index]))
//        //    {
//        //        index = (index + 1) % capacity;
//        //    }
//        //    return values[index]; //若给定key在散列表中存在会返回相应value，否则这里返回的是null
//        //}

//        //public void Add(byte[] key, V value)
//        //{
//        //    if (num >= capacity / 2)
//        //    {
//        //        resize(2 * capacity);
//        //    }

//        //    ulong index = FastHash.Hash(key);
//        //    while (keys[index] != null && !key.equals(keys[index]))
//        //    {
//        //        index = (index + 1) % capacity;
//        //    }
//        //    if (keys[index] == null)
//        //    {
//        //        keys[index] = key;
//        //        values[index] = value;
//        //        return;
//        //    }
//        //    values[index] = value;
//        //    num++;
//        //}

//        public V Get(uint key)
//        {
//            uint index = key % capacity;
//            while (keys[index] != 0 && key!=keys[index])
//            {
//                index = (index + 1) % capacity;
//            }
//            return values[index];
//        }

//        public void Add(uint key, V value)
//        {
//            //if (num >= capacity / 2)
//            //{
//            //    resize(2 * capacity);
//            //}
//            ulong index = key % capacity;
//            while (keys[index] != 0 && key!=keys[index])
//            {
//                index = (index + 1) % capacity;
//            }
//            if (keys[index] == 0)
//            {
//                keys[index] = key;
//                values[index] = value;
//                return;
//            }
//            values[index] = value;
//            num++;
//        }

//        //private void resize(uint i)
//        //{
//        //    LinearProbingHashMap<V> hashmap = new LinearProbingHashMap<V>(newCapacity);
//        //    for (int i = 0; i < capacity; i++)
//        //    {
//        //        if (keys[i] != 0)
//        //        {
//        //            hashmap.put(keys[i], values[i]);
//        //        }
//        //    }
//        //    keys = hashmap.keys;
//        //    values = hashmap.values;
//        //    capacity = hashmap.capacity;

//        //}


//    }

//}
