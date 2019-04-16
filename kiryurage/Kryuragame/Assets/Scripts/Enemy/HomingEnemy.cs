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


    private Renderer renderer;

        //画像変更
    [SerializeField]
    private Sprite damageSprite;
    private bool dead;
    private CircleCollider2D cir;

    private AudioSource audio;

    //dropアイテムと、dropアイテムの画像変更
    [SerializeField]
    private GameObject dropItem;
    [SerializeField]
    private Sprite dropSprite;
    private SpriteRenderer itemSprite;
    private int dropram;    //乱数でdropさせるか決める
    private bool drop;      //ドロップしたかどうか
    private bool rndrop;    //乱数させる

    void Start()
    {
        minusTime = notTouchLimit;
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();
        dead = false;
        diff = (player.transform.position - transform.position);
        itemSprite = dropItem.GetComponent<SpriteRenderer>();
        cir = GetComponent<CircleCollider2D>();
        drop = false;
        rndrop = false;
    }

    void Update()
    {
        if (dead)
            {
            sprite.sprite = damageSprite;
            sprite.material.color = new Color(1, 0, 0, 1.0f);
            if (!rndrop)
            {
                if (renderer.isVisible) audio.PlayOneShot(audio.clip);  //効果音再生
                dropram = Random.Range(0, 12); rndrop = true;
            }
            if (dropram <= 5 && !drop)
            {
                itemSprite.sprite = dropSprite;
                //dropアイテム召喚
                Instantiate(dropItem, transform.position, Quaternion.identity);
                drop = true;
            }

            rig.gravityScale = 10.0f;
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
            if (!renderer.isVisible) {
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
            audio.PlayOneShot(audio.clip);
            dead=true;
        }
    }
}
