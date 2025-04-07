using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxLives = 3;                                //�ִ� �����
    public int currentLives = 1;                            //���� �����

    public float invincibleTime = 1.0f;                     //�ǰ��� �����ð�
    public bool isinvincible = false;                       //���� ������ ��


    void Start()
    {
        currentLives = maxLives;                            //����� �ʱ�ȭ
    }

    private void OnTriggerEnter(Collider other)             //Ʈ���� ���� �ȿ� ���Դٸ� �����ϴ� �Լ�
    {
        //�̻��ϰ� �浹 �˻�
        if (other.CompareTag("Missile"))
        {
            currentLives--;                                 //�̻��ϰ� �浹�� 1�� ������� �����Ѵ�
            Destroy(other.gameObject);                      //�̻��� ������Ʈ�� �����Ѵ�

            if (currentLives == 0)                          //���� ü���� 0 �����ϰ��
            {
                GameOver();                                 //���ӿ��� �Լ�ó��
            }
        }
    }

    public void GameOver()                                     //���ӿ���ó��
    {
        gameObject.SetActive(false);                        //3���� ���� �� �����
        Invoke("RestartGame", 3.0f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);         //����� �� ����
    }
}
