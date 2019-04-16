using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private int hp;

    //ダメージを受けた後少しだけ無敵状態にする
    private bool notTouch;
    [SerializeField]
    private float notTouchLimit;
    private float minusTime;
    private SpriteRenderer sprite;


    private Rigidbody2D rig;
    //画像変更
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite damageSprite;
    private bool dead;
    private bool soundPlay;
    [SerializeField,Header("ダメージ、死亡")]
    private AudioSource[] audio;

    //dropアイテムと、dropアイテムの画像変更
    [SerializeField]
    private GameObject dropItem;
    [SerializeField]
    private Sprite dropSprite;
    private SpriteRenderer itemSprite;
    private int dropram;    //乱数でdropさせるか決める
    private bool drop;      //ドロップしたかどうか
    private bool rndrop;    //乱数させる

    private void Start()
    {
        notTouch = false;
        minusTime = notTouchLimit;
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        audio = GetComponents<AudioSource>();
        itemSprite = dropItem.GetComponent<SpriteRenderer>();
        dead = false;
        soundPlay = false;
    }

    void Update () {
        if (hp <= 0)
        {
            sprite.sprite = damageSprite;
            if (!rndrop)
            {
                dropram = Random.Range(0, 12); rndrop = true;
            }
            if (dropram <= 5 && !drop)
            {
                itemSprite.sprite = dropSprite;
                //dropアイテム召喚
                Instantiate(dropItem, transform.position, Quaternion.identity);
                drop = true;
            }
            rig.gravityScale = 1.0f;
            if (!soundPlay)
            {
                audio[1].PlayOneShot(audio[1].clip);
                soundPlay = true;
            }
                dead = true;
           
            if(transform.position.y < 0)
            {
                Score.OneScore();
                Destroy(this.gameObject);
            }
        }
        if (transform.position.x < 0)
            {
                Destroy(this.gameObject);
            }

        if (dead) return;

        if (notTouch)
        {
            sprite.sprite = damageSprite;
            transform.Translate(speed, 0, 0);
            sprite.material.color = new Color(1, 0, 0, 1.0f);
            if(!soundPlay){
            audio[0].PlayOneShot(audio[0].clip);
                soundPlay = true;
            }
            minusTime -=Time.deltaTime;
            if (minusTime <= 0)
            {
                sprite.sprite = defaultSprite;
             transform.Translate(-speed, 0, 0);
                notTouch = false;
                soundPlay = false;
                minusTime = notTouchLimit;
                sprite.material.color = new Color(1, 1, 1, 1.0f);

            }
        }
        else
        {
            transform.Translate(-speed, 0, 0);
        }

        }

        void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bullet"  && !notTouch && !dead)
        {
            hp--;
            Destroy(collision.gameObject);
            notTouch = true;
        }

        if(collision.tag == "Tentacle" && !notTouch && !dead)
        {
            hp--;
            notTouch = true;
        }
    }
}
