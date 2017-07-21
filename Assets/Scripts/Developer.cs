﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Developer : MonoBehaviour {

    public static Developer dev;
    private GameManager gm;

    [HideInInspector] public List<Post> postDatabase = new List<Post>();  // 부서에 대한 정보를 담고 있음;

    [HideInInspector] public int[] developerCount;      // 각 부서에 몇 명의 개발자가 투입되었는지에 대한 정보를 담고 있음
    [HideInInspector] public int[] useableDeveloperCount;         //현재 사용 가능한 개발자 수

    // [HideInInspector] public Post temp;                 // 부서 이전 시 개발자가 원래 있던 부서를 기억

    [HideInInspector] public int hireCost = 2000;
    [HideInInspector] public int fireCost = -1500;
    //[HideInInspector] public int salaryCost = 0;        // 초당(per second) 지급되는 개발자 월급
    /*
    [HideInInspector] public int serverLimitIncreasePerDeveloper = 1000;
    [HideInInspector] public float macroDecreasePerDeveloper = 0.1f;
    [HideInInspector] public float userIncreasePerDeveloper = 0.2f;
    [HideInInspector] public int developerMonsterGenerationTime = 3;
    */

    void Start()
    {
        dev = this;
        gm = GameManager.gm;

        postDatabase.Add(new Post("버그 전담 GM", "Debugging", 0, "버그, 매크로 발생 시 GM을 1명씩 투입하여 빠르게 해결할 수 있습니다."));
        /*
        // Post(부서명, 영어 부서명, 부서ID, 부서에 속한 개발자 1명당 월급(per second), 설명)
        // 부서ID는 postDatabase, developerCount의 index값과 일치합니다.
        postDatabase.Add(new Post("서버 관리팀", "Server", 0, 6, "개발자 1명당 서버 1대가 수용 가능한 유저의 수가 " + serverLimitIncreasePerDeveloper + "씩 증가합니다."));
        postDatabase.Add(new Post("디버깅 팀", "Debugging", 1, 5, "개발자 1명당 매크로, 버그의 지속시간이 " + macroDecreasePerDeveloper * 100 + "%p씩 줄어듭니다."));
        postDatabase.Add(new Post("행사 홍보팀", "Publicity", 2, 4, "개발자 1명당 초보 유저수의 증가 속도가 " + userIncreasePerDeveloper * 100 + "%p씩 빨라집니다.\n행사를 시행하기 위해 특정 수 이상을 만족해야 합니다."));
        postDatabase.Add(new Post("고객 지원팀", "Customer", 3, 4, "인기도 하락 속도가 줄어듭니다."));
        postDatabase.Add(new Post("Lv.1 다람쥐 개발팀", "DaramLv1", 4, 3, "개발자 1명당 Lv.1 다람쥐를 " + developerMonsterGenerationTime + "초에 1마리씩 자동으로 뿌립니다.\n3/9명을 배치시키면 수동으로 뿌리는 능력이 상승합니다."));
        postDatabase.Add(new Post("Lv.2 다람쥐 개발팀", "DaramLv2", 5, 6, "개발자 1명당 Lv.2 다람쥐를 " + developerMonsterGenerationTime + "초에 1마리씩 자동으로 뿌립니다.\n3/9명을 배치시키면 수동으로 뿌리는 능력이 상승합니다."));
        //postDatabase.Add(new Post("토끼 개발팀", "Rabbit", 6, 9, "개발자 1명당 토끼를 " + developerMonsterGenerationTime + "초에 1마리씩 자동으로 뿌립니다.\n3/9명을 배치시키면 수동으로 뿌리는 능력이 상승합니다."));
        //postDatabase.Add(new Post("슬라임 개발팀", "Slime", 7, 7, "개발자 1명당 슬라임을 " + developerMonsterGenerationTime + "초에 1마리씩 자동으로 뿌립니다.\n3/9명을 배치시키면 수동으로 뿌리는 능력이 상승합니다."));
        */
        //developerCount 초기화
        developerCount = Enumerable.Repeat(0, dev.postDatabase.Count).ToArray();
        useableDeveloperCount = Enumerable.Repeat(0, dev.postDatabase.Count).ToArray();

        for (int i = 0; i < dev.postDatabase.Count; i++)
        {
            developerCount[i] = 0;
            useableDeveloperCount[i] = 0;
        }
        developerCount[FindPostIDByName("Debugging")] = 1;
        useableDeveloperCount[FindPostIDByName("Debugging")] = 1;
        CalculateCost();

    }

    /*
    void ExpandUserLimit()
    {
        // 서버 관리팀의 개발자 한 명당 유저수 제한을 serverLimitIncreasePerDeveloper명씩 늘려줍니다.
        Unlockables.SetInt("ServerEff", Unlockables.GetInt("ServerEff") + serverLimitIncreasePerDeveloper);
    }

    void ReduceUserLimit()
    {
        // 서버 관리팀에서 개발자가 빠지면 유저수 제한이 serverLimitIncreasePerDeveloper명 감소합니다.
        Unlockables.SetInt("ServerEff", Unlockables.GetInt("ServerEff") - serverLimitIncreasePerDeveloper);
    }
    */
    // 초당(per second) 나가는 개발자 월급 계산하는 함수
    // 한 라운드에 나가게 될 예상 월급은 salaryCost에 라운드 진행 시간을 곱한 값임.
    public void CalculateCost()
    {
        //누군가 적절한 함수를 생각해주길
        hireCost = 2000;
        fireCost = -1500; // 해고하면 돈 700을 받습니다.
        /*
        salaryCost = 0;
        for (int i = 0; i < postDatabase.Count; i++)
        {
            salaryCost += developerCount[i] * postDatabase[i].postSalary;
        }
         * */
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
        //if (developerCount[post.postID] >= 9) return; // 개발자 인원 제한
        if (gm.Money() >= hireCost) {
            gm.ChangeMoneyInterRound(-hireCost);
            developerCount[post.postID]++;
            CalculateCost();
            //if (post.postFuncName == "Server") ExpandUserLimit();
        }
        else
        {
            GameManager.gm.ShowMessageBox("돈이 부족합니다.");
            return;
        }
        // 나중에 돈이 부족하거나 개발자가 제한보다 많으면 경고 메시지 띄우기
    }

    // 개발자 해고하는 함수. 인자로 부서를 넘겨주면 해당 부서에서 개발자가 1명 빠집니다.
    public void FireDeveloper(Post post) {
        if (gm.Money() >= fireCost && post.DeveloperInPost() > 0) {
            gm.ChangeMoneyInterRound(-fireCost);
            developerCount[post.postID]--;
            CalculateCost();
            //if (post.postFuncName == "Server") ReduceUserLimit();
        }
        else if (gm.Money() < fireCost)
        {
            GameManager.gm.ShowMessageBox("돈이 부족합니다.");
            return;
        }
        // 나중에 개발자가 부족하면 경고 메시지 띄우기
    }

    //라운드 시작할 때 사용 가능한 개발자 수 맞춰주는거
    public void InitiateUseableDeveloper()
    {
        for(int i = 0; i < dev.postDatabase.Count; i++)
        {
            useableDeveloperCount[i] = developerCount[i];
        }
    }

    //버그gm 투입. 투입 되면 ture, 안되면 false 반환
    public bool UseDeveloper(Post post)
    {
        if(useableDeveloperCount[post.postID]>0)
        {
            useableDeveloperCount[post.postID]--;
            return true;
        }
        else
        {
            return false;
        }
    }

    //버그gm 반환
    public void FinishDeveloper(Post post)
    {
        useableDeveloperCount[post.postID]++;
    }
    


    /*
    // 개발자의 부서를 이동하는 함수. from은 원래 있던 부서, to는 새로 이동할 부서입니다.
    public void MoveDeveloper(Post from, Post to)
    {
        if (developerCount[to.postID] >= 9) return; // 개발자 인원 제한
        if (from.DeveloperInPost() > 0)
        {
            developerCount[from.postID]--;
            developerCount[to.postID]++;
            temp = null;
            CalculateCost();
            if (from.postFuncName == "Server") ReduceUserLimit();
            if (to.postFuncName == "Server") ExpandUserLimit();
        }
        // 나중에 from의 개발자가 부족하거나 to의 개발자가 제한보다 많으면 경고 메시지 띄우기
    }
     * */
}

[System.Serializable]
public class Post
{

    public string postName;
    public string postFuncName;
    public int postID;
    //public int postSalary;
    public string postDescription;
    //public Texture2D postImage;

    //이미지는 부서이름(postName)과 똑같은 파일명을 가진 걸 자동으로 사용하도록 했음. 그런데 일단은 이미지가 없으므로 생략
    public Post(string name, string funcName, int ID,/*int salary,*/ string desc)
    {
        postName = name;
        postFuncName = funcName;
        postID = ID;
        //postSalary = salary;
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

    //이 부서에 투입된 사용가능한 개발자 수를 반환합니다.
    public int UseableDeveloperInPost()
    {
        return Developer.dev.useableDeveloperCount[this.postID];
    }
}
