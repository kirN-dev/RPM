using System;

namespace Lib_11
{
    public static class MathString
    {
        /// <summary>
        /// ��������� ����������� ������ ���������� �����
        /// </summary>
        /// <param name="count">������� ����������� �������</param>
        /// <param name="min">����������� ��������</param>
        /// <param name="max">������������ ��������</param>
        /// <returns>����������� ����������� ������ ���������� �������</returns>
        public static int[] Init(int count, int min = 2, int max = 14)
        {
            int[] array = new int[count];
            Random rnd = new();
            for (int i = 0; i < array.Length; i++)
            {
                int random = rnd.Next(5, 10);
                array[i] = random;
            }
            return array;
        }
        /// <summary>
        /// ���� ����� ����� > 5
        /// </summary>
        /// <param name="numbers">����������� ������</param>
        /// <returns>����� ����� > 5</returns>
        public static int SumMoreFive(this int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > 5)
                {
                    sum += numbers[i];
                }
            }
            return sum;
        }
    }
}
