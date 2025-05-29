using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField]
    private int fruitID;
    [SerializeField]
    private GameObject fruitParent;

    private void OnEnable()
    {
        //VU2ForestProtectionEventManager?.Instance?.OnRemoveLocalFruitOnTree += RemoveLocalFruit;
    }
    private void OnDisable()
    {
        //VU2ForestProtectionEventManager?.Instance?.OnRemoveLocalFruitOnTree -= RemoveLocalFruit;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void RemoveLocalFruit(string treeName, int treeIndex)
    {
        if(treeName == this.gameObject.name)
        {
            fruitParent.transform.GetChild(treeIndex).gameObject.SetActive(false);
        }
    }

    public void FruitOnThisTreeGotHit(int fruitIndex)
    {
        //VU2ForestProtectionEventManager?.Instance?.PlayerHitFruitOnTree(this.gameObject.name,fruitIndex, fruitID, fruitParent.transform.GetChild(fruitIndex).gameObject.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
