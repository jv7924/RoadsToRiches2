using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] private Sprite tileFace;

    protected KeyValuePair<bool, Road> up;
    protected KeyValuePair<bool, Road> down;
    protected KeyValuePair<bool, Road> left;
    protected KeyValuePair<bool, Road> right;

    protected int rotation;

    protected abstract void RotateTileClockwise();
    protected abstract void RotateTileCounterClockwise();
    protected abstract bool CheckIfValidConnection();
}
