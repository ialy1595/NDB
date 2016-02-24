using UnityEngine;
using System.Collections;

public class StageSetting : MonoBehaviour {

    // 각 스테이지의 초기조건을 지정해주는 스크립트
    // 기존에 흩어져 있던 것들을 여기로 옮길 예정

    public GameObject Stage2StartScene;

    private GameManager gm;

    void CommonSettings()   // 스테이지 공통 설정
    {
        Unlockables.SetBool("UnlockBasic1", true);
        Unlockables.SetBool("UnlockBasic1_Amount1", true);
        Unlockables.SetBool("UnlockBasic2_Amount1", true);
        Unlockables.SetBool("UnlockRainbow1_Amount1", true);

        Unlockables.SetInt("Basic1Health", 100);
        Unlockables.SetInt("Basic2Health", 500);
        Unlockables.SetInt("Rainbow1Health", 100);
    }

    public void Stage1Start()
    {
        CommonSettings();

        Unlockables.SetInt("Server", 1);
        Unlockables.SetInt("ServerEff", 5000);

    }

    public void Stage2Start()
    {
        gm = GameManager.gm;
        CommonSettings();
        
        Unlockables.SetBool("UnlockBasic2", true);
        Unlockables.SetBool("UnlockBasic1_Amount10", true);
        Unlockables.SetBool("UnlockBasic2_Amount10", true);
        Unlockables.SetBool("Emergency", true);

        Unlockables.SetInt("Server", 5);
        Unlockables.SetInt("ServerEff", 5000);

        gm.fame = 10000;
        gm.userCount[User.level1] = 12000;
        gm.userCount[User.level2] = 3000;
        gm.initialMoney = 30000;
        gm.StartScene = Stage2StartScene;

        gm.FameChange += gm.FameDaram2;
        gm.UserChange += gm.UserLevel2;
        gm.EventCheck += UserChat.uc.Daram2Number;
        Vector2 pos = GameManager.gm.RandomPosition();
    }

}
