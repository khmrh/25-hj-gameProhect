using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeGameUI : MonoBehaviour
{
    public TextMeshProUGUI timerTxet;       //ui선언
    public float Timer;                     //타이머 선언

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        timerTxet.text = "생존시간 :" + Timer.ToString("0.00");
    }
}
