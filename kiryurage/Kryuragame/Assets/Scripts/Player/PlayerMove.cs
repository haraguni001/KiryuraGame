using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {
    [SerializeField]
    private float speed; //速さ

    //移動用
    Rigidbody2D rig;



    void Start () {
        rig = GetComponent<Rigidbody2D>();
	}

	void Update () {
        Move();
    }

    private void Move()
    {
        float y = Input.GetAxisRaw("Vertical"); //上下
        Vector2 direction = new Vector2(0, y).normalized;
        rig.velocity = direction * speed;
    }
}
