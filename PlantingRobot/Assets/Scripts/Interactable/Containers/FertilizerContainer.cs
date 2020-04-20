using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerContainer : Container
{
    public FertilizerBox fert = null;

    private Transform toolParent = null;
    private ToolsRegistry toolReg = null;

    public new void Start() {
        base.Start();
        Debug.Assert(fert);
        toolReg = FindObjectOfType<ToolsRegistry>();
        Debug.Assert(toolReg);
        toolParent = toolReg.GetToolParent();
        Debug.Assert(toolParent);
    }

    public InteractionResult BuyFertilizer() {
        if(player.Pay(fert.maxUses * fert.costPerUse)) {
            return new InteractionResult(Instantiate(fert, toolParent), true, false);
        }
        return new InteractionResult(null, false, false);
    }
}
