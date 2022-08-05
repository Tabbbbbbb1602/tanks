using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startButton;
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(loadGame);
    }

    private void loadGame()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
