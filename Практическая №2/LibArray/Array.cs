using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace LibArray
{
    public class Array<T>
    {
        private T[] _items;
        private int _capacity;
        private readonly int _defaultCapacity = 8;
        /// <summary>
        /// ����������� ������ Array
        /// </summary>
        /// <param name="capacity">����� ������� (�����������)</param>
        public Array(int capacity)
        {
            _items = new T[capacity];
            Capacity = capacity;
        }

        public Array()
        {
            _items = new T[_defaultCapacity];
            Capacity = _defaultCapacity;
        }
        public int Lenght { get; private set; }
        /// <summary>
        /// ���������� ������ Array
        /// </summary>
        /// <param name="index">������ ������� (�����������)</param>
        /// <returns>�������� ��������</returns>
        public T this[int index]
        {
            
            get { return _items[index]; }
            set { _items[index] = value; }
        }
        /// <summary>
        /// �������� ����� ������� _items (�����������)
        /// </summary>
        public int Capacity
        {
            get => _capacity; // ��������� �������� �� ���� _capacity
            private set // ������������� �������� ��� ���� _capacity
            {
                if (value == _capacity)
                {
                    return;
                }
                _capacity = value;
                Array.Resize(ref _items, value);
            }
        }
        /// <summary>
        /// ���������� ����� ������� _items
        /// </summary>
        /// <param name="itemsLenght"></param>
        /// <returns></returns>
        private int EnsureCapacity(int itemsLenght = 0)
        {
            int tempCapacity = Capacity;
            while (itemsLenght + Lenght >= tempCapacity)
            {
                tempCapacity *= 2;
            }
            return tempCapacity;
        }
        /// <summary>
        /// ���������� �������� ��� ������� _items
        /// </summary>
        /// <param name="item">�������� ������� �� �������� �� ������������</param>
        public void Add(T item)
        {
            Capacity = EnsureCapacity();
            _items[Lenght++] = item;
        }
        /// <summary>
        /// ���������� ���������� �������� � ������ _items
        /// </summary>
        /// <param name="items">�������� ������� �� �������� �� ������������</param>
        public void AddRange(T[] items)
        {
            Capacity = EnsureCapacity(items.Length);
            Array.Copy(items, 0, _items, Lenght, items.Length);
            Lenght += items.Length;
        }
        /// <summary>
        /// ������� ������� � �����
        /// </summary>
        public void Clear()
        {
            Capacity = _defaultCapacity;
            Lenght = 0;
            _items = new T[Capacity];
        }
        /// <summary>
        /// �������� ��������� ��������� �� ������� _items
        /// </summary>
        /// <param name="item">�������� ������� �� �������� �� ������������</param>
        /// <returns>���������� ������ _items � ���������� ����������, ������� �� �������� �� ������������</returns>
        public bool Remove(T item)
        {
            //bool check = false;
            //List<T> vs = new List<T>();
            //for (int i = 0; i < _items.Length; i++)
            //{
            //    T val = _items[i];
            //    if (Convert.ToInt32(val) != 0)
            //    {
            //        vs.Add(val);
            //        if (vs.Remove(item))
            //        {
            //            check = true;
            //            Lenght--;
            //        }
            //    }
            //}
            //_items = vs.ToArray();

            int indexRemove = Array.IndexOf(_items, item);
            if (indexRemove == -1)
            {
                return false;
            }

            Array.Copy(_items, indexRemove + 1, _items, indexRemove, _items.Length - indexRemove - 1);
            Lenght--;
            return true;
        }
        /// <summary>
        /// ����� ��� ���������� ��� ���������� ������ � �������� ����
        /// </summary>
        static private readonly BinaryFormatter _formatter = new BinaryFormatter();
        
        /// <summary>
        /// ��������� ������ � ���������� ������� _items
        /// </summary>
        /// <param name="data">�������� ��� ����������</param>
        /// <param name="path">���� ��� ���������� ������</param>
        public void Save(object data, string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                _formatter.Serialize(stream, data);
            }
        }
        /// <summary>
        /// ��������� ������ � ����� � ��������� ������ _items
        /// </summary>
        /// <param name="path">���� ��� ���������� ������</param>
        /// <returns>������ � ����� � ������ _items</returns>
        public object Open(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return _formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// ��������� ������ � ����� ������
        /// </summary>
        /// <param name="path">���� ��� ���������� ������</param>
        public void Serialize(string path)
        {
            Save(_items, string.Concat(path));
        }
        /// <summary>
        /// ��������� ������ � ����� � ����������� � ����� ������
        /// </summary>
        /// <param name="path">���� ��� ���������� ������ � ����� � ������ _items</param>
        public void Deserialize(string path)
        {
            _items = (T[])Open(string.Concat(path));

        }

        public DataTable ToDataTable()
        {
            var res = new DataTable();
            int countItems  = _items.Take(Lenght).ToArray().Length;
            for (int i = 0; i < countItems; i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            var row = res.NewRow();
            for (int i = 0; i < countItems; i++)
            {
                row[i] = _items[i];
            }
            res.Rows.Add(row);
            return res;
        }
        /// <summary>
        /// ���������� ���������� ��������� ��� ������� _items
        /// </summary>
        public DataTable ToDataTable2()
        {
            var res = new DataTable();
            for (int i = 0; i < Lenght; i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            var row = res.NewRow();
            for (int i = 0; i < Lenght; i++)
            {
                row[i] = _items[i];
            }
            res.Rows.Add(row);
            return res;
        }
    }
}