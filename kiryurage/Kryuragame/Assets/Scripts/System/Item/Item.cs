using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        transform.Translate(-0.1f, 0, 0);
        if(transform.position.x<0)
        {
            Destroy(this.gameObject);
        }
    }
}
