using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text _text;
    public GameObject nextLevel;
    public GameObject finish;

    public static bool isNextLevel;
    public static bool isFinish;
    int scoreCount = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        nextLevel.SetActive(false);
        finish.SetActive(false);
    }

    private void OnEnable()
    {
        EnemyManager.onDeath += UpdateScore;
        GunController.onDeathEnemy += DisplayMenu;
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        scoreCount++;
        _text.text = $"Point:  { scoreCount } ";
        if(scoreCount == 5)
        {
            nextLevel.SetActive(true);
            
        }
    }

    public void DisplayMenu()
    {
        finish.SetActive(true);
    }

    private void OnDisable()
    {
        EnemyManager.onDeath -= UpdateScore;
        GunController.onDeathEnemy -= DisplayMenu;
    }
}
