using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeding : MonoBehaviour
{
    [SerializeField]
    private GameObject[] treeStateModels;

    /**
     * 0 = dead
     * 1 = defaul
     * 2 = grow 1
     * 3 = grow 2
     * 
     **/
    [SerializeField]
    private int treeState;
    [SerializeField]
    private float timeToGrow;
    [SerializeField]
    private float growTimer = 0f;
    /*[SerializeField]
    private float growRate = 1.0f;

    [SerializeField]
    private int maxGrowState;*/

    [SerializeField]
    private GameObject[] treeEmojiIcon;

    [SerializeField]
    private int WeedCount = 0;


    // Start is called before the first frame update

    [SerializeField]
    private CapsuleCollider[] treeColliders;


    [SerializeField]
    private GameObject debugIconOnFire;
    [SerializeField]
    private GameObject debugIconOnConverByGrasses;


    private void OnEnable()
    {
        //VU2ForestProtectionEventManager.Instance.OnUpdateTreeState += OnTreeUpdateStateListener;
    }
    private void OnDisable()
    {
        //VU2ForestProtectionEventManager.Instance.OnUpdateTreeState -= OnTreeUpdateStateListener;
    }

    void Start()
    {
        treeState = 1;
        treeStateModels[treeState].SetActive(true);
        //treeEmojiIcon.SetActive(false);
        ShowEmojiOnTree(0);
        isTreeDying = false;
        isTreeBurning = false;
        timeUntilDie = 20f;
        timeUntilBurn = 10f;
        //maxGrowState = treeStateModels.Length -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
        private void OnTreeUpdateStateListener(string name, int state)
        {
            if(this.name.Equals(name))
            {
                ChangeGrowState(state);
            }
        }
    */
    private void ShowEmojiOnTree(int index)
    {
        foreach (GameObject emoji in treeEmojiIcon)
        {
            emoji.SetActive(false);
        }
        switch (index)
        {
            case 0:

                break;
            case 1:
                treeEmojiIcon[0].SetActive(true);
                break;
            case 2:
                treeEmojiIcon[1].SetActive(true);
                break;
        }
    }
    private void TreeGrown()
    {
        growTimer = 0;


        treeStateModels[treeState].SetActive(false);
        treeState += 1;
        treeStateModels[treeState].SetActive(true);

    }

    private void TreeDegrown()
    {
        growTimer = 0;
        if (treeState == 0) return;

        treeStateModels[treeState].SetActive(false);

        treeState -= 1;
        treeStateModels[treeState].SetActive(true);

    }
    public void ChangeGrowState(int state)
    {
        if (treeState == 0)
        {
            return;
        }
        if (state == 99)
        {
            return;
        }
        treeColliders[treeState].enabled = false;
        treeStateModels[treeState].SetActive(false);
        treeState = state;
        treeColliders[treeState].enabled = true;
        treeStateModels[treeState].SetActive(true);
        if (state > 2 || state <=0)
        {
            //treeEmojiIcon.SetActive(false);
            ShowEmojiOnTree(0);
        }
    }

    [SerializeField]
    private bool isTreeDying;
    [SerializeField]
    private bool isTreeBurning;

    //private float deathTimer = 20f;
    private float timeUntilDie = 20f;
    private float timeUntilBurn = 10f;

    private void FixedUpdate()
    {
        /*if(isTreeDying && treeState != 0)
        {
            if (deathTimer >= timeUntilDie)
            {
                TreeDied();
                deathTimer = 0;
            }
            deathTimer += Time.deltaTime;
        }
        else if(isTreeDying && treeState == 0)
        {
            isTreeDying = false;
        }*/

        if (isTreeBurning && treeState != 0 )
        {
            if(timeUntilBurn <= 0)
            {
                TreeDied();
            }
            timeUntilBurn -= Time.deltaTime;
        }
        else if (isTreeDying && treeState != 0)
        {
            if(timeUntilDie <= 0)
            {
                TreeDied();
            }
            timeUntilDie -= Time.deltaTime;
        }

    }

    private void CheckIsTreeDying()
    {
        
        if (WeedCount >4 && (treeState == 1 || treeState == 2))
        {
            if (WeedCount >= 6)
            {
                if(!isTreeDying) VU2BGSoundManager.Instance?.PlayTreeSoundEffect(this.gameObject, 0);

                //timeUntilDie = 20f;
                isTreeDying = true;
                //treeEmojiIcon.SetActive(true);
                ShowEmojiOnTree(2);


                //debugIconOnConverByGrasses.SetActive(true);
                
            }
            else if (WeedCount < 6)
            {
                isTreeDying = false;
                ShowEmojiOnTree(1);
                //debugIconOnConverByGrasses.SetActive(false);
            }
        }
        else
        {
            ShowEmojiOnTree(0);
            isTreeDying = false;
            //debugIconOnConverByGrasses.SetActive(false);
        }
        
        
    }

    public void ChangeWeedCount(int num)
    {
        WeedCount += num;
        //ChangeGrownRate();
    }

    //
    // -1 = stop growing
    //  0 = die
    //  1 = growing
    //
    private void TreeDied()
    {
        if(treeState != 0) VU2BGSoundManager.Instance?.PlayTreeSoundEffect(this.gameObject, 1);
        /*treeStateModels[treeState].SetActive(false);
        treeState = -1;*/
        ChangeGrowState(0);
        ShowEmojiOnTree(0);
        //isTreeDying = false;
        //isTreeBurning = false;
        treeState = 0;
        VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name, "0");
        
    }
    [SerializeField]
    private int fireOnTree = 0;
    


    public void TreeBurn(GameObject other)
    {
        //Debug.Log(other);

        if (!other.gameObject.name.Equals("FlameAura"))
        {
            if (other.TryGetComponent<ICreateFireOnTree>(out ICreateFireOnTree fire2))
            {
                fire2.OnFireHitTree(this.gameObject);
            }
        }
        else if (other.transform.parent.TryGetComponent<ICreateFireOnTree>(out ICreateFireOnTree fire))
        {
            
            fire.OnFireHitTree(this.gameObject);
        }
        /*if(timeUntilDie > 10f)
        {
            timeUntilDie = 10f;
            deathTimer = 0;
        }*/
        if (fireOnTree == 0)
        {
            isTreeBurning = true;


            //debugIconOnFire.SetActive(true);
        }

        fireOnTree++;
        //Debug.Log("//////// :"+ other.name +" Burn On Tree  "+this.name+ " # :" + fireOnTree);
    }
    public void UnburnTree()
    {
        fireOnTree--;
        //Debug.Log("////////UNBURN On Tree  " + this.name + " # :" + fireOnTree);
        if ( fireOnTree <= 0)
        {
            fireOnTree = 0;
            //isTreeDying = false;
            isTreeBurning = false;
            CheckIsTreeDying();

            //debugIconOnFire.SetActive(false);
            FireOnTree.Clear();
        }
    }

    
    public void GotWeedOnTree()
    {
        if(WeedCount == 0 && treeState!=0)
        {
            VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name,"-1");
        }
        WeedCount++;
        CheckIsTreeDying();
    }
    
    public void RemoveWeedOnTree()
    {
        if(WeedCount > 0)
        {
            WeedCount--;
        }

        if(WeedCount == 0 && treeState != 0)
        {
            VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name,"1");
            
        }
        CheckIsTreeDying();
    }
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.transform.tag == "Fire")
        {
            //Debug.Log("Tree Burn1");
            /*TreeDied();*/
            TreeBurn(other);
        }
    }
    private List<GameObject> FireOnTree;
    private void OnTriggerEnter(Collider other)
    {
        if(FireOnTree == null) FireOnTree = new List<GameObject>();
        
        string tag = other.gameObject.tag;
        if(other.gameObject.CompareTag("Fire"))
        {
            GameObject parent;
            if (other.gameObject.name.Equals("FlameAura"))
            {
                 parent = other.gameObject.transform.parent.gameObject;
            }
            else
            {
                parent = other.gameObject;
            }

            if (!FireOnTree.Contains(parent))
            {
                FireOnTree.Add(parent);
                TreeBurn(other.gameObject);
            }
            
            
        }
        if (other.gameObject.CompareTag("Tools"))
        {
            if (other.gameObject.transform.parent == null) return;


            if(other.gameObject.transform.parent.TryGetComponent<WeedCutter>(out WeedCutter weedScript))
            {
                if (weedScript.IsItemOnPlayerHand())
                {
                    
                    Debug.Log("Player cut tree");
                    TreeDied();
                }
            }
        }
        /*
        if (other.gameObject.tag == "Weed")
        {
            WeedCount++;
            ChangeGrownRate();
        }*/
        
    }
    private void OnTriggerExit(Collider other)
    {
        /*if (other.gameObject.tag == "Weed")
        {
            WeedCount--;
            ChangeGrownRate();
        }*/
    }

   

}
