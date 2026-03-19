using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VU2SeedlingsManager : MonoBehaviour
{
    [SerializeField]
    private SeedlingInfoScriptableObj[] seedlingInfo;
    [SerializeField]
    private GameObject TreePrefab;

    [SerializeField]
    private GameObject playFieldbuttomLeft;
    [SerializeField]
    private GameObject playFieldTopRight;

    private RandomPointPlacer randomPointPlacer;
    // Start is called before the first frame update


    private Dictionary<GameObject, OfflineSeedlingTracker> seedlingDict;
    private Dictionary<string, GameObject> nameToObjDict;

    private List<GameObject> seedingList = new List<GameObject>();

    private List<Vector3> seedlingPositions;
    void Start()
    {
        VU2ForestProtectionEventManager.Instance.OnTreeChangeState += UpdateSeedlingState;


        seedlingDict = new Dictionary<GameObject, OfflineSeedlingTracker>();
        nameToObjDict = new Dictionary<string, GameObject>();


        randomPointPlacer = new RandomPointPlacer();
        randomPointPlacer.SetUpWorldPoint(playFieldbuttomLeft.transform.position,
            Mathf.Abs(playFieldbuttomLeft.transform.position.x - playFieldTopRight.transform.position.x),
            Mathf.Abs(playFieldbuttomLeft.transform.position.z - playFieldTopRight.transform.position.z)
            );

        //PrepareSeedlingArea();
    }

    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnTreeChangeState -= UpdateSeedlingState;
    }
    public void PrepareSeedlingArea()
    {
        PlaceSeedling();
    }
    public void ClearSeedlingArea()
    {
        seedlingDict.Clear();
        nameToObjDict.Clear();
        foreach (GameObject obj in seedingList)
        {
            Destroy(obj);
        }
        foreach(GameObject tOBJ in bigTreeList)
        {
            Destroy(tOBJ);
        }
        seedingList.Clear();
        bigTreeList.Clear();
    }

    private void PlaceSeedling()
    {
        

        List<Vector3> tmpPositions = randomPointPlacer.CalculatePlacePoint();

        seedlingPositions = CreateTreeAndRemoveFromList(tmpPositions, 10);


        seedingList.Clear();
        int TreeNum = 0;

        foreach (var position in seedlingPositions)
        {
            int num = Random.Range(0, seedlingInfo.Length - 1);
            Quaternion tmpQ = Quaternion.identity;
            tmpQ.eulerAngles = new Vector3(0,Random.Range(0,359),0);
            GameObject obj = Instantiate(seedlingInfo[num].seedlingPrefab, position, tmpQ);

            obj.name = seedlingInfo[num].name + TreeNum;
            OfflineSeedlingTracker tracker = new OfflineSeedlingTracker(seedlingInfo[num]);
            seedingList.Add(obj);
            seedlingDict.Add(obj, tracker);
            nameToObjDict.Add(obj.name, obj);


            TreeNum++;
            //Instantiate(seedlingPrefab, position, Quaternion.identity);
        }
    }

    public List<Vector3> GetRandomPointInSeedlingZone(int dis,int num)
    {
        return randomPointPlacer.CalculateCustomPlacePoint(dis ,num);
    }

    public void AddGrowValueToSeedling()
    {
        foreach (var obj in seedingList)
        {
            if (seedlingDict.TryGetValue(obj, out var tracker))
            {
                if (tracker.IsGrowing())
                {
                    tracker.SeedlingGrow(1f);
                    if (tracker.isChangingState())
                    {
                        //Debug.Log("Seedling name: " + obj.name + " Grown");
                        VU2ForestProtectionEventManager.Instance.UpdatePlayerTreeFromGAMA(obj.name, tracker.GetState());
                        VU2ForestProtectionEventManager.Instance.ScoreChange(1);
                    }
                }
            }
        }
    }

    private void UpdateSeedlingState(string treeName,string state)
    {
        int seedlingState = int.Parse(state);
        GameObject seedling = nameToObjDict[treeName];
        //seedlingDict[seedling].
        switch (seedlingState)
        {
            case 0:
                VU2ForestProtectionEventManager.Instance.ScoreChange(seedlingDict[seedling].GetState()*(-1));
                seedlingDict[seedling].SetAliveState(false);
                break;
            case -1:
                seedlingDict[seedling].SetGrowingState(false);
                break;
            case 1:
                seedlingDict[seedling].SetGrowingState(true);
                break;
        }

    }

    public void AddGrassesOnSeedling(int num)
    {
        List<GameObject> randomSeedling = RandomPickSeedling(seedingList, num);

        foreach (var obj in randomSeedling)
        {
            VU2ForestProtectionEventManager.Instance.UpdatePlayerGrassOnTreeFromGAMA(obj.name);
        }
    }

    private List<GameObject> bigTreeList = new List<GameObject>();
    private List<Vector3> CreateTreeAndRemoveFromList(List<Vector3> input, int treeNum)
    {
        if (input == null || treeNum > input.Count) return null;

        List<Vector3> copyI = new List<Vector3>(input);
        List<Vector3> TreesPos = RandomPickPoints(copyI,10);

        foreach (Vector3 tree in TreesPos) {
            Quaternion tmpQ = Quaternion.identity;
            tmpQ.eulerAngles = new Vector3(0, Random.Range(0, 359), 0);
            GameObject tmpOBJ = Instantiate(TreePrefab, tree, tmpQ);
            bigTreeList.Add(tmpOBJ);
        }
        List<Vector3> result = copyI.Except(TreesPos).ToList(); ;

        return result;
    }

    private List<Vector3> RandomPickPoints(List<Vector3> input, int number)
    {
        if (input == null || number > input.Count) return null;

        List<Vector3> copyI = new List<Vector3>(input);
        List<Vector3> result = new List<Vector3>(number);

        for (int i = 0; i < number; i++)
        {
            int randomIndex = Random.Range(0, input.Count);
            (copyI[i], copyI[randomIndex]) = (copyI[randomIndex], copyI[i]);
            result.Add(copyI[i]);
        }

        return result;
    }
    
    private List<GameObject> RandomPickSeedling(List<GameObject> input, int number)
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
