using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndInterRound : MonoBehaviour {

    public void OnClick()
    {
        if (Unlockables.GetInt("Server") <= 1)
        {
            GameManager.gm.ShowMessageBox("서버를 구매해주세요");
            return;
        }
        SceneManager.LoadScene(GameManager.gm.currentStageScene);
    }
}
