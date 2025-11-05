using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VU2PlantingSocket : MonoBehaviour
{
    private enum SocketState
    {
        Planting,
        Fertilizing,
        Finish
    }

    [SerializeField]
    private SocketState thisSocketState;
    [SerializeField]
    private GameObject[] SocketModels;
    [SerializeField]
    private GameObject fertilizerSpawnPoint;
    [SerializeField]
    private GameObject fertilizerPrefab;

    public UnityEvent OnSocketTaskComplete;

    private void OnEnable()
    {
        SetToInitialState();
        ChangeSocketDisplayModel(0);
    }
    private void OnDisable()
    {
        ChangeSocketDisplayModel(0);
    }

    public void SetToInitialState()
    {
        thisSocketState = SocketState.Planting;
        ChangeSocketDisplayModel(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<VU2PlantableItem>(out VU2PlantableItem item))
        {
            if (item.CanItPlaceInSocket())
            {
                if (CheckSocketStatus(item.GetPlantableID()))
                {
                    Destroy(other.gameObject);
                }
            }
            else
            {
                OnHoverEnter();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<VU2PlantableItem>(out VU2PlantableItem item))
        {
            OnHoverExit();
        }
    }

    private bool CheckSocketStatus(int itemState)
    {
        if(thisSocketState == SocketState.Planting && itemState == 1)
        {
            thisSocketState = SocketState.Fertilizing;
            ChangeSocketDisplayModel(2);
            //InstantiateFertilizeBag();
            return true;
        }
        else if( thisSocketState == SocketState.Fertilizing && itemState == 2 )
        {
            thisSocketState = SocketState.Finish;
            ChangeSocketDisplayModel(3);
            TaskComplete();
            return true;
        }
        return false;
    }

    private void InstantiateFertilizeBag()
    {
        Instantiate(fertilizerPrefab, fertilizerSpawnPoint.transform.position, fertilizerSpawnPoint.transform.rotation);
    }

    public void OnHoverEnter()
    {
        if (thisSocketState == SocketState.Planting) { ChangeSocketDisplayModel(1); }
        
    }

    public void OnHoverExit()
    {
        if (thisSocketState == SocketState.Planting) { ChangeSocketDisplayModel(0); }
            
    }
    
    public void ChangeSocketDisplayModel(int state)
    {
        if (SocketModels == null) return;
        switch (state)
        {
            /// Default
            case 0:
                SocketModels[0].SetActive(true);
                SocketModels[1].SetActive(false);
                SocketModels[3].SetActive(false);
                break;
            /// Hover & Hightlight
            case 1:
                SocketModels[0].SetActive(false);
                SocketModels[1].SetActive(true);
                break;

            /// Planting
            case 2:
                SocketModels[0].SetActive(false);
                SocketModels[2].SetActive(true);
                break;
            /// Fertilizing
            case 3:
                SocketModels[2].SetActive(false);
                SocketModels[3].SetActive(true);
                break;
        }
    }

    private void TaskComplete()
    {
        //SetToInitialState();
        OnSocketTaskComplete?.Invoke();
    }
}
