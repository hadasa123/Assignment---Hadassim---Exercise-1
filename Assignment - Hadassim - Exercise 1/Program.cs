using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment___Hadassim___Exercise_1
{
    class Program
    {
        public static IDictionary<string, int> CreatDictionaryNames(string names)
        {
            IDictionary<string, int> numberNames = new Dictionary<string, int>();

            string[] listNames = names.Substring(6, names.Length - 7).Split(',');
            foreach (var item in listNames)
            {
                string name = "";
                name = item.Split('(')[0].Substring(1, item.Split('(')[0].Length - 2);
                string tmp = item.Substring(item.IndexOf('(') + 1, item.Split('(')[1].Length);
                if (tmp[tmp.Length - 1] == ')')
                    tmp = tmp.Substring(0, tmp.Length - 1);
                int frequency = Int32.Parse(tmp);
                numberNames.Add(name, frequency);
            }
            return numberNames;
        }
        public static List<List<string>> CreatListOfSynonyms(string Synonyms)
        {
            List<List<string>> groupsOfSynonyms = new List<List<string>>();
            string[] listSynonyms = Synonyms.Split('(', ')');
            for (int i = 1; i < listSynonyms.Length; i = i + 2)
            {
                int numListsContainPair = 0;
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();
                string symbol1 = listSynonyms[i].Split(',')[0];
                string symbol2 = "";
                if (listSynonyms[i].Split(',')[1][0] == ' ')
                    symbol2 = listSynonyms[i].Split(',')[1].Substring(1, listSynonyms[i].Split(',')[1].Length - 1);
                else
                    symbol2 = listSynonyms[i].Split(',')[1];
                foreach (var groupOfSynonyms in groupsOfSynonyms)
                {
                    if (groupOfSynonyms.Contains(symbol1))
                    {
                        if (!groupOfSynonyms.Contains(symbol2))
                            groupOfSynonyms.Add(symbol2);
                        numListsContainPair += 1;
                        if (list1.Count() == 0)
                            list1 = groupOfSynonyms;
                        else
                            list2 = groupOfSynonyms;
                    }
                    else if (groupOfSynonyms.Contains(symbol2))
                    {
                        groupOfSynonyms.Add(symbol1);
                        numListsContainPair += 1;
                        if (list1.Count() == 0)
                            list1 = groupOfSynonyms;
                        else
                            list2 = groupOfSynonyms;
                    }
                }
                if (numListsContainPair == 0)
                {
                    List<string> newGroup = new List<string> { symbol1, symbol2 };
                    groupsOfSynonyms.Add(newGroup);
                }
                if (numListsContainPair == 2)
                {
                    List<string> list3 = list1.Union(list2).ToList();
                    groupsOfSynonyms.Add(list3);
                    groupsOfSynonyms.Remove(list1);
                    groupsOfSynonyms.Remove(list2);
                }
            }
            return groupsOfSynonyms;

        }
        static void Main(string[] args)
        {
            string names = System.IO.File.ReadAllText(@"C:\Users\User\Desktop\Ex 1\Names.txt");

            IDictionary<string, int>  numberNames = CreatDictionaryNames(names);

            string synonyms = System.IO.File.ReadAllText(@"C:\Users\User\Desktop\Ex 1\Synonyms.txt");
            List<List<string>> groupsOfSynonyms = CreatListOfSynonyms(synonyms);
            string output = "Output: ";
            foreach (var item in numberNames)
                if (!synonyms.Contains(item.Key))
                    output += item.Key + " (" + item.Value + "), ";

            foreach (var groupOfSynonyms in groupsOfSynonyms)
            {
                int sumOfBabiesInGroup = 0;
                string representativeName = "";
                foreach (string s in groupOfSynonyms)
                {
                    if (numberNames.TryGetValue(s, out var value))
                        sumOfBabiesInGroup += value;
                    representativeName = s;
                }
                output += representativeName + " (" + sumOfBabiesInGroup + "), ";
            }
            output=output.Substring(0, output.LastIndexOf(','));
            Console.WriteLine(output);
            File.WriteAllText(@"C:\Users\User\Desktop\Ex 1\Output.txt", output);
        }
       
    }
}


