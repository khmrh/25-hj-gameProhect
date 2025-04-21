using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�⺻�̵�����")]
    public float moveSpeed = 5.0f;      //�̵� �ӵ� ���� ����
    public Rigidbody rb;                //�÷��̾� ��ü�� ���� 
    public float JumpForce = 7.0f;
    public float trunSpeed = 10f;

    [Header("���� ���� ����")]
    public float fallMultiplayer = 2.5f;
    public float lowflallMutiplayer = 2.0f;

    [Header("���� ���� ����")]
    public float coyoteTime = 0.15f;        //���� ���� �ð�
    public float coyoteTImeCounter;         //���� Ÿ�̸�
    public bool realGrouned = true;         //���� ���� ����

    public bool isGround = true;

    public int coinCount = 0;               //���� �׵� ���� ����
    public int totalCoins = 5;              //�� �׵� ���� �ʿ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        coyoteTImeCounter = 0; totalCoins = 0;          //���� Ÿ�̸� �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���� Ȱ��ȭ
        UpdateGrounedSlalc();


        //������ �Է�
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //�̵����� ����
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);            //�Է� ���� ����

        if (movement.magnitude > 0.1f)          //�Է��� ������ ȸ��
        {
            Quaternion targetRotaion = Quaternion.LookRotation(movement);           //�̵� ������ �ٶ󺸵���
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotaion, trunSpeed * Time.deltaTime);
        }

        //�ӵ������� ���� �̵�
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGround)        //&& �� ���� true�ϋ� -> (jump��ư{���� �����̽���}�� �� ���� ������
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGround = false;
        }

        //���� ���� ���� ����
        if (rb.velocity.y < 0)                  //�ϰ� �ÿ�
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;         //�ϰ� �ÿ� �߷� ��ȭ
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))                                              //����� ���� ��ư�� ������ ���� ����
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowflallMutiplayer - 1) * Time.deltaTime;
        }

    }

    void OnCollisionEnter(Collision collision)      //�浹�� �Ͼ���� ȣ�� �Ǵ� �Լ�
    {
        if (collision.gameObject.tag == "Ground")           //�浹�� �Ͼ ��ü�� Tag�� Ground�� ���
        {
            realGrouned = true;                                     //���� �浹 ������ true�� �������ش�
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

    private void OnTriggerEnter(Collider other)         //Ʈ���� ���� �ȿ� ���Դٸ� �����ϴ� �Լ�
    {
        //���μ���
        if (other.CompareTag("Coin"))                   //���� Ʈ���ſ� �浹�ϸ�
        {
            coinCount++;                //���� ������ 1 ������Ų��
            Destroy(other.gameObject);                      //  
            Debug.Log($"���� ���� : {coinCount}/{totalCoins}");
        }
        if (other.CompareTag("Door") && coinCount >= totalCoins)
        {
            Debug.Log("����Ŭ����");
            //���� �Ϸ� ���� �� Scene ��ȯ �Ѵ�
        }
    }

    //���� ���� ������Ʈ �Լ�
    void UpdateGrounedSlalc()
    {
        if (realGrouned)
        {
            coyoteTImeCounter = coyoteTime;
            isGround = true;
        }
        else
        {
            //�����δ� ���鿡 ������ �ڿ��� Ÿ�� ���� �֤��� ������ �������� �Ǵ�
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
