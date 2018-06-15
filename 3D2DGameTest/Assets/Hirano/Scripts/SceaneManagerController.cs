using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceaneManagerController : SingletonMonoBehaviour<SceaneManagerController>
{
    public Image BlackImage;
    public Image WhiteImage;
    private string NowScene;
    private float alfa = 1.0f;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void NowSceneManagement()
    {
        NowScene = SceneManager.GetActiveScene().name;
        if (NowScene == "Title")
        {
            if (alfa >= 0.0f)
            {
                FadeOut();
            }
            //BFFALO　２番キーを押したら
            if (Input.GetButtonDown("JoyStick1"))
            {
                SceneManager.LoadScene("StageSelect");
            }
        }
    }

    public void FadeOut()
    {
        alfa -= 0.2f * Time.deltaTime;
        WhiteImage.color = new Color(255, 255, 255, alfa);
    }
}
