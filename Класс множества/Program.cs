using System.Runtime.Intrinsics.Arm;

namespace Класс_множества
{
    internal class Program
    {
        class Set
        {
            int[] elems;
            int n;
            public Set() { }
            public Set(int k)
            {
                elems = new int[k];
            }
            public Set(int[] a)
            {
                elems = new int[a.Length];
                n = 0;
                for(int i = 0; i < a.Length; i++)
                {
                    bool t = false;
                    for (int j = 0; j < n && !t; j++)
                        if (elems[j] == a[i])
                            t = true;
                    if (!t)
                        elems[n++] = a[i];
                }
            }
            public int N
            {
                get => n;
            }
            public int this[int i]
            {
                get
                {
                    if (i < 0 || i >= n)
                        throw new Exception("Ingext out of range");
                    return elems[i];
                }
                set
                {
                    if (i < 0 || i >= n)
                        throw new Exception("Ingext out of range");
                    bool t = false;
                    for (int j = 0; j < n; j++)
                        if (value == elems[j] && j != i)
                            t = true;
                    if (!t)
                        elems[i] = value;
                    else
                        throw new Exception("Double element");
                }
            }
            public void DeleteByPosition(int k)
            {
                if (k < 0 || k >= n)
                    throw new Exception("Ingext out of range");
                for(int i = k; i < n - 1; i++)
                    elems[i] = elems[i + 1];
                n--;
            }
            public void DeleteByValue(int v)
            {
                int ind = IndexOF(v);
                if (ind != -1)
                {
                    for (int i = ind; i < n - 1; i++)
                        elems[i] = elems[i + 1];
                    n--;
                }
            }
            public void AddElement(int v)
            {
                if (n < elems.Length)
                {
                    if (!this.Contains(v)) elems[n++] = v;
                }
                else
                {
                    int[] x = new int[n + 4];
                    for (int i = 0; i < n; i++)
                        x[i] = elems[i];
                    x[n++] = v;
                    elems = new int[x.Length];
                    for (int i = 0; i < n; i++)
                        elems[i] = x[i];
                }
            }
            public bool Contains(int v)
            {
                bool t = false;
                for (int i = 0; i < n && !t; i++)
                    if (elems[i] == v)
                        t = true;
                return t;
            }
            public int IndexOF(int v)
            {
                int ind = -1;
                for (int i = 0; i < n && ind == -1; i++)
                    if (elems[i] == v)
                        ind = i;
                return ind;
            }
            public static Set operator*(Set s1, Set s2)
            {
                int p;
                if (s1.N < s2.N)
                    p = s1.N;
                else
                    p = s2.N;
                Set s3 = new Set(p);
                for (int i = 0; i < s1.N; i++)
                    for (int j = 0; j < s2.N; j++)
                        if (s1[i] == s2[j])
                            s3.AddElement(s1[i]);
                return s3;
            }
            public static Set operator+(Set s1, Set s2)
            {
                Set s3 = new Set(s2.N + s1.N);
                for (int i = 0; i < s1.N; i++)
                    if (!s3.Contains(s1[i]))
                        s3.AddElement(s1[i]);
                for (int i = 0; i < s2.N; i++)
                    if (!s3.Contains(s2[i]))
                        s3.AddElement(s2[i]);
                return s3;
            }
            public static Set operator-(Set s1, Set s2)
            {
                Set s3 = new Set(s1.N);
                for (int i = 0; i < s1.N; i++)
                {
                    bool t = s2.Contains(s1[i]);
                    if (!t)
                        s3.AddElement(s1[i]);
                }
                return s3;
            }
            public static bool operator==(Set s1, Set s2)
            {
                if (s1.N != s2.N)
                    return false;
                bool t = true;
                for (int i = 0; i < s1.N; i++)
                    t = t && s2.Contains(s1[i]);
                return t;
            }
            public static bool operator!=(Set s1, Set s2)
            {
                if (s1.N != s2.N)
                    return true;
                bool t = true;
                for (int i = 0; i < s1.N; i++)
                    t = t && !s2.Contains(s1[i]);
                return t;
            }
            public bool IsSubset(Set s)
            {
                bool t = true;
                for (int i = 0; i < n && t; i++)
                    if (!s.Contains(elems[i]))
                        t = false;
                return t;
            }
            public override string ToString()
            {
                string s = "{";
                for(int i = 0; i < n; i++)
                    s += elems[i] + ", ";
                s+= "}";
                return s;
            }
        }
        static void Main(string[] args)
        {
            Set set = new Set(new int[] { 1, 2, 1, 2, 3, 4, 3, 5 });
            set[2] = 3;
            Console.WriteLine(set);
            Console.WriteLine();

            set.DeleteByPosition(0);
            Console.WriteLine(set);
            Console.WriteLine();

            set.DeleteByValue(3);
            Console.WriteLine(set);
            Console.WriteLine();

            set.AddElement(9);
            set.AddElement(1);
            Console.WriteLine(set);
            Set set2 = new Set(new int[] {4, 6, 3, 8, 9, 1, 2, 5});
            Console.WriteLine(set2);
            Console.WriteLine();

            Set set3 = set * set2;
            Console.WriteLine(set3);
            Console.WriteLine();

            set3 = set + set2;
            Console.WriteLine(set3);
            Console.WriteLine();

            set3 = set - set2;
            Console.WriteLine(set3);
            Console.WriteLine();

            bool t = set.IsSubset(set2);
            Console.WriteLine(t);
            Console.WriteLine();
        }
    }
}
