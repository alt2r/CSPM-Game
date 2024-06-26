using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Button startGame;
    [SerializeField] GameObject enterName;
    [SerializeField] TMP_Text leaderboard;
    static string playerName = "";
    public static string GetPlayerName()
    {
        return playerName;
    }
    void Start()
    {
        Button btn = startGame.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);

        enterName.GetComponent<TMP_InputField>().onValueChanged.AddListener(OnTextChanged);
        PopulateLeaderboard();
    }

    void StartGame()
    {
        playerName = enterName.GetComponent<TMP_InputField>().text;
        if(!File.Exists(Constants.PLAYERS_FILE_NAME))
        {
            File.Create(Constants.PLAYERS_FILE_NAME);
        }
        using(StreamWriter sw = new StreamWriter(Constants.PLAYERS_FILE_NAME, append: true))
        {
            sw.WriteLine(playerName + "," + 0); //name,score
        }

        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
        return;
    }

    void Update()
    {
        
    }

    void OnTextChanged(string name)
    {
        if(name.Length > 10)
        {
            enterName.GetComponent<TMP_InputField>().text = name.Substring(0, 10);
        }
    }

    void PopulateLeaderboard()
    {
        string leaderboardText = "";
        if(!File.Exists(Constants.PLAYERS_FILE_NAME))
        {
            return;
        }
        using(StreamReader sr = new StreamReader(Constants.PLAYERS_FILE_NAME))
        {
            string line = "";
            while((line = sr.ReadLine()) != null)
            {
                leaderboardText = leaderboardText + line + "\n";
            }
        }

        leaderboard.text = leaderboardText;


    }
    
}
