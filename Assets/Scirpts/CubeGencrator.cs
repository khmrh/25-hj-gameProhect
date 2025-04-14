using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGencrator : MonoBehaviour
{

    public GameObject cubePrefab;       //������ ť�� ������
    public int totalCubes = 10;         //�� ������ ť�� ����
    public float cubeSacing = 1.0f;     //ť�� ����

    // Start is called before the first frame update
    void Start()
    {
        Gencube();
    }

    public void Gencube()
    {
        Vector3 myPosition = transform.position;        //��ũ��Ʈ�� ���� ������Ʈ�� ��ġ
        GameObject firstCube = Instantiate(cubePrefab, myPosition, Quaternion.identity); //ù���� ť�� ����

        for (int i = 1; i < totalCubes; i++)
        {
            //�� ��ġ���� z������ ���� ���� ������ ��ġ�� ����
            Vector3 Position = new Vector3(myPosition.x, myPosition.y, myPosition.z + (i * cubeSacing));
            Instantiate(cubePrefab, Position, Quaternion.identity); //ť�������
        }
    }   
}
