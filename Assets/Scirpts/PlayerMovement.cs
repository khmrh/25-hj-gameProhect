using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;      //이동 속도 변수 설정
    public Rigidbody rb;                //플레이어 강체를 선언 
    public float JumpForce = 5.0f;

    public bool isGround = true;

    public int coinCount = 0;               //코인 휙득 변수 선언
    public int totalCoins = 5;              //총 휙득 코인 필요 변수 선언

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //속도값으로 집접 이동
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGround)        //&& 두 값이 true일떄 -> (jump버튼{보통 스페이스바}와 땅 위에 있을떄
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }

    void OnCollisionEnter(Collision collision)      //충돌이 일어났을떄 호출 되는 함수
    {
        if (collision.gameObject.tag == "Ground")           //충돌이 일어난 불체의 Tag가 Ground인 경우
        {
            isGround = true;                                     //땅과 충돌 했을때 true로 변경해준다
        }
    }

    private void OnTriggerEnter(Collider other)         //트리거 영억 안에 들어왔다를 감시하는 함수
    {
        //코인수집
        if (other.CompareTag("Coin"))                   //코인 트리거와 충돌하면
        {
            coinCount++;                //코인 변수를 1 증가시킨다
            Destroy(other.gameObject);                      //  
            Debug.Log($"코인 수집 : {coinCount}/{totalCoins}");
        }
        if (other.CompareTag("Door"))
    }
}
