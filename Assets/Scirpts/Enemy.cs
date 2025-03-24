using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 100;            //체력을 선언 한다.(정수)
    public float Timer = 1.0f;          //타이머 변수를 선언한다
    public int AttackPiont = 50;        //공격력을 선언한다
    // 최초 프레임이 업데이트 되기 전 한번 실행 된다.
    void Start()
    {
        Health += 100;                   //이 스크립트가 실행 될 때 100을 더 올려준다.
    }

    // 게임 진행중인 매 프레임 마다 호출된다.
    void Update()
    {
        CharacterHealthUp();
        if (Input.GetKeyDown(KeyCode.Space)) //스페이스 키를 눌럿을때
        {
            Health -= AttackPiont;         //체력 포인트를 공격 포인트 만큼 감소 시켜 준다
        }

        checkDeath();
    }
    void CharacterHealthUp()
    {
        Timer -= Time.deltaTime;        //시간을 매 프레임마다 감소 시긴다(deltaTinm 프레임 간격의 시간을 의미합니다)
                                        //(Timer = Timer - Time.deltaTime)
        if (Timer <= 0)                 //만약 Timer 의 수치가 0 이하로 내려갈경우 
        {
            Timer = 1;                  //다시 1초로 타이머를 초기화 시켜준다
            Health -= 10;               //1초마다 체력을 10 올려준다
        }
    }
    public void CharacterHit(int Danmage)                      //데미지를 받는 함수를 선언 한다
    {
        Health -= Danmage;                              //받은 공격력에 대한 체력을 감소시킨다
    }
    void checkDeath()
    {
        if (Health <= 0)                   //체력이 0 이하일 경우           
        {
            Destroy(gameObject);            //이 오브젝트를 파괴 시킨다
        }
    }
}
