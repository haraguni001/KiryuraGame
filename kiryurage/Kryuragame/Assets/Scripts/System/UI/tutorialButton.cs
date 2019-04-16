using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialButton : MonoBehaviour {
    private bool push;
    void Start()
    {
        push = false;
    }
    public void Push()
    {
        push = true;
    }

    public bool IsPush()
    {
        return push;
    }
}
