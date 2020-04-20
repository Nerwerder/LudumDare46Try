using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsRegistry : MonoBehaviour
{
    public Transform toolParent;

    public Transform GetToolParent() {
        return toolParent;
    }
}
