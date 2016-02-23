using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
//using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour {

    public GameObject SetGameNameBox;

    public void ExitGame()
    {
        GameManager.gm.SetSE((int)SE.SEType.Click_Cute);
        Application.Quit();
    }

    public void GameStart()
    {
        if (SaveLoad.HasSave())
        {
            bool successed = SaveLoad.Load();
            if (successed)
            {
                GameManager.gm.SetSE((int)SE.SEType.Click_Cute);
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
        GameManager.gm.SetSE((int)SE.SEType.Click_Cute);
        SceneManager.LoadScene("Credit");
    }
}
