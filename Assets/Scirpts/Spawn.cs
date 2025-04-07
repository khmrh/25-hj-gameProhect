using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject coinPrefabs;
    public GameObject MissilePrefabs;

    [Header("���� Ÿ�̹� ����")]
    public float minspawninterval = 0.5f;
    public float maxspawninterval = 2.0f;


    [Header("���� ���� Ȯ�� ����")]
    [Range(0, 100)]
    public int coinSpawnChace = 50;

    public float timer = 0.0f;
    public float nextSpawntime;
    // Start is called before the first frame update
    void Start()
    {
        SetnextSpawnTime();                                                     //�Լ�ȣ��
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;                                                  //�ð��� 0���� ���� �����Ѵ�

        if (timer >= nextSpawntime)                                             //���� �ð��� �Ǹ� ������Ʈ�� ���� �Ѵ�
        {
            SpawnObject();
            timer = 0.0f;                                                           //�ð��� �ʱ�ȭ �����ش�
            SetnextSpawnTime();                                                          //�ٽ� �Լ� ȣ��
        }
    }

    void SetnextSpawnTime()
    {
        nextSpawntime = Random.Range(minspawninterval, maxspawninterval);           //�ּ�-�ִ� ������ ������ �ð� ����
    }

    void SpawnObject()
    {
        Transform spawnTransform = transform;                                       //������ ������Ʈ�� ��ġ�� ȸ������ �����´�

        //Ȯ���� ���� ���� �Ǵ� �̻��� ����
        int randomValue = Random.Range(0, 100);                                     //0-100������ ���� ���� �Ǥä��Ƴ���
        if (randomValue < coinSpawnChace)                                           //0~coinspawnchace
        {
            Instantiate(coinPrefabs, spawnTransform.position, spawnTransform.rotation);
        }
        else
        {
            Instantiate(MissilePrefabs, spawnTransform.position, spawnTransform.rotation);
        }

    }
}
