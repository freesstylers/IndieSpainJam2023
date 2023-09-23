using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CSVReader
{
    static string newLineCheck = @"\n";
    public static Dictionary<string, List<string>> ReadCSV(TextAsset text)
    {
        Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();

        string[] allLines = text.text.Split(Environment.NewLine);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split('\t');

            ret[splitData[0]] = new List<string>();

            for (int j = 1; j < splitData.Length; j++)
            {
                string data = splitData[j];

                if(data.Contains(newLineCheck))
                    data = data.Replace(newLineCheck, Environment.NewLine);

                ret[splitData[0]].Add(data);
            }
        }

        return ret;
    }
}
