using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SeedingWithGrass))]
public class SeedingWithGrassEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SeedingWithGrass script = (SeedingWithGrass)target;


        if (GUILayout.Button("Active Grass"))
        {
            script.GrassesGrow();
        }

    }
}
