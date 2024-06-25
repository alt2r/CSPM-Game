using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject malwareGO;

    private System.Random random = new System.Random();

    private float malwareSpeed = 2;
    private float malwareRadius = 0.5f;
    private float timeDelay = 1.5f;
    private static bool playGame = true;

    public static bool getPlayGame()
    {
        return playGame;
    }
    void Start()
    {
        Player player = new Player();
        StartCoroutine(SpawnMalware());
     }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnMalware()
    {
        while(playGame)
        {
            GameObject thisGO = Instantiate(malwareGO, new Vector3(15, random.Next(-40, 40) / 10.0f, 0), new Quaternion(0, 0, 0, 0));
            MalwareScript malwareScript = thisGO.GetComponent<MalwareScript>();
            malwareScript.init(malwareSpeed, malwareRadius);
            yield return new WaitForSeconds(timeDelay);
        }
   
    }
}
