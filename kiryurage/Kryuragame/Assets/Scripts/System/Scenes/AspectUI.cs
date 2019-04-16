using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectUI : MonoBehaviour {
    private Camera cam;
    //固定したいサイズ
    [SerializeField]
    private float width=1600f;
    [SerializeField]
    private float height=1000f;
    //画像のPixel Per Unit
    private float PPU = 200f;
    void Awake () {
        float aspect = (float)Screen.height / (float)Screen.width; //表示画面のアスペクト比
        float bgAspect = height / width; //理想とするアスペクト比
        cam = GetComponent<Camera>();
        cam.orthographicSize = (height / 2f / PPU);

        if (bgAspect > aspect)
        {
            //横に広い時
            float bgScale = height / Screen.height;
            //viewport rectの幅
            float camWidth = width / (Screen.width * bgScale);
            //viewportRect設定
            cam.rect = new Rect((1f-camWidth)/2f,0f,camWidth,1f);
        }
        else
        {
            //縦に広い時
            float bgScale = aspect / bgAspect;
            cam.orthographicSize *= bgScale;
            cam.rect = new Rect(0f, 0f, 1f, 1f);
        }
	}
	
	void Update () {
		
	}
}
