using UnityEngine;
using System.Collections;

public class StageSetting : MonoBehaviour {

    // 각 스테이지의 초기조건을 지정해주는 스크립트
    // 기존에 흩어져 있던 것들을 여기로 옮길 예정

    public GameObject Stage2StartScene;

    private GameManager gm;

    void CommonSettings()   // 스테이지 공통 설정
    {
        gm = GameManager.gm;

        Unlockables.SetBool("UnlockBasic1", true);
        Unlockables.SetBool("UnlockBasic1_Amount1", true);
        Unlockables.SetBool("UnlockBasic2_Amount1", true);
        Unlockables.SetBool("UnlockRainbow1_Amount1", true);
        Unlockables.SetBool("UnlockRainbow2_Amount1", true);
        Unlockables.SetBool("UnlockSlime1_Amount1", true);
        Unlockables.SetBool("UnlockMush2_Amount1", true);

        Unlockables.SetInt("Basic1Health", 100);
        Unlockables.SetInt("Basic2Health", 500);
        Unlockables.SetInt("Rainbow1Health", 300);
        Unlockables.SetInt("Rainbow2Health", 1000);
        Unlockables.SetInt("Slime1Health", 700);
        Unlockables.SetInt("Mush2Health", 2000);
        Unlockables.SetInt("Tokki1Health", 1000);
        Unlockables.SetInt("Tokki2Health", 3500);

        gm.bugResponeTimeMin = 8.0f;
        gm.bugResponeTimeMax = 12.0f;
        gm.roundCount = 0;


    }

    public void Stage1Start()
    {
        CommonSettings();
        gm = GameManager.gm;
        Unlockables.SetInt("Server", 1);
        Unlockables.SetInt("ServerEff", 5000);
        Unlockables.SetBool("RivalGameOn", false);
        
    }

    public void Stage2Start()
    {
        gm = GameManager.gm;
        CommonSettings();
        
        Unlockables.SetBool("UnlockBasic2", true);
        Unlockables.SetBool("UnlockRainbow1", true);
        Unlockables.SetBool("UnlockRainbow2", true);
        Unlockables.SetBool("UnlockBasic1_Amount10", true);
        Unlockables.SetBool("UnlockBasic2_Amount10", true);
        Unlockables.SetBool("UnlockRainbow1_Amount10", true);
        Unlockables.SetBool("UnlockRainbow2_Amount10", true);
        Unlockables.SetBool("Emergency", true);
        Unlockables.SetBool("RivalGameOn", true);

        Unlockables.SetInt("Basic1Health", 200);
        Unlockables.SetInt("Basic2Health", 750);

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

        UpgradeDatabase ud = GameObject.Find("Database").GetComponent<UpgradeDatabase>();
        ud.RemoveUpgrade(2);
        ud.RemoveUpgrade(3);
        ud.RemoveUpgrade(4);
        ud.RemoveUpgrade(5);
        ud.RemoveUpgrade(6);

        ud.upgradeDatabase.Add(new Upgrade("Lv.2 기본 다람쥐 체력 증가", 2, 2000, 1, "Basic Lv.2 다람쥐의 체력을 250 증가시킵니다.", "Basic2Health", "현재 최대 체력", 250, 2000.0f));
        ud.upgradeDatabase.Add(new Upgrade("Lv.1 무지개 다람쥐 체력 증가", 3, 1500, 1, "Rainbow Lv.1 다람쥐의 체력을 100 증가시킵니다.", "Rainbow1Health", "현재 최대 체력", 100, 1500.0f));
        ud.upgradeDatabase.Add(new Upgrade("Lv.2 무지개 다람쥐 체력 증가", 4, 3000, 1, "Rainbow Lv.2 다람쥐의 체력을 400 증가시킵니다.", "Rainbow2Health", "현재 최대 체력", 400, 3000.0f));
    }

}
