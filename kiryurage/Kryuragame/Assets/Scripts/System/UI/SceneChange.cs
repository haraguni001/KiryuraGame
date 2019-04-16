using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour {
    [SerializeField]
    private string sceneName;
    private AudioSource audio;
    private float soundTime = 0.5f;
    private bool ispush;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        ispush = false;
    }
    private void Update()
    {
        if (ispush)
        {
            soundTime -= Time.deltaTime;
            if (soundTime <= 0)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    //ボタンをクリックするとsceneNameで指定したシーンに飛ぶ
    public void Push()
    {
        audio.PlayOneShot(audio.clip);
        ispush = true;
    }
}
