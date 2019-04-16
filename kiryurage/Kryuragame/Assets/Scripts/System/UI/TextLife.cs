using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLife : MonoBehaviour {
    [SerializeField]
    private Text lifetext;
    private int life=0;

    void Update()
    {
        life = Player.GetLife();
        lifetext.text = "残機:" + life;
    }
}
