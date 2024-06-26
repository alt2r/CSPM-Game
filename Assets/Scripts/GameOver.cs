using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button backToMenu;
    void Start()
    {
        Button btn = backToMenu.GetComponent<Button>();  //the button component that is attached to the button object
        btn.onClick.AddListener(BackToMenu);
    }

    void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);

    }
}
