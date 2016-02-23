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
    public GameObject SlimeParty_Slime;
    public GameObject FirstTutorial_Box;
    public GameObject InterRoundTutorial_Box;
    public GameObject EmergencyTutorial_Box;
    public GameObject VarietyTutorial_Box;
    public GameObject FirstEmergency_Box;
    public GameObject ShutDownJe_Box;

    public GameObject NormalMessage_Box;

    public static GameObject InterRoundTutorialBox;
    public static GameObject FirstEmergencyBox;

    void Start ()
    {
        gm = GameManager.gm;
        InterRoundTutorialBox = InterRoundTutorial_Box;
        FirstEmergencyBox = FirstEmergency_Box;

        gm.EventCheck += UnlockUpBasic;
        gm.EventCheck += UserLimitExcess;
        gm.EventCheck += RivalGameRelease;
        gm.EventCheck += MacroEvent;
        gm.EventCheck += TreeOfSavior;
        gm.EventCheck += GettingFamous;
        gm.EventCheck += EmergencyTutorial;
        gm.EventCheck += ShutDownJe;

        gm.RoundStartEvent += DaramUpDownTutorial;
        gm.RoundStartEvent += SlimeParty;
        gm.RoundStartEvent += FirstTutorial;
    }

    void UnlockUpBasic()
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
        if (GameManager.gm.UserAllCount() > Unlockables.GetInt("UserLimit")) {

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
        if (gm.UserAllCount() >= 10000 && Random.value < 1f/4201f)
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
            UserChat.CreateChat(UserChat.GoodChat("와와"), 2);
            UserChat.CreateChat(UserChat.GoodChat("와와"), 3);
            UserChat.CreateChat(UserChat.GoodChat("와와"), 2);
            gm.EventCheck -= GettingFamous;
        }
    }

    void DaramUpDownTutorial()
    {
        if (Unlockables.GetBool("UnlockBasic1_Amount10") || Unlockables.GetBool("UnlockBasic2_Amount10"))
        {
            Instantiate(DaramUpDownTutorial_Box);
            gm.RoundStartEvent -= DaramUpDownTutorial;
        }
    }

    private bool SPStarted = false;
    void SlimeParty()
    {
        if(false) //if (gm.roundCount == 2)  // 2스테이지 어딘가에 쓸 예정
        {
            // 다람쥐와 슬라임을 스왑함
            GameObject temp = GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram;
            GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram = SlimeParty_Slime;
            SlimeParty_Slime = temp;

            Instantiate(SlimeParty_Box);
            SPStarted = true;
        }
        if (SPStarted == true && gm.isInterRound == false)
        {
            UserChat.CreateChat("아니 이게 무슨..!", 2);
            UserChat.CreateChat("메이플 하고싶다", 3);
            UserChat.CreateChat(UserChat.BadChat("내 다람쥐 어디갔어!!"), 2);
            SPStarted = false;
        }
        if(false) //if(gm.roundCount == 3)  // 3라운드에 해제
        {
            // 다람쥐와 슬라임을 스왑함
            GameObject temp = GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram;
            GameObject.Find("AddBasicDaram").GetComponent<AddBasicDaram>().daram = SlimeParty_Slime;
            SlimeParty_Slime = temp;

            gm.RoundStartEvent -= SlimeParty;
        }
            
    }

    void FirstTutorial()
    {
        Instantiate(FirstTutorial_Box);
        gm.RoundStartEvent -= FirstTutorial;
    }

    private int ETRound;
    void EmergencyTutorial()
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
            Instantiate(VarietyTutorial_Box);
            gm.EventCheck -= VarietyTutorial;
        }
    }

    void ShutDownJe()
    {
        if (false)
        {
            gm.ChangeMoneyInRound(-3000);
            UserChat.CreateChat("안녕 나 셧다운제야..", 3);
            UserChat.CreateChat("여성부 OUT!", 0.5f);  // 판사님 저는 아무것도 보지 못했습니다
            Instantiate(ShutDownJe_Box);
            gm.EventCheck -= ShutDownJe;
        }
    }
}
