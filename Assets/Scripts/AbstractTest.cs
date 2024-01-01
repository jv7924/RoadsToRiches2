using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTest : MonoBehaviour
{
    protected abstract void ImplementMe();
    protected virtual void ChangeMe()
    {
        Debug.Log("Hi");
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
