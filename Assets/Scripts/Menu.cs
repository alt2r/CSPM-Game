using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Threading;

public class Menu : MonoBehaviour
{
    [SerializeField] Button startGame;
    [SerializeField] GameObject enterName;
    [SerializeField] TMP_Text leaderboard;
    static string playerName = "";
    void Start()
    {
        Button btn = startGame.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);

        enterName.GetComponent<TMP_InputField>().onValueChanged.AddListener(OnTextChanged);
        PopulateLeaderboard();
    }

    void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

        Constants.SceneNames n;
        int i = 10;
        do{
            Enum.TryParse(SceneManager.GetActiveScene().name, out n);
            Debug.Log(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            Thread.Sleep(500);
            i--;
        } while(n != Constants.SceneNames.GameScene && i > 0);

        new GameSceneInit(playerName);
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
