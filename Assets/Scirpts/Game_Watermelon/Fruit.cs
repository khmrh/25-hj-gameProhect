using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    //����Ÿ�� (0:���, 1:��纣��, 2: ���ڳ�....) int�� �����
    public int fruitType;

    //������ �̹� ���������� Ȯ���ϴ� �÷���
    public bool hasMerged = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�̹� ������ ������ ����
        if (hasMerged)
            return;

        //�ٸ� ���ϰ� �浹�ߴ��� Ȯ��
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        //�浹�Ѱ��� �����ϰ� Ÿ���� ���ٸ�
        if (otherFruit != null && !otherFruit.hasMerged && otherFruit.fruitType == fruitType)
        {
            //���ƴٰ� ǥ��
            hasMerged = true;
            otherFruit.hasMerged = true;

            //�� ������ �߰� ���
            Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2f;

            //���� �ܰ� ���Ϸ� ���׷��̵� (���� ����)

            //���� ���� ����
            Destroy(otherFruit.gameObject);
            Destroy(gameObject);
        }
    }
}
