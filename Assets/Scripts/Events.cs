using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour {

    private GameManager gm; //많이 쓸것같아서 만들어둠

    public GameObject UnlockUpBasic_Box;
    public GameObject UserLimitExcess_Box;
    public GameObject RivalGameRelease_Box;
    public GameObject MacroEvent_Box;
    public GameObject TreeOfSavior_Box;
    public GameObject GettingFamous_Box;
    public GameObject DaramUpDownTutorial_Box;
    public GameObject SlimeParty_Box;
    public GameObject SlimeParty_Box2;
    public GameObject SlimeParty_Slime;

    public GameObject GodLaunch_Box;
    public GameObject GodTaunt_Box;
    public GameObject GodFreedom_Box;
    public GameObject GodBug_Box;
    public GameObject GodPassedBy_Box;
    public GameObject GodKiri_Box;
    public GameObject GodLifeGoesOn_Box;
    public GameObject GodDdos_Box;
    public GameObject GodAttackCount_Box;


    public GameObject GodDemo_Box;

    public GameObject FirstEmergency_Box;
    public GameObject ShutDownJe_Box;
    public GameObject ViolenceTest_Box;
    public GameObject FreeServer_Box;
    public GameObject Stage1Clear_Box;

    public GameObject Stage2Clear_Box;

    public GameObject NewClassChoice_Box;


    public GameObject NormalMessage_Box;

    public static GameObject InterRoundTutorialBox;
    public static GameObject DaramUpgradeTutorialBox;
    public static GameObject FirstEmergencyBox;
    public static GameObject BugTutorialBox;
    private bool[] isStageOnceLoaded = new bool[2];


    public GameObject Tutorial1_Box;
    public GameObject Tutorial2_Box;
    public GameObject S2_Tutorial1_Box;
    public GameObject BugTutorial_Box;
    public GameObject InterRoundTutorial_Box;
    public GameObject DaramUpgradeTutorial_Box;
    public GameObject EmergencyTutorial_Box;
    public GameObject VarietyTutorial_Box;


    void Start()
    {
        gm = GameManager.gm;
        BugTutorialBox = BugTutorial_Box;
        DaramUpgradeTutorialBox = DaramUpgradeTutorial_Box;
        InterRoundTutorialBox = InterRoundTutorial_Box;
        FirstEmergencyBox = FirstEmergency_Box;

        gm.UserChange += UserLimitExcess;   // 하드리밋보다는 조금 여유롭게 하기 위해 1초에 한번 체크 (버그도 있음)
        //gm.EventCheck += RivalGameRelease; //갓나무 하나만
        gm.EventCheck += MacroEvent;
        
        gm.EventCheck += GettingFamous;
        

    }

    void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "ChooseStages" || SceneManager.GetActiveScene().name == "Credit") return;
        if (gm.currentStageScene == "Stage1" && !isStageOnceLoaded[0])
        {
            gm.EventCheck += TreeOfSavior;
            gm.EventCheck += UnlockUpBasic;
            gm.EventCheck += EmergencyTutorial;
            gm.RoundStartEvent += DaramUpDownTutorial;
            gm.RoundStartEvent += Tutorial1;

            isStageOnceLoaded[0] = !isStageOnceLoaded[0];
        }
        else if (gm.currentStageScene == "Stage2" && !isStageOnceLoaded[1])
        {
            gm.enemyFame = 0;

            gm.EventCheck += GodTaunt;
            gm.EventCheck += GodFreedom;
            gm.EventCheck += GodBug;
            gm.EventCheck += GodDemo;
            gm.EventCheck += GodDdos;
            gm.EventCheck += GodAttackCount;
            gm.EventCheck += GodPassedBy;

            gm.EventCheck += ViolenceTest;
            gm.EventCheck += FreeServer;
            gm.EventCheck += CheckConstantDecrease;

            gm.RoundStartEvent += GodLaunch;

            gm.RoundStartEvent += SlimeParty;

            gm.EventCheck += Stage2Clear;

            isStageOnceLoaded[1] = !isStageOnceLoaded[1];
        }
    }

    public void UnlockUpBasic()
    {
        if (GameManager.gm.fame >= 10000)
        {
            GameManager.gm.EventCheck -= UnlockUpBasic;
 
            Instantiate(UnlockUpBasic_Box);
            LogText.WriteLog("인기에 힘입어 LV.2 다람쥐를 개발했다!");
            UserChat.CreateChat("GM: 고레벨 다람쥐가 새롭게 등장합니다!!", 5);
            
            gm.FameChange += gm.FameDaram2;
            gm.UserChange += gm.UserLevel2;
            gm.EventCheck += UserChat.uc.Daram2Number;
            Unlockables.SetBool("UnlockBasic2", true);
            
        }
    }

    void UserLimitExcess() {
        if (GameManager.gm.UserAllCount() > Unlockables.GetInt("UserLimit") + 50) {

            Instantiate(UserLimitExcess_Box);
            LogText.WriteLog("서버가 게임의 인기를 감당하지 못하고 폭파되었습니다.");
            
            
            //유저채팅 추가
            gm.UserChat += UserChat.uc.UserLimitExcess;

            GameManager.gm.userCount[User.level1] -= (int)( GameManager.gm.userCount[User.level1] * Random.Range(0.3f, 0.5f));
            GameManager.gm.userCount[User.level2] -= (int)(GameManager.gm.userCount[User.level2] * Random.Range(0.3f, 0.5f));
            GameManager.gm.fame -= (int)(GameManager.gm.fame * 0.2 );

        }
    }

    void RivalGameRelease() {
        if (Random.value < 1f/14401f) {
            Instantiate(RivalGameRelease_Box);
            LogText.WriteLog("경쟁작 '전설의 어둠'이 베타 테스트를 시작했다!");
            UserChat.CreateChat("전설의 어둠하러 갑시다.", 5);
            UserChat.CreateChat("ㄱㄱㄱ", 5);
            UserChat.CreateChat("이 게임 접으려는데 아이디 사실 분?", 5);

            GameManager.gm.userCount[User.level1] -= 1500 + (int)(GameManager.gm.userCount[User.level1] * 0.1f);
            GameManager.gm.userCount[User.level2] -= (int)(GameManager.gm.userCount[User.level2] * 0.1f);

            gm.EventCheck -= RivalGameRelease;
        }
    }

    void MacroEvent()
    {
        if (gm.UserAllCount() >= 10000 && Random.value < 1f/6001f)
            Instantiate(MacroEvent_Box);
    }

    void TreeOfSavior()
    {
        if (gm.time >= 200)
        {
            Instantiate(TreeOfSavior_Box);
            UserChat.CreateChat("우리 모두 갓나무 하러 갑시다!!", 4);
            UserChat.CreateChat("우리 모두 갓나무 하러 갑시다!!", 4);
            gm.EventCheck -= TreeOfSavior;
        }
    }

    void GettingFamous()
    {
        if (gm.fame >= 30000)
        {
            Instantiate(GettingFamous_Box);
            gm.userCount[User.level1] += 1500;
            gm.userCount[User.level2] += 100;
            LogText.WriteLog("게임이 유명해지고 있다!!");
            UserChat.CreateChat(UserChat.GoodChat("와와"), 2);
            UserChat.CreateChat(UserChat.GoodChat("와와"), 3);
            UserChat.CreateChat(UserChat.GoodChat("와와"), 2);
            gm.EventCheck -= GettingFamous;
        }
    }

    public void DaramUpDownTutorial()
    {
        if (Unlockables.GetBool("UnlockBasic1_Amount10") || Unlockables.GetBool("UnlockBasic2_Amount10"))
        {
            Instantiate(DaramUpDownTutorial_Box);
            gm.RoundStartEvent -= DaramUpDownTutorial;
        }
    }

    
    void SlimeParty()
    {
        if(gm.roundCount == 3)
        {
            // 다람쥐와 슬라임을 스왑함
            SlimeParty_Slime.GetComponent<Daram>().Type = "Basic";
            GameObject temp = GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram;
            GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram = SlimeParty_Slime;
            SlimeParty_Slime = temp;

            LogText.WriteLog("정기점검 중 뭔가 문제가 있었던 것 같습니다..");
            Instantiate(SlimeParty_Box);
            gm.EventCheck += SlimeParty2;
            gm.RoundStartEvent -= SlimeParty;
        }
    }

    private bool SP2Started = false;
    void SlimeParty2()
    {
        if (SP2Started == false && gm.isInterRound == false)
        {
            UserChat.CreateChat("아니 이게 무슨..!", 2);
            UserChat.CreateChat("메이플 하고싶다", 3);
            UserChat.CreateChat(UserChat.BadChat("내 다람쥐 어디갔어!!"), 2);
            SP2Started = true;
        }
        if (gm.timeLeft <= 1)
        {
            // 다람쥐와 슬라임을 스왑함
            GameObject temp = GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram;
            GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram = SlimeParty_Slime;
            SlimeParty_Slime = temp;
            SlimeParty_Slime.GetComponent<Daram>().Type = "Slime";

            gm.EventCheck -= SlimeParty2;
            Instantiate(SlimeParty_Box2);

            Unlockables.SetBool("UnlockSlime1", true);
            Unlockables.SetBool("UnlockSlime1_Amount10", true);
            Unlockables.SetBool("UnlockMush2", true);
            Unlockables.SetBool("UnlockMush2_Amount10", true);

        }
    }
 
    /* Tutorials */

    public void Tutorial1()
    {
        Instantiate(Tutorial1_Box);
        gm.RoundStartEvent -= Tutorial1;
        gm.EventCheck += Tutorial2;
    }

    public void Tutorial2()
    {
        if (Daram.All.Count < 10)
        {
            gm.timeLeft = gm.basicTime;
        }

        else
        {
            Instantiate(Tutorial2_Box);
            gm.EventCheck -= Tutorial2;
        }
    }

    private int ETRound;
    public void EmergencyTutorial()
    {
        if (gm.fame >= 15000 && 15 <= gm.timeLeft && gm.timeLeft <= 25)
        {
            EFFame = gm.fame;
            gm.FameChange += EmergencyFame;
            gm.DaramDeath += EmergencyDeath;
            StartCoroutine("ET");
            gm.EventCheck -= EmergencyTutorial;
            gm.EventCheck += VarietyTutorial;
            ETRound = gm.roundCount;
        }
    }

    private static bool alternative = false;
    public static void EmergencyDeath()
    {
        if (alternative)
        {
            if (Daram.All.Count != 0)
                Daram.All[Random.Range(0, Daram.All.Count)].HP -= 99999999;
            alternative = false;
        }
        else
            alternative = true;
    }

    private static int EFFame = 0;
    public static void EmergencyFame()
    {
        EFFame -= 3;
        GameManager.gm.fame = EFFame;
    }

    IEnumerator ET()
    {
        yield return new WaitForSeconds(0.5f);
        Unlockables.SetBool("Emergency", true);
        Instantiate(EmergencyTutorial_Box);
    }

    void VarietyTutorial()
    {
        if (gm.UserAllCount() >= 10000 && gm.roundCount != ETRound)
        {
            Unlockables.SetBool("UnlockRainbow1", true);
            Unlockables.SetBool("UnlockRainbow2", true);
            UpgradeDatabase ud = GameObject.Find("Database").GetComponent<UpgradeDatabase>();
            ud.upgradeDatabase.Add(new Upgrade("LV.1 무지개 다람쥐 많이 뿌리기", 5, 3000, 1, "Rainbow Lv.1 다람쥐를 한번에 10마리씩 뿌릴 수 있는 능력이 추가됩니다.", "UnlockRainbow1_Amount10"));
            ud.upgradeDatabase.Add(new Upgrade("LV.2 무지개 다람쥐 많이 뿌리기", 6, 5000, 1, "Rainbow Lv.2 다람쥐를 한번에 10마리씩 뿌릴 수 있는 능력이 추가됩니다.", "UnlockRainbow2_Amount10"));
        
        Instantiate(VarietyTutorial_Box);
            gm.EventCheck -= VarietyTutorial;
            gm.EventCheck += Stage1Clear;
        }
    }

    void ShutDownJe()
    {
        if (Random.value < 1f / 7201f)
        {
            gm.ChangeMoneyInRound(-3000);
            LogText.WriteLog("게임에 셧다운제가 도입되었습니다.");
            UserChat.CreateChat("안녕 나 셧다운제야..", 3);
            UserChat.CreateChat("여성부 OUT!", 0.5f);  // 판사님 저는 아무것도 보지 못했습니다
            Instantiate(ShutDownJe_Box);
            gm.EventCheck -= ShutDownJe;
        }
    }

    void ViolenceTest()
    {
        if (Random.value < 1f / 7201f)
        {
            gm.fame += 5000;
            gm.userCount[User.level1] += 1000;
            LogText.WriteLog("폭력성 실험을 통해 게임의 인지도가 상승했다!");
            UserChat.CreateChat("이게 그 유명한 " + gm.GameName + "인가요??", 3);
            Instantiate(ViolenceTest_Box);
            gm.EventCheck -= ViolenceTest;
            gm.EventCheck += ShutDownJe;
        }
    }

    void FreeServer()
    {
        if (gm.userCount[User.level2] > 10000 && Random.value < 1f / 7201f)
        {
            gm.fame -= 5000;
            gm.userCount[User.level2] -= 4000;
            LogText.WriteLog("프리서버가 생겼다는 소문이 퍼지고 있다.");
            UserChat.CreateChat("여러분 현질 필요없는 게임이 생겼대요!!", 3);
            UserChat.CreateChat(UserChat.BadChat("슬슬 이 게임도 뜰 때가 됬나.."), 4);
            Instantiate(FreeServer_Box);
            gm.EventCheck -= FreeServer;
        }
    }

    public void Stage1Clear()
    {
        if (gm.UserAllCount() >= 15000)
        {
            Instantiate(Stage1Clear_Box);
            gm.EventCheck -= Stage1Clear;
        }
    }

    private int count = 0;
    private int prevFame = 0;
    private float CCDCool = 0;
    void CheckConstantDecrease()
    {
        int deltafame = gm.fame - prevFame;
        prevFame = gm.fame;

        if (CCDCool > gm.time)
            return;

        if (deltafame < -6)
            count += 2;
        else if (deltafame < -3)
            count++;
        else if (deltafame < 2)
            count--;
        else if (deltafame < 5)
            count -= 3;
        else
            count = 0;
        if (count < 0)
            count = 0;

        if (count >= 600)
        {
            Instantiate(NewClassChoice_Box);
            count = 0;
            CCDCool = gm.time + 41; // 라운드마다 최대 한번
        }
    }

    void GodLaunch()
    {
        Instantiate(GodLaunch_Box);
        LogText.WriteLog("경쟁작 갓나무가 런칭했다.");
        UserChat.CreateChat("새로 나온 게임이 있다던데요?", 5);
        UserChat.CreateChat("갓나무 하러 갑시다", 5);
        gm.RoundStartEvent -= GodLaunch;

        StartCoroutine(GodLaunch_Effect());
    }

    IEnumerator GodLaunch_Effect() // 특정 시간 동안 정해진 시간마다 유저 수 하락, 갓나무 인기도 증가율 향상
    {
        float startTime = gm.time;
        float duration = 15f;
        int minusUser = 10;
        int fameIncrease = 20;
        while (true)
        {
            if (!gm.isPaused && !gm.isInterRound)
            {
                gm.userCount[User.level1] = Mathf.Max(gm.userCount[User.level1] - minusUser, 0);
                gm.enemyFame += fameIncrease;
            }
            yield return new WaitForSeconds(0.1f);

            if (gm.time - startTime > duration) break;
        }
        LogText.WriteLog("갓나무 신규 런칭 이벤트가 종료되었습니다.");
    }

    void GodTaunt()
    {
        if (gm.timeLeft < 5)
        {
            Instantiate(GodTaunt_Box);
            gm.EventCheck -= GodTaunt;
        }
    }

    void GodFreedom()
    {
        if (gm.roundCount == 2 && gm.timeLeft < 20)
        {
            Instantiate(GodFreedom_Box);
            LogText.WriteLog("갓나무가 방대한 자유도로 인기를 끌고 있다.");
            gm.EventCheck -= GodFreedom;

            StartCoroutine(GodFreedom_Effect());
        }
    }

    IEnumerator GodFreedom_Effect()
    {
        float startTime = gm.time;
        float duration = 5f;
        int fameIncrease = 10;
        while (true)
        {
            if(!gm.isPaused && !gm.isInterRound)
                gm.enemyFame += fameIncrease;
            yield return new WaitForSeconds(0.1f);

            if (gm.time - startTime > duration) break;
        }
    }

    void GodBug()
    {
        if (gm.roundCount == 3 && gm.timeLeft < 15)
        {
            Instantiate(GodBug_Box);
            LogText.WriteLog("갓나무가 버그의 발생에도 불구하고 인기를 끌고 있다.");
            gm.EventCheck -= GodBug;

            StartCoroutine(GodBug_Effect());
        }
    }

    IEnumerator GodBug_Effect()
    {
        float startTime = gm.time;
        float duration = 10f;
        int fameIncrease = 15;

        while (true)
        {
            if(!gm.isPaused && !gm.isInterRound)
                gm.enemyFame += fameIncrease;
            yield return new WaitForSeconds(0.1f);

            if (gm.time - startTime > duration) break;
        }

    }

    void GodPassedBy()
    {
        if (gm.fame - gm.enemyFame < 0)
        {
            Instantiate(GodPassedBy_Box);
            LogText.WriteLog("갓나무가 " + gm.GameName + "의 인기를 위협합니다!");
            UserChat.CreateChat(gm.GameName + "보다 갓나무가 더 재밌다던데?", 4);
        }
        gm.EventCheck -= GodPassedBy;
        gm.EventCheck += GodKiri;
    }

    void GodKiri()
    {
        int enemyMinusFame = 3000;
        if (gm.fame - gm.enemyFame < -5000)
        {
            Instantiate(GodKiri_Box);
            LogText.WriteLog("갓나무의 인기도가 하락하고 있습니다.");
            UserChat.CreateChat("헐 갓나무 왜 저럼?", 3);
            UserChat.CreateChat(gm.GameName + " 계속 해야겠네", 3);

            gm.enemyFame -= enemyMinusFame;
        }
        gm.EventCheck -= GodKiri;
        gm.EventCheck += GodLifeGoesOn;
    }

    void GodLifeGoesOn()
    {
        if (gm.fame - gm.enemyFame < -10000 || gm.enemyFame > 50000)
        {
            Instantiate(GodLifeGoesOn_Box);
        }
        gm.EventCheck -= GodLifeGoesOn;
        gm.EventCheck -= Stage2Clear;
    }

    void GodDdos()
    {
        if (gm.fame - gm.enemyFame > 20000)
        {
            Instantiate(GodDdos_Box);
            gm.EventCheck -= GodDdos;
            StartCoroutine(GodDdos_Effect());
        }

    }

    IEnumerator GodDdos_Effect()
    {
        float startTime = gm.time;
        float duration = 15f;
        float multiConstant = 0.75f;

        GameManager.gm.bugResponeTimeMin *= multiConstant;
        GameManager.gm.bugResponeTimeMax *= multiConstant;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (gm.time - startTime > duration) break;
        }

        GameManager.gm.bugResponeTimeMin *= 1.0f / multiConstant;
        GameManager.gm.bugResponeTimeMax *= 1.0f / multiConstant;
    }

    void GodAttackCount()
    {
        if (gm.GetComponentInChildren<ItemDatabase>().attackItemUseCount >= 5)
        {
            Instantiate(GodAttackCount_Box);
            gm.EventCheck -= GodAttackCount;
            StartCoroutine(GodDdos_Effect());
        }
    }


    private int GDThreshold = 1;
    void GodDemo()
    {
        int diff = gm.enemyFame - gm.fame;
        if (diff > 2000 + 2000*GDThreshold && Random.value < 1f / (float)(55501 + 10000 * GDThreshold - 5 * diff))
        {
            gm.enemyFame = gm.fame + 1000 * GDThreshold;
            Instantiate(GodDemo_Box);
            LogText.WriteLog("갓나무 유저들의 불만이 증가하고 있다.");
            GDThreshold++;
        }
    }

    void Stage2Clear()
    {
        if (gm.fame > 50000)
        {
            Instantiate(Stage2Clear_Box);
            gm.EventCheck -= Stage2Clear;
        }
    }
}
