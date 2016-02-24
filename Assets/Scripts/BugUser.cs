using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BugUser : MonoBehaviour {
    
    public static List<BugUser> Bugs = new List<BugUser>();
    private bool fixing = false;
    public int fixTime;
    private int fixStartTime;
    private int startTime;
    private Post modifyingDeveloper = Developer.dev.FindPostByPostID(Developer.dev.FindPostIDByName("Debugging"));
    private Animator Anim;

     void Start () {
        Anim = GetComponent<Animator>();
        startTime = GameManager.gm.timeLeft;
        Bugs.Add(this);
    }

	void FixedUpdate () {
        if (fixing)
        {
            int nowTime = GameManager.gm.timeLeft;
            if (fixStartTime - nowTime >= fixTime)
            {
                Developer.dev.FinishDeveloper(modifyingDeveloper);
                Bugs.Remove(this);
                Destroy(gameObject);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.gm.isPaused)
            {        
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                //충돌한 오브젝트가 이 오브젝트랑 일치할때
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (Developer.dev.UseDeveloper(modifyingDeveloper))
                    {
                        fixStartTime = GameManager.gm.timeLeft;
                        Anim.SetTrigger("Fix");
                        GameManager.gm.SetSE((int)SE.SEType.Click_Retro);
                        fixing = true;
                    }
                    else
                    {
                        //사용 가능한 개발자가 없다고 메시지점
                        GameManager.gm.ShowMessageBox("사용 가능한 GM이 없습니다.");
                    }
                }
                
            }
        }
	}
    
    public int LiveTime()
    {
        if (fixing) return 0;
        int nowTime = GameManager.gm.timeLeft;
        return startTime - nowTime;
    }

}
