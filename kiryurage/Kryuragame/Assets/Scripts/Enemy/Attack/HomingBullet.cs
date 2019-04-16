using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour {

    private GameObject target;  //ホーミングする対象
    [SerializeField]
    private float speed;        //スピード
    private Vector3 diff;

    private Rigidbody2D rig;

	void Start () {
        rig = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        diff = (target.transform.position - transform.position);        
        //進行方向に進む
        var angle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }


    void Update () {
        rig.velocity = new Vector3(diff.x * speed * 3, diff.y * speed * 3);


    }

    //画面外に出たら消滅
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
