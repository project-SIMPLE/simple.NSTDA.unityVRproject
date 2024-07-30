using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FruitBase : MonoBehaviour
{
    public enum ToolType
    {
        HighTool,
        GroundTool,
        UnderTool
    }
   public enum FruitType
    {
        HighFruit,
        GroundFruit,
        UnderFruit
    }

    public ToolType DesireToolTypeParam;
    public FruitType FruitParam;

    public virtual void CompareFruitToTool(ToolType tool)
    {
        //compare tool?
    }
    public virtual void ActiveFruitBunchOnHook()
    {

    }
}
