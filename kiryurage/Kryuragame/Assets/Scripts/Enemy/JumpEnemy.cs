using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour {
    [SerializeField]
    private int hp;
    [SerializeField]
    private float jump; //ジャンプ力
    private Rigidbody2D rig;

    public bool isGround;  //着地中か
    public bool isJump;    //ジャンプ中か
    public bool isAttack;  //攻撃中か
    [SerializeField]
    private GameObject bullet; //攻撃

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

    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        isGround = false;
        isJump = false;
        isAttack = false;
        set = false;
        notTouch = false;
	}
	

	void Update () {
        if (notTouch)
        {
            Damage();
            return;
        }
        if (hp <= 0)
        {
            sprite.sprite = damageSprite;
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
                    //ホーミング弾発射
                    Instantiate(bullet, transform.position, Quaternion.identity);
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
        if (!set) { minusTime = notTouchLimit; set = true; }
        sprite.material.color = new Color(1, 0, 0, 1.0f);
        minusTime -= Time.deltaTime;
        if (minusTime <= 0)
        {
            sprite.sprite = defaultSprite;
            notTouch = false;
            set = false;
            sprite.material.color = new Color(1, 1, 1, 1.0f);
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
