using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CSVReader
{
    public static Dictionary<string, List<string>> ReadCSV(string path)
    {
        Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();

        string[] allLines = File.ReadAllLines(Application.dataPath + path);

        for (int i = 1; i < allLines.Length - 1; i++)
        {
            string[] splitData = allLines[i].Split(',');

            ret[splitData[0]] = new List<string>();

            for (int j = 1; j < splitData.Length - 1; j++)
            {
                ret[splitData[0]].Add(splitData[j]);
            }
        }

        return ret;
    }
}
