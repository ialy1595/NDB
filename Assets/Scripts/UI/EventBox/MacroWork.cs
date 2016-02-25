using UnityEngine;
using System.Collections;

public class MacroWork : MonoBehaviour {

    private float FinishTime;
    void Start()
    {
        FinishTime = GameManager.gm.wokring
    }

    void GMWorking()
    {
        GameManager.gm.fame += 1000;
        LogText.WriteLog("버그gm을 투입해 열심히 매크로를 잡았다.");
        Developer.dev.FinishDeveloper(modifyingDeveloper);
    }
}
