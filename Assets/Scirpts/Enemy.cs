using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 100;            //ü���� ���� �Ѵ�.(����)
    public float Timer = 1.0f;          //Ÿ�̸� ������ �����Ѵ�
    public int AttackPiont = 50;        //���ݷ��� �����Ѵ�
    // ���� �������� ������Ʈ �Ǳ� �� �ѹ� ���� �ȴ�.
    void Start()
    {
        Health += 100;                   //�� ��ũ��Ʈ�� ���� �� �� 100�� �� �÷��ش�.
    }

    // ���� �������� �� ������ ���� ȣ��ȴ�.
    void Update()
    {
        CharacterHealthUp();
        if (Input.GetKeyDown(KeyCode.Space)) //�����̽� Ű�� ��������
        {
            Health -= AttackPiont;         //ü�� ����Ʈ�� ���� ����Ʈ ��ŭ ���� ���� �ش�
        }

        checkDeath();
    }
    void CharacterHealthUp()
    {
        Timer -= Time.deltaTime;        //�ð��� �� �����Ӹ��� ���� �ñ��(deltaTinm ������ ������ �ð��� �ǹ��մϴ�)
                                        //(Timer = Timer - Time.deltaTime)
        if (Timer <= 0)                 //���� Timer �� ��ġ�� 0 ���Ϸ� ��������� 
        {
            Timer = 1;                  //�ٽ� 1�ʷ� Ÿ�̸Ӹ� �ʱ�ȭ �����ش�
            Health -= 10;               //1�ʸ��� ü���� 10 �÷��ش�
        }
    }
    public void CharacterHit(int Danmage)                      //�������� �޴� �Լ��� ���� �Ѵ�
    {
        Health -= Danmage;                              //���� ���ݷ¿� ���� ü���� ���ҽ�Ų��
    }
    void checkDeath()
    {
        if (Health <= 0)                   //ü���� 0 ������ ���           
        {
            Destroy(gameObject);            //�� ������Ʈ�� �ı� ��Ų��
        }
    }
}
