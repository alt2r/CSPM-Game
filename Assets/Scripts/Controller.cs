using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject malwareGO;
    [SerializeField]
    private TMP_Text pointsDisplay;
    [SerializeField]
    private TMP_Text playerNameDisplay;
    [SerializeField]
    private TMP_Text livesDisplay;

    private System.Random random = new System.Random();

    private float malwareSpeed = 2;
    private float malwareRadius = 0.5f;
    private float timeDelay = 1.5f;

    void Start()
    {
        Player player = new Player(pointsDisplay, livesDisplay);
        playerNameDisplay.text = Menu.GetPlayerName();
        StartCoroutine(SpawnMalware());
        return;
     }

    IEnumerator SpawnMalware()
    {
        Constants.SceneNames sceneNameAsEnum;
        Enum.TryParse(SceneManager.GetActiveScene().name, out sceneNameAsEnum);
        //basically this is just a much better way of doing while(scenename = "gamescene")
        while(sceneNameAsEnum == Constants.SceneNames.GameScene)
        {
            GameObject thisGO = Instantiate(malwareGO, new Vector3(Constants.ENEMY_SPAWN_DISTANCE, random.Next(-40, 40) / 10.0f, 0), new Quaternion(0, 0, 0, 0));
            MalwareScript malwareScript = thisGO.GetComponent<MalwareScript>();
            malwareScript.init(malwareSpeed, malwareRadius);
            yield return new WaitForSeconds(timeDelay); //run the next iteration of this loop after timedelay seconds
        }

   
    }
}
