using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2PlayerEffectAndUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject rainEffect;
    [SerializeField]
    private GameObject fireEffect;
    // Start is called before the first frame update

    private void Start()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += UpdateRainEffect;
        VU2ForestProtectionEventManager.Instance.OnUpdateFireEffect += UpdateFireEffect;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= UpdateRainEffect;
        VU2ForestProtectionEventManager.Instance.OnUpdateFireEffect -= UpdateFireEffect;
    }
    private void UpdateRainEffect(bool t)
    {
        rainEffect.SetActive(t);
    }
    private void UpdateFireEffect(bool t)
    {
        fireEffect.SetActive(t);
    }
    private void HideEffect()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
