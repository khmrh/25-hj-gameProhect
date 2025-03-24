using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContriller : MonoBehaviour
{
    public float Timer = 1.0f;
    public GameObject EnemyObject;
    void Update()
    {
        Timer -= Time.deltaTime;        //�ð��� �� �����Ӹ��� ���� �ñ��(deltaTinm ������ ������ �ð��� �ǹ��մϴ�)
                                        //(Timer = Timer - Time.deltaTime)
        if (Timer <= 0)                 //���� Timer �� ��ġ�� 0 ���Ϸ� ��������� 
        {
            Timer = 1;                  //�ٽ� 1�ʷ� Ÿ�̸Ӹ� �ʱ�ȭ �����ش�
            
            GameObject Temp = Instantiate(EnemyObject);
            Temp.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
        }


        if (Input.GetMouseButtonDown(0))                                        //���콺 ��ư�� ��������
        {
            RaycastHit hit;                                                     //���� hit����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        //ī�޶󿡼� Ray�� ���� 3d�������� ��ü�� Ȯ���Ѵ�

            if (Physics.Raycast(ray, out hit))                                  //Ray�� �i���� hit�Ǵ� ��ü�� ������ 
            {
                if (hit.collider != null)                                       //��ü�� �����ϸ�
                {
                    //Debug.Log($"hit : {hit.collider.name}");                    //��ü �̸��� ����Ѵ�
                    hit.collider.gameObject.GetComponent<Enemy>().CharacterHit(30);     //Enemy��ũ��Ʈ�� ��Ʈ �Լ��� ȣ���Ѵ�    
                }

            }
        }
    }
}
