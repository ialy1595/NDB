using UnityEngine;
using System.Collections;

public class MacroWork : MonoBehaviour {

    public Post modifyingDeveloper;
    private float FinishTime;

    void Start()
    {
        FinishTime = GameManager.gm.time + 10.0f;
    }

    void Update()
    {
        if (GameManager.gm.time >= FinishTime)
        {
            GameManager.gm.fame += 1000;
            LogText.WriteLog("버그gm을 투입해 열심히 매크로를 잡았다.");
            Developer.dev.FinishDeveloper(modifyingDeveloper);
            Destroy(gameObject);
        }

    }
}
