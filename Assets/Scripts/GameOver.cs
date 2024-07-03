using System.IO;
using TMPro;
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
        if(player.getEasyMode())
        {
            statsDisplay.text = Constants.YOU_DIED_TEXT + player.GetPlayerName() + Constants.SCORE_TEXT + player.GetScore();
        }
        else
        {
            statsDisplay.text = Constants.YOU_DIED_TEXT + player.GetPlayerName() + Constants.SCORE_TEXT + (player.GetScore() * 2);
        }
    }

    void BackToMenu()
    {
        using(StreamWriter sw = new StreamWriter(Constants.SCORES_FILE_PATH, true))
        {
            if(player.getEasyMode())
            sw.WriteLine(player.GetPlayerName() + "," + player.GetScore());
            else
            sw.WriteLine(player.GetPlayerName() + "," + (player.GetScore() * 2));
        }
        SceneManager.LoadSceneAsync(Constants.SceneNames.MenuScene.ToString(), LoadSceneMode.Single);

    }
}
