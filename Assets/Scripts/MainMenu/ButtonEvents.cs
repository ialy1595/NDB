using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour {

    public GameObject SetGameNameBox;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        if (SaveLoad.HasSave())
        {
            bool successed = SaveLoad.Load();
            if (successed)
            {
                SceneManager.LoadScene("ChooseStages");

            }
            else
            {
                SetGameNameBox.SetActive(true);
            }
        }
        else
        {
            SetGameNameBox.SetActive(true);
            
        }
        
    }

    public void GameCredit()
    {
        SceneManager.LoadScene("Credit");
    }
}
