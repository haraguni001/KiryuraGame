using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemGenerator : MonoBehaviour {
    [SerializeField]
    private GameObject[] enemyGeneral; //発生させるWave
    [SerializeField]
    private GameObject bossGeneral; //ボスのWave
    private GameObject GeneObj;
    private int generalNumber;  //発生させるWave
    private int count;  //Waveが消えるとWaveを再生させる
    [SerializeField]
    private int countWave; //これが0になるとボスのWaveになる
    private bool boss;  //ボスのWaveが出たかどうか
    void Start()
    {
        boss = false;
    }

	void Update ()
    {
        count = transform.childCount;
        if (count == 0)
        {
            if (countWave <= 0)
            {
                GeneObj = (GameObject)Instantiate(bossGeneral, transform.position, transform.rotation);
                GeneObj.transform.parent = transform;
                boss = true;
                if (boss)
                {
                    Destroy(this.gameObject);
                }
                return;
            }
            generalNumber = Random.Range(0, enemyGeneral.Length);
           GeneObj= (GameObject)Instantiate(enemyGeneral[generalNumber], transform.position, transform.rotation);
            GeneObj.transform.parent = transform;
            countWave--;

        }
	}
}
