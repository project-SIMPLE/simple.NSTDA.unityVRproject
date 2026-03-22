using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageSetup : MonoBehaviour
{
    [SerializeField]
    private SeedcollectionTimeline timeline;
    [SerializeField]
    private List<GameObject> noFruitTreeList;
    [SerializeField]
    private int seasonIndex = 1;
    [SerializeField]
    private List<GameObject> posList;
    [SerializeField]
    private GameObject treeObjParent;


    public void ClearAllTreeFromStage()
    {
        if (treeObjParent.transform.childCount == 0) return;
        //Debug.Log(treeObjParent.transform.childCount);
        int i =0;
        /*foreach (Transform o in treeObjParent.GetComponentsInChildren<Transform>())
        {
            Debug.Log(i);
            Destroy(o.gameObject);
            i++;
        }*/
        for (i = 0; i< treeObjParent.transform.childCount;i++)
        {
            //Debug.Log(i);
            Destroy(treeObjParent.transform.GetChild(i).gameObject);
        }

    }

    public void SetupTreeTimelineInfo(int seaon)
    {
        List<SeedInSeason> seasonInfo;
        switch (seaon)
        {
            case 1:
                seasonInfo = timeline.Season1;
                break;
            case 2:
                seasonInfo = timeline.Season2;
                break;
            case 3:
                seasonInfo = timeline.Season3;
                break;
            case 4:
                seasonInfo = timeline.Season4;
                break;
            case 5:
                seasonInfo = timeline.Season5;
                break;
            case 6:
                seasonInfo = timeline.Season6;
                break;
            default:
                seasonInfo = timeline.Season1;
                break;
        }

        SetupTreeInCurrentSeason(seasonInfo);
    }

    private void SetupTreeInCurrentSeason(List<SeedInSeason> info)
    {
        List<GameObject> tmpAllPos = new List<GameObject>(posList);
        List<GameObject> usedPos;
        foreach (SeedInSeason season in info)
        {
            usedPos = RandomPickGameObject(tmpAllPos, season.number);
            CreateTreeOnMap(usedPos,season.treePrefab);

            tmpAllPos.RemoveAll(x => usedPos.Contains(x));
        }

        List<GameObject> nonFruitPos = RandomPickGameObject(tmpAllPos, 40);
        CreateRandomTree(nonFruitPos, noFruitTreeList);
        
    }

    private void CreateTreeOnMap(List<GameObject> pos, GameObject prefab)
    {
        foreach (GameObject obj in pos)
        {
            Quaternion tmpQ = Quaternion.identity;
            tmpQ.eulerAngles = new Vector3(0, Random.Range(0, 359), 0);
            Instantiate(prefab, obj.transform.position, tmpQ, treeObjParent.transform);
        }
    }

    private void CreateRandomTree(List<GameObject> pos, List<GameObject> prefabList)
    {
        foreach (GameObject obj in pos)
        {
            Quaternion tmpQ = Quaternion.identity;
            tmpQ.eulerAngles = new Vector3(0, Random.Range(0, 359), 0);
            Instantiate(prefabList[Random.Range(0, (prefabList.Count-1)  )]
                , obj.transform.position, tmpQ, treeObjParent.transform);
        }
    }


    private List<GameObject> RandomPickGameObject(List<GameObject> input, int number)
    {
        if (input == null || number > input.Count) return null;

        List<GameObject> copyI = new List<GameObject>(input);
        List<GameObject> result = new List<GameObject>(number);

        for (int i = 0; i < number; i++)
        {
            int randomIndex = Random.Range(0, input.Count);
            (copyI[i], copyI[randomIndex]) = (copyI[randomIndex], copyI[i]);
            result.Add(copyI[i]);
        }

        return result;
    }

}
