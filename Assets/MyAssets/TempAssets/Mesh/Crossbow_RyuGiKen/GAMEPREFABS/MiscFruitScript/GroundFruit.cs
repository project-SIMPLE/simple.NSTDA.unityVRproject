using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFruit : FruitBase
{
    // Start is called before the first frame update
    void Start()
    {
        FruitParam = FruitType.GroundFruit;
        DesireToolTypeParam = ToolType.GroundTool;
    }

    // Update is called once per frame

    public override void CompareFruitToTool(ToolType tool)
    {
        if (tool == DesireToolTypeParam)
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
