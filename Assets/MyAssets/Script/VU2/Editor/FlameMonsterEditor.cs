using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlameMonster))]
public class FlameMonsterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FlameMonster script = (FlameMonster)target;


        if (GUILayout.Button("Kill Flame"))
        {
            script.KillFlame();
        }

    }
}
