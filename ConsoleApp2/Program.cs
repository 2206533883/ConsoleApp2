using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Dictionary<string, int> fdic = GetFileDic("D:\\sdxl1.txt");
            List<KeyValuePair<string, int>> lst = GetFileDic("D:\\sdxl1.txt");
            //List<string> lstTen = new List<string>(fdic.Keys);
            for (int i = 0; i < 10; i++)
            {
                //Console.WriteLine(lstTen[i] + ":" + fdic[lstTen[i]]);
                Console.WriteLine(lst[i].Key + ":" + lst[i].Value);
            }
            stopwatch.Stop();
            //test
            Console.WriteLine("running:{0}", stopwatch.Elapsed);
            Console.WriteLine("Hello!");
        }

        static List<KeyValuePair<string, int>> GetFileDic(string filePath)
        {
            string str;
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs,Encoding.UTF8);
            Dictionary<string, int> fdic = new Dictionary<string, int>();
            while((str = sr.ReadLine()) != null)
            {
                string s = str.ToLower();
                string Pattern = @"\,|\.|\ |\n|\[|\]|\r|\?|\;|\:|\!|\(|\)|\042|\“|\”|\-|[0-9]|\'|""|\‘|\’|\…|__";
                string[] words = Regex.Split(s,Pattern);
                foreach(string word in words)
                {
                    if (fdic.ContainsKey(word))
                    {
                        fdic[word]++;
                    }
                    else
                    {
                        if (word != null && word.Trim() != ""&&word.Length>=2)
                        {
                            fdic[word] = 1;
                        }
                    }
                }
            }
            //aaa
            List<KeyValuePair<string, int>> lst = new List<KeyValuePair<string, int>>(fdic);
            lst.Sort(delegate (KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
            {
                return s2.Value.CompareTo(s1.Value);
            });
            fdic.Clear();
            //foreach (KeyValuePair<string, int> kvp in lst)
            //{
            //    fdic.Add(kvp.Key, kvp.Value);
            //}
            return lst;
        }
    }
}
