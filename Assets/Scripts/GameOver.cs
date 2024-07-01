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

        statsDisplay.text = Constants.YOU_DIED_TEXT + player.GetPlayerName() + Constants.SCORE_TEXT + player.GetScore();
    }

    void BackToMenu()
    {
        using(StreamWriter sw = new StreamWriter(Constants.SCORES_FILE_PATH, true))
        {
            sw.WriteLine(player.GetPlayerName() + "," + player.GetScore());
        }
        SceneManager.LoadSceneAsync(Constants.SceneNames.MenuScene.ToString(), LoadSceneMode.Single);

    }
}
