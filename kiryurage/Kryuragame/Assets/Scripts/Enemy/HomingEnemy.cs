using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingEnemy : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float notTouchLimit;
    private float minusTime;
    private SpriteRenderer sprite;

    private GameObject player;
    private Rigidbody2D rig;
    private Vector3 diff;

    //画面外にいるかどうか 画面外にいるとfalse
    private bool visible;

        //画像変更
    [SerializeField]
    private Sprite damageSprite;
    private bool dead;
    private CircleCollider2D cir;
    void Start()
    {
        minusTime = notTouchLimit;
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        visible = false;
        dead = false;
        diff = (player.transform.position - transform.position);
        cir = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (dead)
            {
            sprite.sprite = damageSprite;
            rig.gravityScale = 1.0f;
            transform.Translate(0, 0, 0);
            cir.enabled = false;
            if (transform.position.y < 0)
            {
                Score.OneScore();
                Destroy(this.gameObject);
            }
            return;
        }

        if (transform.position.x < 0)
        {
            Destroy(this.gameObject);
        }
            if (!visible) {
                transform.Translate(-speed, 0, 0);
            }
            else
            {
                //プレイヤーの方向に向かって移動していく
                rig.velocity = new Vector3(diff.x * speed * 3, diff.y * speed * 3);
            }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && !dead)
        {
            dead=true;
            Destroy(collision.gameObject);

        }

        if (collision.tag == "Tentacle" && !dead)
        {
            dead=true;
        }
    }

    //画面内にいるとき
   private void OnBecameVisible()
    {
        visible = true;
    }
}
