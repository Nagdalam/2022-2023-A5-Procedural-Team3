using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemSpawner))]
[CanEditMultipleObjects]
public class ItemSpawnerEditor : Editor
{
    SerializedProperty ItemList;

    void OnEnable()
    {
    }
    public override void OnInspectorGUI()
    {
        ItemSpawner itemSpawner = (ItemSpawner)target;
        base.OnInspectorGUI();
        serializedObject.Update();

        if (GUILayout.Button("Spawn"))
        {
            itemSpawner.Refresh();
        }
        if (GUILayout.Button("Delete"))
        {
            itemSpawner.Delete();
        }
        serializedObject.ApplyModifiedProperties();
    }
}