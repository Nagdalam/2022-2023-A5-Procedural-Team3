using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Generate Enum", menuName = "ScriptableObjects/GenerateEntitiesID", order = 1)]
public class DataGenerateEnum : ScriptableObject
{
    public string enumName;
    public string filePath;
}
