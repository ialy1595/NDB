using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadStage : MonoBehaviour {


	public void loadScece(string stageName)
    {
        GameManager.gm.currentStageScene = stageName;
        GameManager.gm.SetSE((int)SE.SEType.Click_Cute);
        //GameManager.ResetGM();
        if (GameManager.gm.roundCount != 0) // 팅~
            Application.Quit();
        if (stageName == "Stage1")
            GameManager.gm.GetComponentInChildren<StageSetting>().Stage1Start();
        else
            GameManager.gm.GetComponentInChildren<StageSetting>().Stage2Start();
        SceneManager.LoadScene(stageName);
    }
}
