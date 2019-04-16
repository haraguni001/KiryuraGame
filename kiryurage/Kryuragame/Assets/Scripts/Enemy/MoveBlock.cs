using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour {
    [SerializeField]
    private float speed;
	void Update () {
        if (transform.position.x <= 0) { Destroy(this.gameObject); }
            transform.Translate(-speed, 0, 0);
    }
}
