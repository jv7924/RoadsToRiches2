using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class Road : Tile
{
    string name = "hi";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override bool CheckIfValidConnection()
    {
        throw new System.NotImplementedException();
    }

    protected override void RotateTileClockwise()
    {
        throw new System.NotImplementedException();
    }

    protected override void RotateTileCounterClockwise()
    {
        throw new System.NotImplementedException();
    }
}
