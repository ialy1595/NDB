using UnityEngine;
using System.Collections;

public class Macro : MonoBehaviour {

    private float ActivityEnd;
    private float NextActivity = 0;

    public GameObject MacroWork;

    void Start()
    {
        GetComponent<EventBox>().DisableHotkey = true;
    }

    public void KillMacroByMoney()
    {
        if (GameManager.gm.Money() >= 3000)
        {
            GameManager.gm.ChangeMoneyInRound(-3000);
            GameManager.gm.fame += 1000;
            LogText.WriteLog("돈을 투입해 열심히 매크로를 잡았다.");
            GetComponentInParent<EventBox>().OnClick();   
        }
        else
            GameManager.gm.ShowMessageBox("돈이 부족합니다.");
    }
    public void KillMacroByGM()
    {
        // 버그 담당 GM이 일할 곳. 수정바람.
        Post modifyingDeveloper = Developer.dev.FindPostByPostID(Developer.dev.FindPostIDByName("Debugging"));
        if (Developer.dev.UseDeveloper(modifyingDeveloper))
        {
            LogText.WriteLog("버그gm을 투입해 열심히 매크로를 잡는중...(10초 소요)");
            Instantiate(MacroWork).GetComponent<MacroWork>().modifyingDeveloper = modifyingDeveloper; 
        }
        else
            GameManager.gm.ShowMessageBox("버그gm이 부족합니다.");
    }



    public void KeepMacro()
    {
        GameManager.gm.fame -= 1000;
        // 디버깅 팀의 개발자 한 명당 매크로 지속시간이 10초씩 줄어듭니다.
        ActivityEnd = GameManager.gm.time + 100f;// -10f * (float)Mathf.Min(10, Developer.dev.developerCount[Developer.dev.FindPostIDByName("Debugging")]);
        Debug.Log(ActivityEnd);
        Debug.Log(GameManager.gm.time);
        GameManager.gm.DaramDeath += MacroActivity;
        LogText.WriteLog("매크로가 게임에 판을 치고 있다.");
    }
    
    void MacroActivity()
    {
        if (GameManager.gm.time >= ActivityEnd)
        {
            GameManager.gm.DaramDeath -= MacroActivity;
            GameManager.gm.ShowMessageBox("매크로가 잡혔다.");
        }
        if (GameManager.gm.time >= NextActivity)
        {
            if (Daram.All.Count != 0)
            {
                Daram.All[Random.Range(0, Daram.All.Count)].HP -= 1000;
                NextActivity = GameManager.gm.time + 10.0f / (float)Daram.All.Count;    // 다람쥐가 초당 10% 감소
            }
        }
    }
}
