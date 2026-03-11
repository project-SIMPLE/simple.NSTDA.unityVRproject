using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedlingInfoScriptableObj")]
public class SeedlingInfoScriptableObj : ScriptableObject
{
    public string seedlingName;
    public GameObject seedlingPrefab;
    public float[] growValue = new float[2];
    public float growRate;
}
