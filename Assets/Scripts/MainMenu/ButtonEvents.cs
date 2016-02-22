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
        SetGameNameBox.SetActive(true);
    }

    public void GameCredit()
    {
        SceneManager.LoadScene("Credit");
    }
}
