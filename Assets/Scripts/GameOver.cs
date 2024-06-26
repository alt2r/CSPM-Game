using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button backToMenu;
    void Start()
    {
        Button btn = backToMenu.GetComponent<Button>();  //the button component that is attached to the button object
        btn.onClick.AddListener(BackToMenu);

        string playerName = " ";//Menu.GetPlayerName();
        if(!File.Exists(Constants.PLAYERS_FILE_NAME))
        {
            File.Create(Constants.PLAYERS_FILE_NAME);
        }
        using(StreamWriter sw = new StreamWriter(Constants.PLAYERS_FILE_NAME, append: true))
        {
            sw.WriteLine(playerName + "," + Player.instance.getScore()); //name,score
        }
    }

    void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);

    }
}
