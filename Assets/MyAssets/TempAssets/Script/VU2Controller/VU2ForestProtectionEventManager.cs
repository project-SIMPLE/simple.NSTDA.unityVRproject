using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2ForestProtectionEventManager : MonoBehaviour
{   
    public static VU2ForestProtectionEventManager Instance { get; private set; }
    [SerializeField]
    private GameObject FlamePrefab;
    [SerializeField]
    private GameObject AlienPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }


    void Start()
    {
        
    }
    private string thisPlayerID;
    public void GetPlayerID(string playerID)
    {
        //Debug.Log("#################### Plater ID:  "+ playerID);
        thisPlayerID = playerID;
    }
    public void RemoveOtherPlayerTree(List<GAMATreesMessage> tree)
    {
        foreach (GAMATreesMessage t in tree) 
        {
            string cID = t.PlayerID;
            //Debug.Log("#################  cID "+ cID);
            //Debug.Log("#################  this ID " + thisPlayerID);
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID != thisPlayerID)
            {
                GameObject.Find(t.Name)?.gameObject.SetActive(false);
            }
        }
    }
    public void UpdateTreeFromGAMA(List<GAMATreesMessage> tree)
    {
        //GameObject.Find(treeName)?.GetComponent<Seeding>()?.ChangeGrowState(Int32.Parse(status));

        foreach (GAMATreesMessage t in tree)
        {
            string cID = t.PlayerID;
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID == thisPlayerID)
            {
                //Debug.Log("Player : "+ cID);
                GameObject.Find(t.Name)?.GetComponent<Seeding>()?.ChangeGrowState(Int32.Parse(t.State));
                //GameObject.Find(t.Name).gameObject.SetActive(false);
            }
        }
    }

    public void UpdateGrassOnTreeFromGAMA(List<GAMATreesMessage> tree)
    {
        foreach (GAMATreesMessage t in tree)
        {
            string cID = t.PlayerID;
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID == thisPlayerID)
            {
                //Debug.Log("Player : "+ cID);
                GameObject.Find(t.Name)?.GetComponent<SeedingWithGrass>()?.GrassesGrow();
                //GameObject.Find(t.Name).gameObject.SetActive(false);
            }
        }
    }

    public void UpdateThreatsMessageFromGAMA(List<GAMAThreatMessage> threats)
    {
        foreach(GAMAThreatMessage t in threats)
        {
            if (t.PlayerID != thisPlayerID) continue;
            Debug.Log(t.Name);
            float GamaX;
            float GamaY;
            float GamaZ;
            if (float.TryParse(t.x, out GamaX) && float.TryParse(t.y, out GamaY) &&
                float.TryParse(t.z, out GamaZ))
            {
            
                Vector3 tmp = new Vector3(GamaX, GamaY, GamaZ);
                CreateThreat(t.Name, tmp);
            }
            else
            {
                Debug.Log("Error : Cannot convert to Float Number");
            }
                
        }
    }
    public void GetPlayerRainEffect(string effect)
    {
        Debug.Log("+++++++++++++ Rain : " + effect);
       
        if(effect == "Start")
        {
            UpdateRainEffect(true);
        }
        else if (effect == "Stop")
        {
            UpdateRainEffect(false);
        }

    }

    public event Action<bool> OnUpdateRainEffect;
    public void UpdateRainEffect(bool t)
    {
        OnUpdateRainEffect?.Invoke(t);
    }

    public void UpdatePlayerBackground(List<GAMATreesMessage> message)
    {
        foreach(GAMATreesMessage t in message)
        {
            string cID = t.PlayerID;
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID == thisPlayerID)
            {
                Debug.Log("Change background to stage: "+ t.Name);
            }
        }
    }

    private int totalFire = 0;
    private void CreateThreat(string name,Vector3 pos)
    {
        switch(name)
        {
            case "Flame1":
                totalFire++;
                UpdateFireEffect(true);
                Instantiate(FlamePrefab,pos, this.transform.rotation);
                break;
            case "Alien":
                Instantiate(AlienPrefab, pos, this.transform.rotation);
                break;
        }
    }
    public event Action<string, string> OnTreeChangeState;
    public void TreeChangeState(string treeName, string state)
    {
        OnTreeChangeState?.Invoke(treeName, state);
    }



    public event Action<bool> OnUpdateFireEffect;
    public void UpdateFireEffect(bool t)
    {
        OnUpdateFireEffect?.Invoke(t);
    }




    public event Action<Vector3> OnFireRemove;
    public void FireRemove(Vector3 location) 
    {

        OnFireRemove?.Invoke(location);
        totalFire--;
        if(totalFire == 0)
        {
            UpdateFireEffect(false);
        }

    }
    

    /*public event Action<string, int> OnRemoveLocalFruitOnTree;
    public void RemoveFruitOnTree(string treeName, int treeIndex)
    {
        OnRemoveLocalFruitOnTree?.Invoke(treeName,treeIndex);
    }

    public event Action<string,int,int,Vector3> OnPlayerHitFruitOnTree;
    public void PlayerHitFruitOnTree(string treeName, int fruitIndex, int fruitID, Vector3 Pos)
    {
        OnPlayerHitFruitOnTree?.Invoke(treeName, fruitIndex, fruitID, Pos);
    }

    public event Action<string> OnPutFruitIntoBucket;
    public void PutFruitIntoBucket(string seedName)
    {
        OnPutFruitIntoBucket?.Invoke(seedName);
    }

    public event Action OnLoadSeedToNextZone;
    public void LoadSeedToNextZone()
    {
        OnLoadSeedToNextZone?.Invoke();
    }*/
    // Update is called once per frame
    void Update()
    {
        
    }

}
