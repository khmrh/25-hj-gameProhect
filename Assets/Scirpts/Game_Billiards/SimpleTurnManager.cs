using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurnManager : MonoBehaviour
{
    //전역 변수 (모든 공이 공유)
    public static bool canPlay = true;
    public static bool anyBallMoving = false;

    // Update is called once per frame
    void Update()
    {
        CheckAllBalls();

        if (!anyBallMoving && !canPlay)
        {
            canPlay = true;
            Debug.Log("턴 종료! 다시 칠수 있습니다!");
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
        Debug.Log("턴 시작! 공이 멈출떄까지 기다리세요!");
    }

}
