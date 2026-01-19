using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedingWithGrass : Seeding
{
    [SerializeField]
    private GameObject[] grasses;
    [SerializeField]
    private int grassCount;

    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateGrassOnTree += OnGrassesUpdateListener;
        VU2ForestProtectionEventManager.Instance.OnUpdateTreeState += OnTreeUpdateStateListener;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateGrassOnTree -= OnGrassesUpdateListener;
        VU2ForestProtectionEventManager.Instance.OnUpdateTreeState -= OnTreeUpdateStateListener;
    }


    private bool haveGrasses = false;
    public void GrassesGrow()
    {
        grassCount = 1;
        foreach(GameObject grass in grasses)
        {
            grass.SetActive(true);
        }
        if (!haveGrasses)
        {
            GotWeedOnTree();
            haveGrasses = true;
        }
        
    }
    public void GrassCut()
    {
        grassCount--;
        if(grassCount <= 0)
        {
            grassCount = 0;
            RemoveWeedOnTree();
            VU2BGSoundManager.Instance?.PlayTreeSoundEffect(this.gameObject,2);
            haveGrasses = false;
        }
    }

    private void OnGrassesUpdateListener(string name)
    {
        if (this.name.Equals(name))
        {
            GrassesGrow();
        }
        else return;
    }

    private void OnTreeUpdateStateListener(string name, int state)
    {
        if (this.name.Equals(name))
        {
            ChangeGrowState(state);
        }
    }
}
