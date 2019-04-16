using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialUI : MonoBehaviour {
    [SerializeField, Header("次のページ")]
    private string next;
    [SerializeField,Header("前のページ")]
    private string mae;
    [SerializeField]
    private tutorialButton buttonA;
    [SerializeField]
    private tutorialButton buttonB;

    void Update() {
        if(buttonA.IsPush())
        {
            SceneManager.LoadScene(next);
        }
        if(buttonB.IsPush())
        {
            SceneManager.LoadScene(mae);
        }
    }
}
