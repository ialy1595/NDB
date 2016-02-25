using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public GameObject GodFreedom_Box;
    public GameObject GodBug_Box;
    public GameObject GodDemo_Box;

    public GameObject FirstEmergency_Box;
    public GameObject ShutDownJe_Box;
    public GameObject ViolenceTest_Box;
    public GameObject FreeServer_Box;
    public GameObject Stage1Clear_Box;

    public GameObject NormalMessage_Box;

    public static GameObject InterRoundTutorialBox;
    public static GameObject DaramUpgradeTutorialBox;
    public static GameObject FirstEmergencyBox;
    public static GameObject BugTutorialBox;
    private bool[] isStageOnceLoaded = new bool[2];


    public GameObject Tutorial1_Box;
    public GameObject Tutorial2_Box;
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
            gm.EventCheck += GodFreedom;
            gm.EventCheck += GodBug;
            gm.EventCheck += GodDemo;

            gm.EventCheck += ViolenceTest;
            gm.EventCheck += FreeServer;

            gm.RoundStartEvent += SlimeParty;

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

    void GodFreedom()
    {
        if (Random.value < 1f / 12001f)
        {
            gm.fame += 3000;
            Instantiate(GodFreedom_Box);
            LogText.WriteLog("갓나무가 방대한 자유도로 인기를 끌고 있다.");
            gm.EventCheck -= GodFreedom;
        }
    }

    void GodBug()
    {
        int diff = gm.fame - gm.enemyFame;
        if (diff > 8000 && Random.value < 1f / (float)(50001 - 3 * diff))
        {
            gm.enemyFame = gm.fame - 6000;
            Instantiate(GodBug_Box);
            LogText.WriteLog("갓나무가 버그의 발생에도 불구하고 인기를 끌고 있다.");
            gm.EventCheck -= GodBug;
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
}
