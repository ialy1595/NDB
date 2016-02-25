using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewClassChoice : MonoBehaviour {

    private static int NewClassCost = 5000;

    public GameObject CostText;
    public GameObject YesBox;
    public GameObject NoBox;
    public GameObject NewClassBox;

    private GameManager gm;
    private EventBox box;
    void Start()
    {
        gm = GameManager.gm;
        box = GetComponent<EventBox>();
        box.DisableHotkey = true;
        CostText.GetComponent<Text>().text = "개발 착수 (" + NewClassCost + "G)";
    }

    public void YesClick()
    {
        if (gm.Money() >= NewClassCost)
        {
            gm.ChangeMoneyInRound(-NewClassCost);
            NewClassCost += 5000;
            Instantiate(YesBox);
            box.OnClick();
            gm.FameChange += FameUp_NewClass1;
            gm.FameChange -= gm.FameBug;
            gm.RoundStartEvent += NewClassCreated;
            LogText.WriteLog("개발팀이 신직업 개발에 착수했다!!");
            LogText.WriteLog("신직업이 나온다는 소식만으로 게임의 인기가 상승하고 있다.");
            UserChat.CreateChat("신직업 빨리 나오게 해주세요 ㅠㅠ", 3);
        }
        else
            LogText.WriteLog("돈이 부족합니다.");
    }

    public void NoClick()
    {
        Instantiate(NoBox);
        LogText.WriteLog(gm.GameName + "은(는) 신직업이 없을 것이라고 밝혔다.");
        box.OnClick();
    }

    
    void FameUp_NewClass1()
    {
        float sum = 0;
        foreach (Quadric q in gm.DaramFunction)
            sum += q.value;
        if (sum < 0)
            gm.fame -= ((int)sum - 1);
        else
            gm.fame += 2;
    }

    void NewClassCreated()
    {
        gm.FameChange -= FameUp_NewClass1;
        gm.FameChange += gm.FameBug;
        gm.RoundStartEvent -= NewClassCreated;
        gm.FameChange += FameUp_NewClass2;
        EndTime = gm.time + 25;

        Instantiate(NewClassBox);
        gm.userCount[User.level1] += 3000;
        LogText.WriteLog("신직업이 성공적으로 개발되었다!!");
    }

    private float EndTime = 0;
    void FameUp_NewClass2()
    {
        gm.fame += 3;
        gm.ChangeMoneyInRound(gm.CalculateMoney(0.3f));
        if (gm.time >= EndTime)
            gm.FameChange -= FameUp_NewClass2;
    }
}
