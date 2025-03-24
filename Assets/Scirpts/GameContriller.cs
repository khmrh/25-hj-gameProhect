using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContriller : MonoBehaviour
{
    public float Timer = 1.0f;
    public GameObject EnemyObject;
    void Update()
    {
        Timer -= Time.deltaTime;        //시간을 매 프레임마다 감소 시긴다(deltaTinm 프레임 간격의 시간을 의미합니다)
                                        //(Timer = Timer - Time.deltaTime)
        if (Timer <= 0)                 //만약 Timer 의 수치가 0 이하로 내려갈경우 
        {
            Timer = 1;                  //다시 1초로 타이머를 초기화 시켜준다
            
            GameObject Temp = Instantiate(EnemyObject);
            Temp.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
        }


        if (Input.GetMouseButtonDown(0))                                        //마우스 버튼을 눌렀을때
        {
            RaycastHit hit;                                                     //물리 hit선언
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        //카메라에서 Ray를 쏴서 3d공간상의 물체를 확인한다

            if (Physics.Raycast(ray, out hit))                                  //Ray를 쐇을때 hit되는 물체가 있으면 
            {
                if (hit.collider != null)                                       //물체가 존재하면
                {
                    //Debug.Log($"hit : {hit.collider.name}");                    //물체 이름을 출력한다
                    hit.collider.gameObject.GetComponent<Enemy>().CharacterHit(30);     //Enemy스크립트의 히트 함수를 호출한다    
                }

            }
        }
    }
}
