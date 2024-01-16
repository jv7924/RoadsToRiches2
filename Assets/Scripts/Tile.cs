using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    protected KeyValuePair<bool, Road> up;
    protected KeyValuePair<bool, Road> down;
    protected KeyValuePair<bool, Road> left;
    protected KeyValuePair<bool, Road> right;
}
