using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;

namespace RopeToolkit
{
    [CustomEditor(typeof(Rope)), CanEditMultipleObjects]
    public class RopeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        public void OnSceneGUI()
        {
            if (Application.isPlaying)
            {
                return;
            }

            var rope = target as Rope;
            if (rope == null)
            {
                return;
            }

            // Draw floating window with buttons
            if (Selection.objects.Length == 1)
            {
                Handles.BeginGUI();
                GUI.skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene);

                var lastSpawn = rope.spawnPoints.Count > 0 ? rope.spawnPoints[rope.spawnPoints.Count - 1] : float3.zero;
                var location = HandleUtility.WorldToGUIPoint(rope.transform.TransformPoint(lastSpawn)) + Vector2.right * 64.0f;
                GUILayout.Window(0, new Rect(location, Vector2.one), (id) =>
                {
                    if (GUILayout.Button("Push spawn point"))
                    {
                        Undo.RecordObject(rope, "Push Rope Spawn Point");
                        rope.PushSpawnPoint();
                    }
                    if (rope.spawnPoints.Count > 2 && GUILayout.Button("Pop spawn point"))
                    {
                        Undo.RecordObject(rope, "Pop Rope Spawn Point");
                        rope.PopSpawnPoint();
                    }
                }, rope.gameObject.name);

                Handles.EndGUI();
            }

            // Draw position handles
            Handles.color = Rope.Colors.spawnPointHandle;
            for (int i = 0; i < rope.spawnPoints.Count; i++)
            {
                var spawnPoint = rope.spawnPoints[i];
                var position = rope.transform.TransformPoint(spawnPoint);

                EditorGUI.BeginChangeCheck();
                if (Event.current.modifiers.HasFlag(EventModifiers.Shift))
                {
                    position = Handles.PositionHandle(position, Quaternion.identity);
                }
                else
                {
                    var fmh_69_65_638563752596402653 = Quaternion.identity; position = Handles.FreeMoveHandle(position, rope.radius * 4.0f, Vector3.one * 0.5f, Handles.SphereHandleCap);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(rope, "Move Rope Spawn Point");
                    spawnPoint = rope.transform.InverseTransformPoint(position);
                    rope.spawnPoints[i] = spawnPoint;
                }
            }
        }
    }
}
