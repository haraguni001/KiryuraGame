using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField, Header("攻撃する弾")]
    private GameObject bullet;
    [SerializeField, Header("触手")]
    private GameObject tentacle;

    //敵にぶつかった後少しだけ無敵状態にする
    private bool notTouch;
    [SerializeField]
    private float notTouchLimit;
    private float minusTime;

    public static int hp; //体力
    [SerializeField]
    private int defoHp;
    [SerializeField]
    private int defoLife;
    public static int life = 3; //残機
    //アイテムを取ると増える。これが100になると残機アップ
    public static int item = 0;

    //発動制限

        //弾撃ち
    [SerializeField]
    private float bulletTime;
    private float minusTimeB;
    private bool isBullet;

        //触手
    [SerializeField]
    private float tentacleTime;
    private float minusTimeT;
    private bool isTentacle;

        //食事
    [SerializeField]
    private float eatTime;
    private float minusTimeE;
    private bool isEat;

    //画像変更
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite attackSprite;
    [SerializeField]
    private Sprite damageSprite;
    [SerializeField]
    private Sprite eatSprite;

    void Start()
    {
        tentacle.SetActive(false);
        minusTimeB = bulletTime;
        minusTimeT = tentacleTime;
        isBullet = false;
        isTentacle = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        notTouch = false;
        minusTime = notTouchLimit;
        hp = defoHp;
        isEat = false;
        minusTimeE = eatTime;
    }

    void Update()
    {

        if (notTouch)
        {
            spriteRenderer.sprite = damageSprite;
                tentacle.SetActive(false);
            minusTime -= Time.deltaTime;
            if (minusTime <= 0)
            {
                spriteRenderer.sprite = defaultSprite;
                minusTime = notTouchLimit;
                notTouch = false;
                
            }
        }


        if (isEat)
        {
            spriteRenderer.sprite = eatSprite;
                tentacle.SetActive(false);
            minusTimeE -= Time.deltaTime;
            if (minusTimeE<=0)
            {
                spriteRenderer.sprite = defaultSprite;
                minusTimeE = eatTime;
                isEat = false;
            }
        }

        //itemが100になると残機アップ
        if (item >= 100)
        {
            life++;
            item -= 100;
        }



        if (isBullet)
        {
            minusTimeB -= Time.deltaTime;
            if (minusTimeB <= 0.0f)
            {
                isBullet = false;
                minusTimeB = bulletTime;
            }
        }
        else
        {
            //攻撃処理
            if (Input.GetKeyDown(KeyCode.Space) && !isBullet)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                isBullet = true;
            }
        }

        if (isTentacle && !notTouch && !isEat)
        {
            tentacle.SetActive(true);
            minusTimeT -= Time.deltaTime;
            if (minusTimeT <= 0.0f)
            {
                tentacle.SetActive(false);
                spriteRenderer.sprite = defaultSprite;
                isTentacle = false;
                minusTimeT = tentacleTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && !isTentacle && !notTouch && !isEat)
            {
                spriteRenderer.sprite = attackSprite;
                isTentacle = true;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //アイテム取得
        if (collision.tag == "Item1")
        {
            item++;
            Score.ItemScore1();
            isEat = true;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Item2")
        {
            item += 5;
            Score.ItemScore2();
            isEat = true;
            Destroy(collision.gameObject);
        }

        //敵に触れるとＨＰが減る。
        //ＨＰが無くなると残機が減る。
        //残機が無くなるとゲームオーバー
        if ((collision.tag == "Enemy" || collision.tag=="EnemyBullet" )&& !notTouch)
        {
            hp--;
            notTouch = true;
            if (hp <= 0)
            {
                hp = defoHp;
                life--;
                //シーンをリセットする
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneIndex);
                if (life < 0)
                {
                    //ゲームオーバー
                    life = defoLife;
                    hp = defoHp;
                    item = 0;
                    SceneManager.LoadScene("GameOver");

                }
            }
        }
    }

    //UIに表示させるために取得させる
    public static int GetItem()
    {
        return item;
    }
    public static int GetHp()
    {
        return hp;
    }
    public static int GetLife()
    {
        return life;
    }
    public bool IsNotTouch()
    {
        return notTouch;
    }
}
