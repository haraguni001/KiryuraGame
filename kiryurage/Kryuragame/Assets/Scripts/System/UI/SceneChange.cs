using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour {
    [SerializeField]
    private string sceneName;
    //ボタンをクリックするとsceneNameで指定したシーンに飛ぶ
    public void OnClick()
    {
      SceneManager.LoadScene(sceneName);
    }
}
