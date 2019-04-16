using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    private int count;
    void Update () {
        count = transform.childCount;
        if(count<=0)
        {
            Destroy(this.gameObject);
        }
	}
}
