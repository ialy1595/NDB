using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour {

	public void BackToMainMenu()
    {
        GameManager.gm.SetSE((int)SE.SEType.Click_Cute);
        SceneManager.LoadScene("MainMenu");
    }
}
