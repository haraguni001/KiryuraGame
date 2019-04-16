using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHp : MonoBehaviour {
    [SerializeField]
    private Text hpText;
    private int hp=0;


    void Update()
    {
        hp = Player.GetHp();
        hpText.text = "HP:" + hp;
	}
}
