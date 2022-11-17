using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile; // Reference of CSV file

    void Start()
    {
        ReadData();
    }

    // Read data from CSV file
    public void ReadData()
    {
        string[] records = csvFile.text.Split('\n');
        Debug.Log(records.Length);

        string[] columnNames = records[0].Split(';');

        for (int i = 0; i < columnNames.Length; ++i)
        {
            Debug.Log(columnNames[i]);
        }

        Debug.Log("!!!!!!!!!!!");

        for (int i = 1; i < columnNames.Length; ++i)
        {
            string[] fields = records[i].Split(';');
            Debug.Log($"ID : {fields[0]}");

            GenerateEnum.enumNames.Add(fields[0]);

            for (int j = 1; j < columnNames.Length; ++j)
            {
                Debug.Log(fields[j]);
            }

            Debug.Log("----------");
        }
    }
}