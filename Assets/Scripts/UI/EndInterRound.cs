using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndInterRound : MonoBehaviour {

    public void OnClick()
    {
        if (Unlockables.GetInt("Server") <= 1 && GameManager.gm.isTutorialCleared[51] == true)
        {
            GameManager.gm.ShowMessageBox("서버를 구매해주세요");
            return;
        }
        
        //버그 있다 잡자
        if (GameManager.gm.isTutorialCleared[50] == true && (Unlockables.GetInt("Basic1Health") <= 100 || Unlockables.GetBool("UnlockBasic1_Amount10") == false))
        {
            GameManager.gm.ShowMessageBox("다람쥐 업그레이드를 해주세요");
            return;
        }

        SceneManager.LoadScene(GameManager.gm.currentStageScene);
    }
}
