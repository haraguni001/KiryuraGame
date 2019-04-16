using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    void Update()
    {
        transform.Translate(speed, 0, 0);
    }

    //画面外に出たら消滅
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
