using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;

public class CSVData : MonoBehaviour
{
    private List<string> listA;

    private object GetCSVdata()
    {
        string filePath = @"../CSV/data.csv";
        StreamReader reader = null;
        if (UnityEngine.Windows.File.Exists(filePath))
        {
            reader = new StreamReader(System.IO.File.OpenRead(filePath));
            listA = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                foreach (var item in values)
                {
                    listA.Add(item);
                }
                foreach (var coloumn1 in listA)
                {
                    
                }
            }
        }

        return listA;
    }

    void Start()
    {
        Debug.Log(GetCSVdata());
    }
}
