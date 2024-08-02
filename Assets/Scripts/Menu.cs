using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] Button startGame;
    [SerializeField] GameObject enterName;
    [SerializeField] TMP_Text leaderboard;
    [SerializeField] Button quit;
    [SerializeField] GameObject enterNameErrorMessage;
    static string playerName = "";

    public static string GetPlayerName()
    {
        return playerName;
    }
    void Start()
    {
        startGame.onClick.AddListener(StartGame);
        quit.onClick.AddListener(Quit);
        enterName.GetComponent<TMP_InputField>().onValueChanged.AddListener(OnTextChanged);
        PopulateLeaderboard();
        return;
    }

    void StartGame()
    {
        playerName = enterName.GetComponent<TMP_InputField>().text;
        if (playerName != "")
        {
            enterNameErrorMessage.SetActive(false);
            //load the game scene and close this one
            SceneManager.LoadSceneAsync(Constants.SceneNames.GameScene.ToString(), LoadSceneMode.Single);
        }
        else
        {
            enterNameErrorMessage.SetActive(true);
        }
        return;
    }
    void Quit()
    {
        Application.Quit();
        return;
    }

    void OnTextChanged(string name)
    {
        //cool funciton that does not allow names longer than Constants.PLAYER_NAME_MAXIMUM_LENGTH characters (currently 15)
        //when the user enters the 16th character it will replace that string with the first 15 chars of that string
        if (name.Length > Constants.PLAYER_NAME_MAXIMUM_LENGTH)
        {
            enterName.GetComponent<TMP_InputField>().text = name.Substring(0, Constants.PLAYER_NAME_MAXIMUM_LENGTH);
        }
        return;
    }

    void PopulateLeaderboard()
    {
        if (!File.Exists(Constants.PLAYERS_FILE_NAME))
        {
            File.Create(Constants.PLAYERS_FILE_NAME);
            leaderboard.text = "";
        }
        using (StreamReader sr = new StreamReader(Constants.PLAYERS_FILE_NAME))
        {
            string? line;
            string[] splitString;
            string textToAdd = "";
            List<string[]> playersAndScores = new List<string[]>();
            while ((line = sr.ReadLine()) != null)
            {
                splitString = line.Split(",");
                playersAndScores.Add(splitString);
            }
            List<string[]> sortedScores = mergeSort(playersAndScores);
            int numberOfPlayersOnScoreBoard = 0;
            for (int i = sortedScores.Count - 1; i >= 0 && numberOfPlayersOnScoreBoard < Constants.NUMBER_OF_LEADERBOARD_ENTRIES; i--)
            {
                textToAdd += $"{sortedScores[i][0]}: {sortedScores[i][1]} \n";
                numberOfPlayersOnScoreBoard++;
            }
            leaderboard.text = textToAdd;
        }
        return;
    }

    public static List<string[]> mergeSort(List<string[]> array)
    {
        List<string[]> left = new List<string[]>();
        List<string[]> right = new List<string[]>();
        List<string[]> result;
        if (array.Count <= 1)
        {
            return array;
        }
        int midPoint = array.Count / 2; ;
        for (int i = 0; i < midPoint; i++)
        {
            left.Add(array[i]);
        }
        int x = 0;
        for (int i = midPoint; i < array.Count; i++)
        {
            right.Add(array[i]);
            x++;
        }
        left = mergeSort(left);
        right = mergeSort(right);
        result = merge(left, right);
        return result;
    }
    public static List<string[]> merge(List<string[]> left, List<string[]> right)
    {
        List<string[]> result = new List<string[]>();
        int indexLeft = 0, indexRight = 0;
        while (indexLeft < left.Count || indexRight < right.Count)
        {
            if (indexLeft < left.Count && indexRight < right.Count)
            {
                if (Convert.ToInt32(left[indexLeft][1]) <= Convert.ToInt32(right[indexRight][1]))
                {
                    result.Add(left[indexLeft]);
                    indexLeft++;
                }
                else
                {
                    result.Add(right[indexRight]);
                    indexRight++;
                }
            }
            else if (indexLeft < left.Count)
            {
                result.Add(left[indexLeft]);
                indexLeft++;
            }
            else if (indexRight < right.Count)
            {
                result.Add(right[indexRight]);
                indexRight++;
            }
        }
        return result;
    }
}
