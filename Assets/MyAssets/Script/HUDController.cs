using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text1;


    [SerializeField]
    private TextMeshProUGUI text2;

    [SerializeField]
    private GameObject PlayHUD;
    [SerializeField]
    private GameObject PauseHUD;

    [SerializeField]
    private GameObject infoPannel;
    [SerializeField]
    private TextMeshProUGUI infoName;
    [SerializeField]
    private TextMeshProUGUI infoDescription;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject sliderFill;

    public static HUDController HUDInstance { get; private set; }

    private void Awake()
    {
        if (HUDInstance != null && HUDInstance != this)
        {
            Destroy(this);
        }
        else
        {
            HUDInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayHUD.SetActive(true);
        PauseHUD.SetActive(false);
        UpdateText1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchHUD(bool pause)
    {
        PlayHUD.SetActive(!pause);
        PauseHUD.SetActive(pause);
    }
    public void UpdateText1()
    {
        
    }

    private Coroutine co_HideInActiveInfoPannel;
    private float timeout = 3f;

    public void ShowInfoPannel(string objName, int cHardness, int maxHardness)
    {
        if (co_HideInActiveInfoPannel != null)
        {
            StopCoroutine(co_HideInActiveInfoPannel);
        }

        if(!infoPannel.activeSelf) infoPannel.SetActive(true);
        infoName.text = objName;
        slider.maxValue = maxHardness;
        UpdateHardnessInfo(cHardness, maxHardness);

        co_HideInActiveInfoPannel = StartCoroutine(HideInfoPannel());
    }
    public void ShowInfoPannel(string objName)
    {
        if (co_HideInActiveInfoPannel != null)
        {
            StopCoroutine(co_HideInActiveInfoPannel);
        }

        if (!infoPannel.activeSelf) infoPannel.SetActive(true);
        infoName.text = objName;
        infoDescription.text = " Click to collect ";
        slider.gameObject.SetActive(false);
        co_HideInActiveInfoPannel = StartCoroutine(HideInfoPannel());
    }


    public void UpdateHardnessInfo(int cHardness, int maxHardness)
    {
        infoDescription.text = "Hardness " + cHardness.ToString() + " / " + maxHardness.ToString();
        slider.gameObject.SetActive(true);
        if(cHardness <= 0) { sliderFill.SetActive(false); }
        else 
        {
            sliderFill.SetActive(true); 
            slider.value = cHardness;
        }
    }

    public void HideInfoPanel()
    {
        if (co_HideInActiveInfoPannel != null)
        {
            StopCoroutine(co_HideInActiveInfoPannel);
        }
        infoPannel.SetActive(false);
    }

    private IEnumerator HideInfoPannel()
    {
        yield return new WaitForSeconds(timeout);
        infoPannel.SetActive(false);
    }
}
