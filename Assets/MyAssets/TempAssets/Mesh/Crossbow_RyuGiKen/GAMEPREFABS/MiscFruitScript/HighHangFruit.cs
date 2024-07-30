using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighHangFruit : FruitBase
{
    // Start is called before the first frame update
    void Start()
    {
        FruitParam = FruitType.HighFruit;
        DesireToolTypeParam = ToolType.HighTool;
    }

    // Update is called once per frame

    public override void CompareFruitToTool(ToolType tool)
    {
        if(tool == DesireToolTypeParam)
        {
            CorrectToolType();
        }

        else
        {
            IncorrectToolType();
        }
    }

    public void CorrectToolType()
    {
        Debug.Log(" I used the correct tool");
    }

    public void IncorrectToolType()
    {
        Debug.LogWarning("Wrong Tool");
    }
}
