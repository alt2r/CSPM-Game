using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button startGame;

    [SerializeField]
    InputField enterName;
    void Start()
    {
        Button btn = startGame.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        Debug.Log("gmaing time");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
