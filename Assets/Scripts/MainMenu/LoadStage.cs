using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadStage : MonoBehaviour {


	public void loadScece(string stageName)
    {
        GameManager.gm.currentStageScene = stageName;
        GameManager.gm.SetSE((int)SE.SEType.Click_Cute);
        SceneManager.LoadScene(stageName);
    }
}
