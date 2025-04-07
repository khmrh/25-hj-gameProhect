using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject coinPrefabs;
    public GameObject MissilePrefabs;

    [Header("스폰 타이밍 설정")]
    public float minspawninterval = 0.5f;
    public float maxspawninterval = 2.0f;


    [Header("동전 스폰 확률 설정")]
    [Range(0, 100)]
    public int coinSpawnChace = 50;

    public float timer = 0.0f;
    public float nextSpawntime;
    // Start is called before the first frame update
    void Start()
    {
        SetnextSpawnTime();                                                     //함수호출
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;                                                  //시간이 0에서 점점 증가한다

        if (timer >= nextSpawntime)                                             //생성 시간이 되면 오브젝트를 생성 한다
        {
            SpawnObject();
            timer = 0.0f;                                                           //시간을 초기화 시켜준다
            SetnextSpawnTime();                                                          //다시 함수 호출
        }
    }

    void SetnextSpawnTime()
    {
        nextSpawntime = Random.Range(minspawninterval, maxspawninterval);           //최소-최대 사이의 랜덤한 시간 설정
    }

    void SpawnObject()
    {
        Transform spawnTransform = transform;                                       //스포너 오브젝트의 위치와 회전값을 가져온다

        //확률에 따라 동전 또는 미사일 생성
        int randomValue = Random.Range(0, 100);                                     //0-100사이의 랜덤 값을 뽀ㅓㅂ아낸다
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
