using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextItem : MonoBehaviour {
    [SerializeField]
    private Text itemText;
    private int item=0;


    void Update()
    {
        item = Player.GetItem();
        itemText.text = "素材:" + item;
    }
}
