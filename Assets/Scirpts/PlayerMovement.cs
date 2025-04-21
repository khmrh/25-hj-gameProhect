using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("기본이동설정")]
    public float moveSpeed = 5.0f;      //이동 속도 변수 설정
    public Rigidbody rb;                //플레이어 강체를 선언 
    public float JumpForce = 7.0f;
    public float trunSpeed = 10f;

    [Header("점프 개선 설정")]
    public float fallMultiplayer = 2.5f;
    public float lowflallMutiplayer = 2.0f;

    [Header("지면 감지 설정")]
    public float coyoteTime = 0.15f;        //지면 관성 시간
    public float coyoteTImeCounter;         //관성 타이머
    public bool realGrouned = true;         //실제 지면 상태

    public bool isGround = true;

    public int coinCount = 0;               //코인 휙득 변수 선언
    public int totalCoins = 5;              //총 휙득 코인 필요 변수 선언

    // Start is called before the first frame update
    void Start()
    {
        coyoteTImeCounter = 0; totalCoins = 0;          //관성 타이머 초기화
    }

    // Update is called once per frame
    void Update()
    {
        //지면 감지 활성화
        UpdateGrounedSlalc();


        //움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //이동방향 벡터
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);            //입력 방향 감지

        if (movement.magnitude > 0.1f)          //입력이 있을떄 회전
        {
            Quaternion targetRotaion = Quaternion.LookRotation(movement);           //이동 방향을 바라보도록
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotaion, trunSpeed * Time.deltaTime);
        }

        //속도값으로 집접 이동
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGround)        //&& 두 값이 true일떄 -> (jump버튼{보통 스페이스바}와 땅 위에 있을떄
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGround = false;
        }

        //착지 점프 높이 구현
        if (rb.velocity.y < 0)                  //하강 시에
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;         //하강 시에 중력 강화
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))                                              //상승중 점프 버튼으 ㄹ때면 낮게 점프
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowflallMutiplayer - 1) * Time.deltaTime;
        }

    }

    void OnCollisionEnter(Collision collision)      //충돌이 일어났을떄 호출 되는 함수
    {
        if (collision.gameObject.tag == "Ground")           //충돌이 일어난 불체의 Tag가 Ground인 경우
        {
            realGrouned = true;                                     //땅과 충돌 했을때 true로 변경해준다
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            realGrouned = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            realGrouned = false;
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
        if (other.CompareTag("Door") && coinCount >= totalCoins)
        {
            Debug.Log("게임클리어");
            //이후 완료 연출 및 Scene 전환 한다
        }
    }

    //지면 상태 업데이트 함수
    void UpdateGrounedSlalc()
    {
        if (realGrouned)
        {
            coyoteTImeCounter = coyoteTime;
            isGround = true;
        }
        else
        {
            //실제로는 지면에 없지만 코요테 타임 내에 있ㅇ면 여전히 지면으로 판단
            if (coyoteTImeCounter > 0)
            {
                coyoteTImeCounter = Time.deltaTime;
            }
            else
            {
                isGround = false;
            }
        }
    }
}
