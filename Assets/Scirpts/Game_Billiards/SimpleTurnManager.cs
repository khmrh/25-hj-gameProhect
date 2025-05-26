using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurnManager : MonoBehaviour
{
    //���� ���� (��� ���� ����)
    public static bool canPlay = true;
    public static bool anyBallMoving = false;

    // Update is called once per frame
    void Update()
    {
        CheckAllBalls();

        if (!anyBallMoving && !canPlay)
        {
            canPlay = true;
            Debug.Log("�� ����! �ٽ� ĥ�� �ֽ��ϴ�!");
        }
    }

    void CheckAllBalls()
    {
        SimpleBallContoller[] allBalls = FindObjectOfType<SimpleBallContoller>();
        anyBallMoving = false;

        foreach (SimpleBallContoller ball in allBalls)
        {
            if (Ball.isMoving())
            {   
                anyBallMoving = true;
                break;
            }
        }
    }

    public static void OnBallHit()
    {
        canPlay = false;
        Debug.Log("�� ����! ���� ���⋚���� ��ٸ�����!");
    }

}
