using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlameMonster2))]
public class Flame2Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FlameMonster2 script = (FlameMonster2)target;


        if (GUILayout.Button("Kill Flame"))
        {
            script.KillFlame();
        }
        if(GUILayout.Button("Shoot Small Fire"))
        {
            script.ShootFireParticle();
        }
    }
}
