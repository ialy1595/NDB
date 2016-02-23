using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonEvents : MonoBehaviour {

    public GameObject SetGameNameBox;

    public void ExitGame()
    {
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
