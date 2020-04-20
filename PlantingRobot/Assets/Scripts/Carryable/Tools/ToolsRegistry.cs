using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsRegistry : MonoBehaviour
{
    public Transform toolParent;

    private List<Tool> tools =  new List<Tool>();
    public Transform GetToolParent() {
        return toolParent;
    }

    public void RegisterTool(Tool t) {
        tools.Add(t);
    }

    public void DeregisterTool(Tool t) {
        tools.Remove(t);
    }

    public List<Tool> GetAllTools() {
        return tools;
    }
}
