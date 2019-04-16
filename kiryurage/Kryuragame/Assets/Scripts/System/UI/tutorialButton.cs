using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialButton : MonoBehaviour {
    private bool push;
    private AudioSource audio;
    private float soundTime=0.5f;
    private bool ispush;
    void Start()
    {
        push = false;
        audio = GetComponent<AudioSource>();
        ispush = false;
    }

    private void Update()
    {
        if (ispush)
        {
            soundTime -= Time.deltaTime;
            if(soundTime<=0)
            {
                push = true;
            }
        }
    }

    public void Push()
    {
        audio.PlayOneShot(audio.clip);
        ispush = true;
    }

    public bool IsPush()
    {
        return push;
    }
}
