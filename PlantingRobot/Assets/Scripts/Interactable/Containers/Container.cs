using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    public ContainerType type;
    public enum ContainerType { WaterContainer, SeedContainer, DropOffContainer }
}
