using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour {
    [SerializeField]
    private int hp;
    [SerializeField]
    private float jump; //ジャンプ力
    private Rigidbody2D rig;
    [SerializeField]
    private float speed; //迫りくる速さ

    private bool isGround;  //着地中か
    private bool isJump;    //ジャンプ中か
    private bool isAttack;  //攻撃中か
    [SerializeField]
    private GameObject bullet; //攻撃

    //dropアイテムと、dropアイテムの画像変更
    [SerializeField]
    private GameObject dropItem;
    [SerializeField]
    private Sprite dropSprite;
    private SpriteRenderer itemSprite;
    private int dropram;    //乱数でdropさせるか決める
    private bool drop;      //ドロップしたかどうか
    private bool rndrop;    //乱数させる

    //画像変更
    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite jumpSprite;
    [SerializeField]
    private Sprite attackSprite;
    [SerializeField]
    private Sprite damageSprite;

    //攻撃する間隔
    [SerializeField]
    private float jumpTime;
    [SerializeField]
    private float attackTime;
    public float minusTime;
    private bool set;

    //ダメージを受けた後少しだけ無敵状態にする
    private bool notTouch;
    [SerializeField]
    private float notTouchLimit;
    private BoxCollider2D box;

    //効果音
    [SerializeField,Header("ジャンプ、アタック、死亡、ダメージ")]
    private AudioSource[] audio;
    private Renderer renderer;
    private bool soundPlay;

    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        itemSprite = dropItem.GetComponent<SpriteRenderer>();
        audio = GetComponents<AudioSource>();
        renderer = GetComponent<Renderer>();
        sprite.sprite = dropSprite;
        isGround = false;
        isJump = false;
        isAttack = false;
        set = false;
        notTouch = false;
        drop = false;
        rndrop = false;
        soundPlay = false;
	}
	

	void Update () {
        if (transform.position.x < 0)
        {
            Destroy(this.gameObject);
        }

        if (notTouch)
        {
            Damage();
        }
        else
        {
            transform.Translate(-speed, 0, 0);
        }
        if (hp <= 0)
        {
            sprite.sprite = damageSprite;
            if (!rndrop) {
        if (renderer.isVisible) audio[2].PlayOneShot(audio[2].clip);  //効果音再生
                dropram = Random.Range(0, 12);  rndrop = true;
            }
            if (dropram<=5 && !drop)
            {
                itemSprite.sprite = dropSprite;
                //dropアイテム召喚
                Instantiate(dropItem, transform.position, Quaternion.identity);
                drop = true;
            }
            rig.gravityScale = 1.0f;
            box.enabled = false;
            if (transform.position.y < 0)
            {
                Score.OneScore();
                
                Destroy(this.gameObject);
            }
            return;
        }

        Jump();
	}

    void Jump()
    {
        //着地
        if (isGround)
        {
            sprite.sprite = defaultSprite;
            isAttack = false;
            //時間セット
            if (!set) { minusTime = jumpTime; set = true; }
            MinusTime();
            if (minusTime <= 0)
            {
                //ジャンプ処理
                set = false;
        if (renderer.isVisible) audio[0].PlayOneShot(audio[0].clip);  //効果音再生
                rig.AddForce(Vector2.up * jump);
                sprite.sprite = jumpSprite;
            }
        }
        //ジャンプ
        if (isJump)
        {
            //上昇中はジャンプのスプライト
            if (!isAttack) { sprite.sprite = jumpSprite; }
            //時間セット
            if (!set) { minusTime = attackTime; }
            MinusTime();
            if (minusTime <= 0)
            {
                //攻撃
                if (!isAttack)
                {
                    sprite.sprite = attackSprite;
                    if (renderer.isVisible) {
                        //ホーミング弾発射
                        Instantiate(bullet, transform.position, Quaternion.identity);
                    audio[1].PlayOneShot(audio[1].clip);  //効果音再生
                    }
                    isAttack = true;
                    set = false;
                }
            }
        }
    }

    //ダメージ
    void Damage()
    {
        sprite.sprite = damageSprite;
        if (renderer.isVisible && !soundPlay) audio[3].PlayOneShot(audio[3].clip); soundPlay = true;  //効果音再生
        if (!set) { minusTime = notTouchLimit; set = true; }
        sprite.material.color = new Color(1, 0, 0, 1.0f);
        minusTime -= Time.deltaTime;
        if (minusTime <= 0)
        {
            sprite.sprite = defaultSprite;
            notTouch = false;
            set = false;
            soundPlay = false;
            sprite.material.color = new Color(1, 1, 1, 1.0f);
             transform.Translate(speed, 0, 0);
        }
    }

    void MinusTime()
    {
        minusTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag== "Tentacle" && !notTouch)
        {
            hp--;
            notTouch = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //着地
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //ジャンプ
        if(collision.gameObject.tag=="Ground")
        {
            isGround = false;
            isJump = true;
        }
    }
}
