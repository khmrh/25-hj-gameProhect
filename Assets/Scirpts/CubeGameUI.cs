using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeGameUI : MonoBehaviour
{
    public TextMeshProUGUI timerTxet;       //ui����
    public float Timer;                     //Ÿ�̸� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        timerTxet.text = "�����ð� :" + Timer.ToString("0.00");
    }
}
