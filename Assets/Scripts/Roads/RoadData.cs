using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public abstract class RoadData
{
    public string prefabName { get; set; }
    public Vector3 position { get; set; }
    public Quaternion rotation { get; set; }
}
