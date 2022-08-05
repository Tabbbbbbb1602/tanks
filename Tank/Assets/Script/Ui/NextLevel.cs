using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public Button nextLvlBtn;
    public Button tryAgain;
   /* public Button finish;*/
 
    // Start is called before the first frame update
    void Start()
    {
        nextLvlBtn.onClick.AddListener(nextLevel);
        tryAgain.onClick.AddListener(LoadAgain);
       /* finish.onClick.AddListener(finishGame);*/
    }

    private void LoadAgain()
    {
       //SceneManager.GetActiveScene().buildIndex
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   /* private void finishGame()
    {

    }
*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
