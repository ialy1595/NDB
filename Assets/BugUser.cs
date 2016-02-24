using UnityEngine;
using System.Collections;

public class BugUser : MonoBehaviour {

    private bool fixing = false;
    public int fixTime;
    private int fixStartTime;
    private int startTime;
    private Post modifyingDeveloper = Developer.dev.FindPostByPostID(Developer.dev.FindPostIDByName("Debugging"));
    private Animator Anim;

     void Start () {
        Anim = GetComponent<Animator>();
        startTime = GameManager.gm.timeLeft;
    }

	void FixedUpdate () {
        if (fixing)
        {
            int nowTime = GameManager.gm.timeLeft;
            if (fixStartTime - nowTime >= fixTime)
            {
                Developer.dev.FinishDeveloper(modifyingDeveloper);
                Destroy(gameObject);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                //충돌한 오브젝트가 이 오브젝트랑 일치할때
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (Developer.dev.UseDeveloper(modifyingDeveloper))
                    {
                        Anim.SetTrigger("Fix");
                        fixing = true;
                    }
                    else
                    {
                        //사용 가능한 개발자가 없다고 메시지점
                    }
                }
                
            }
        }
	}
    /*
    //인기도 떨어뜨리기
    private void Bugging()
    {

    }
    */
}
