using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitObj : MonoBehaviour
{
    public Image image;

    private Color c;

    public float life_time = 7.5f; //消滅時刻

    public float nextTime = 4.5f; //点滅開始時刻
    public float interval = 1.0f; //点滅周期

    float time = 0f;
   



    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();

       

        time = 0;
        
    }



    // Update is called once per frame
    void Update()
    {
        //指定時間で削除
        time += Time.deltaTime;
        print(time);
        if (time > life_time)
        {
            Destroy(gameObject);
        }

        //一定時間ごとに点滅
        if (Time.time > nextTime)
        {
            float alpha = image.GetComponent<CanvasRenderer>().GetAlpha();
            if (alpha == 1.0f)
                image.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            else
                image.GetComponent<CanvasRenderer>().SetAlpha(1.0f);

            nextTime += interval;
        }

    }

    
}
