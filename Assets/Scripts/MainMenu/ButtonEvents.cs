using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonEvents : MonoBehaviour {

    public GameObject SetGameNameBox;

    public void ExitGame()
    {
        GameManager.gm.SetSE((int)SE.SEType.Click_Cute);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else 
		Application.Quit();
#endif
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
