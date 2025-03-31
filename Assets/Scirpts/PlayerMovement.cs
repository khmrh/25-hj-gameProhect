using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;      //�̵� �ӵ� ���� ����
    public Rigidbody rb;                //�÷��̾� ��ü�� ���� 
    public float JumpForce = 5.0f;

    public bool isGround = true;

    public int coinCount = 0;               //���� �׵� ���� ����
    public int totalCoins = 5;              //�� �׵� ���� �ʿ� ���� ����

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //������ �Է�
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //�ӵ������� ���� �̵�
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGround)        //&& �� ���� true�ϋ� -> (jump��ư{���� �����̽���}�� �� ���� ������
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }

    void OnCollisionEnter(Collision collision)      //�浹�� �Ͼ���� ȣ�� �Ǵ� �Լ�
    {
        if (collision.gameObject.tag == "Ground")           //�浹�� �Ͼ ��ü�� Tag�� Ground�� ���
        {
            isGround = true;                                     //���� �浹 ������ true�� �������ش�
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
        if (other.CompareTag("Door"))
    }
}
