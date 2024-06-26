using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]Button backToMenu;

    [SerializeField]TMP_Text statsDisplay;

    Player player;
    void Start()
    {
        player = Player.GetInstance();
        Button btn = backToMenu.GetComponent<Button>();  //the button component that is attached to the button object
        btn.onClick.AddListener(BackToMenu);

        statsDisplay.text = "You Died! \n Name: " + player.GetPlayerName() + "\n Score: " + player.GetScore();
    }

    void BackToMenu()
    {
        using(StreamWriter sw = new StreamWriter("players.txt", true))
        {
            sw.WriteLine(player.GetPlayerName() + "," + player.GetScore());
        }
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);

    }
}
