using LibArray;
using System;

namespace Lib_11
{
    public static class ExtensionArray
    {
        /// <summary>
        /// ���������� ����������� ������� �� ������ Array
        /// </summary>
        /// <param name="numbers">���������� ������</param>
        public static void Init(this Array<int> numbers)
        {
            Random rnd = new();
            for (int i = 0; i < numbers.Capacity; i++)
            {
                numbers.Add(rnd.Next(0, 100));
            }
        }
        /// <summary>
        /// ������� ������� �����
        /// </summary>
        /// <param name="elements">���������� ������</param>
        /// <returns>�������� ��������� �������</returns>
        public static int Difference(this Array<int> elements)
        {
            int difference = 0;
            for (int i = 0; i < elements.Capacity; i++)
            {
                difference -= elements[i];
            }
            return difference;
        }
    }
}
