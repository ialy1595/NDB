﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Developer : MonoBehaviour {

    public static Developer dev;
    private GameManager gm;

    public List<Post> postDatabase = new List<Post>();  // 부서에 대한 정보를 담고 있음;

    [HideInInspector] public int[] developerCount;      // 각 부서에 몇 명의 개발자가 투입되었는지에 대한 정보를 담고 있음

    [HideInInspector] public int hireCost = 1000;
    [HideInInspector] public int fireCost = -700;
    [HideInInspector] public int salaryCost = 0;
    

	void Start () {
        dev = this;
        gm = GameManager.gm;

        // Post(부서명, 영어 부서명, 부서ID, 부서에 속한 개발자 1명당 월급, 설명)
        // 부서ID는 postDatabase, developerCount의 index값과 일치합니다.
        postDatabase.Add(new Post("서버 관리팀", "Server", 0, 300, "개발자 1명당 최대 유저수 제한이 2500씩 증가합니다."));
        postDatabase.Add(new Post("디버깅 팀", "Debugging", 1, 250, "개발자 1명당 매크로, 버그의 지속시간이 10%p씩 줄어듭니다."));
        postDatabase.Add(new Post("홍보 팀", "Publicity", 2, 220, "개발자 1명당 초보 유저수의 증가 속도가 20%p씩 빨라집니다."));
        postDatabase.Add(new Post("고객 지원팀", "Customer", 3, 200, "인기도 하락 속도가 줄어듭니다."));
        postDatabase.Add(new Post("Lv.1 다람쥐 개발팀", "DaramLv1", 4, 180, "개발자 1명당 Lv.1 다람쥐를 3초에 1마리씩 자동으로 뿌립니다."));
        postDatabase.Add(new Post("Lv.2 다람쥐 개발팀", "DaramLv2", 5, 360, "개발자 1명당 Lv.2 다람쥐를 3초에 1마리씩 자동으로 뿌립니다."));
        postDatabase.Add(new Post("토끼 개발팀", "Rabbit", 6, 500, "개발자 1명당 토끼를 3초에 1마리씩 자동으로 뿌립니다."));
        postDatabase.Add(new Post("슬라임 개발팀", "Slime", 7, 600, "개발자 1명당 슬라임을 3초에 1마리씩 자동으로 뿌립니다."));

        //developerCount 초기화
        developerCount = Enumerable.Repeat(0, dev.postDatabase.Count).ToArray();
        for (int i = 0; i < dev.postDatabase.Count; i++)
        {
            developerCount[i] = 0;
        }

        CalculateCost();
        ExpandUserLimit();

	}

    void ExpandUserLimit()
    {
        // 서버 관리팀의 개발자 한 명당 유저수 제한을 2500명씩 늘려줍니다.
        gm.userLimit = gm.basicUserLimit + 2500 * developerCount[FindPostIDByName("Server")]; 
    }

    public void CalculateCost()
    {
        //누군가 적절한 함수를 생각해주길
        hireCost = 1000;
        fireCost = -700; // 해고하면 돈 700을 받습니다.

        salaryCost = 0;
        for (int i = 0; i < postDatabase.Count; i++)
        {
            //salaryCost += gm.DeveloperCount[i] * DeveloperSalary[i];
            salaryCost += developerCount[i] * postDatabase[i].postSalary;
        }
    }

    /// <summary>
    /// 총 개발자 수를 반환합니다.
    /// </summary>
    public int DeveloperAllCount()
    {
        int sum = 0;
        foreach (int dev in developerCount)
        {
            sum += dev;
        }
        return sum;
    }

    /// <summary>
    /// 인자로 부서명(Name) 또는 영어 부서명(FuncName)을 넘겨주면 부서ID를 반환합니다. (부서를 못 찾으면 -1 반환)
    /// </summary>
    public int FindPostIDByName(string name)
    {
        for (int i = 0; i < postDatabase.Count; i++)
        {
            if (postDatabase[i].postFuncName == name) return postDatabase[i].postID;
            else if (postDatabase[i].postName == name) return postDatabase[i].postID;
        }
        return -1;
    }

    /// <summary>
    /// 인자로 부서ID를 넘겨주면 부서(Post)를 반환합니다. (부서를 못 찾으면 null 반환)
    /// </summary>
    public Post FindPostByPostID(int id)
    {
        if (id < 0 || id >= postDatabase.Count) return null;
        else return postDatabase[id];
    }

    // 개발자 고용하는 함수. 인자로 부서를 넘겨주면 해당 부서에 개발자가 1명 투입됩니다.
    public void HireDeveloper(Post post) {
        //if (post.postFuncName == "Debugging" && gm.DeveloperCount[post.postID] >= 10) return; // 나중에 개발자 인원 제한이 필요하면 사용
        if (gm.money >= hireCost) {
            gm.money -= hireCost;
            developerCount[post.postID]++;
            CalculateCost();
            if (post.postFuncName == "Server") ExpandUserLimit();
        }
        // 나중에 돈 부족 시 경고 메시지 띄우기
    }

    // 개발자 해고하는 함수. 인자로 부서를 넘겨주면 해당 부서에서 개발자가 1명 빠집니다.
    public void FireDeveloper(Post post) {
        if (gm.money >= fireCost && post.DeveloperInPost() > 0) {
            gm.money -= fireCost;
            developerCount[post.postID]--;
            CalculateCost();
            if (post.postFuncName == "Server") ExpandUserLimit();
        }
        // 나중에 개발자 부족 시 경고 메시지 띄우기
    }

    // 개발자의 부서를 이전하는 함수. from은 원래 있던 부서, to는 새로 이동할 부서입니다.
    public void MoveDeveloper(Post from, Post to)
    {
        //if (to.postFuncName == "Debugging" && gm.DeveloperCount[to.postID] >= 10) return; // 나중에 개발자 인원 제한이 필요하면 사용
        developerCount[from.postID]--;
        developerCount[to.postID]++;
        CalculateCost();
        if (from.postFuncName == "Server" || to.postFuncName == "Server") ExpandUserLimit();
    }
}

[System.Serializable]
public class Post
{

    public string postName;
    public string postFuncName;
    public int postID;
    public int postSalary;
    public string postDescription;
    public Texture2D postImage;

    //이미지는 부서이름(postName)과 똑같은 파일명을 가진 걸 자동으로 사용하도록 했음. 그런데 일단은 이미지가 없으므로 생략
    public Post(string name, string funcName, int ID, int salary, string desc)
    {
        postName = name;
        postFuncName = funcName;
        postID = ID;
        postSalary = salary;
        postDescription = desc;
        //postImage = Resources.Load<Texture2D>("Post Icons/" + name);
    }

    /// <summary>
    ///  이 부서에 투입된 개발자 수를 반환합니다.
    /// </summary>
    public int DeveloperInPost()
    {
        return Developer.dev.developerCount[this.postID];
    }
}