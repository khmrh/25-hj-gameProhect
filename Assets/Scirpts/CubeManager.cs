using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public CubeGencrator[] generatedCubes = new CubeGencrator[5];

    public float timer = 0.0f;
    public float interval = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            RandomizeCubeActivation();
            timer = 0.0f;
        }
    }

    public void RandomizeCubeActivation()
    {
        for (int i = 0; i < generatedCubes.Length; i++)
        {
            int randomNum = Random.Range(0, 2);
            if (randomNum == 1)
            {
                generatedCubes[i].Gencube();
            }
        }
    }
}
