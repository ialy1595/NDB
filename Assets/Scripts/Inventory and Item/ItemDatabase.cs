using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public int attackItemUseCount;

    public List<Item> itemDatabase = new List<Item>();
    public List<Item> rivalItemDatabase = new List<Item>();
    private List<Item> specialItemDatabase = new List<Item>();

    public bool[] isItemUsing;

    //아이템 데이터베이스는 여기에 추가
    void Start()
    {
        attackItemUseCount = 0;
        //itemDatabase.Add(new Item("아이템이름", ID, 가격, 지속시간, "설명", 함수명));

        //normal Item

        itemDatabase.Add(new Item("도토리", 0, 100, 5f, "도토리를 먹은 다람쥐들이 5초간 무적이 됩니다. \n (아이템 사용 시 필드 상에 있는 다람쥐들에만 적용)", Dotory));
        itemDatabase.Add(new Item("녹용", 1, 200, 5f, "녹용을 먹은 유저들이 5초간 강해져 더 빠르게 다람쥐를 잡습니다.", Nokyong));
        itemDatabase.Add(new Item("초보자용 갑주", 2, 500, 5f, "초보 유저가 중수 유저로 5초간 더 빨리 증가합니다.", NoviceArmor));
        itemDatabase.Add(new Item("아싸고도리", 3, 1000, 0f, "사행성 아이템!\n 인기도가 랜덤하게 증가 또는 감소합니다.", AssaGodory));
        itemDatabase.Add(new Item("집행검", 4, 9999, 0f, "득템! 인기도가 1000 증가합니다.", JipHangSword));
        itemDatabase.Add(new Item("용마제구검 제작서", 5, 2000, 0f, "10% 확률로 용마제구검 제작에 성공합니다.", DragonSwordPaper));
        itemDatabase.Add(new Item("노란비서", 6, 1000, 0f, "개발자들이 집에 가버립니다! 라운드의 남은 시간이 10초 감소합니다.", YellowPaper));

        rivalItemDatabase.Add(new Item("접속 장애", 7, 500, 3f, "3초간 경쟁작의 인기도가 오르지 않습니다.", AttackRival0));
        rivalItemDatabase.Add(new Item("청소년 유해매체", 8, 1000, 5f, "5초간 경쟁작의 인기도가 오르지 않습니다.", AttackRival0));
        rivalItemDatabase.Add(new Item("랜섬웨어", 9, 8000, 0f, "경쟁작의 인기도를 초기화시킵니다. \n 50% 확률로 실패합니다.", ResetRival));
        rivalItemDatabase.Add(new Item("악성 루머", 10, 1000, 3f, "3초간 경쟁작의 인기도를 감소시킵니다.", AttackRival1));

        specialItemDatabase.Add(new Item("용마제구검", 0, 0, 0f, "인스타에 찍어서 올리자!\n #겜스타그램#좋아요.", DragonSword));

        isItemUsing = new bool[itemDatabase.Count + rivalItemDatabase.Count];
        isItemUsing.Initialize();
    }


    //아이템 효과는 여기에 추가
    public bool useItem(Item item) {
        if (isItemUsing[item.itemID] == true)
        {
            GameManager.gm.ShowMessageBox("이미 해당 아이템이 사용중입니다.");
            return false;
        }
        isItemUsing[item.itemID] = true;
        StartCoroutine(item.itemfunc(item));

        if (item.itemID >= itemDatabase.Count)
            attackItemUseCount++;
        GameManager.gm.SetSE((int)SE.SEType.ItemUse);
        LogText.WriteLog(item.itemName+"를 사용하였습니다.");

        return true;

    }

    IEnumerator Dotory(Item item)
    {
        int numOfDaram = Daram.All.Count;
        float startTime;

        for (int i = 0; i < numOfDaram; i++)
        {
            Daram.All[i].HP = int.MaxValue;
        }

        startTime = GameManager.gm.time;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (GameManager.gm.time - startTime > item.itemDuration) break;
        }

        for (int i = 0; i < numOfDaram; i++)
        {
            Daram.All[i].HP = Daram.All[i].InitialHP;
        }

        isItemUsing[item.itemID] = false;
    }

    IEnumerator Nokyong(Item item)
    {
        float startTime;

        for(int i = 0; i<User.Count; i++)
            GameManager.gm.userDamagePerLevel[i] *= 2;

        startTime = GameManager.gm.time;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (GameManager.gm.time - startTime > item.itemDuration) break;
        }
        for (int i = 0; i < User.Count; i++)
            GameManager.gm.userDamagePerLevel[i] /= 2f;

        isItemUsing[item.itemID] = false;
    }

    IEnumerator JipHangSword(Item item)
    {
        GameManager.gm.fame += 1000;
        isItemUsing[item.itemID] = false;
        yield return new WaitForSeconds(0f);
    }

    IEnumerator NoviceArmor(Item item)
    {
        float startTime = GameManager.gm.time;

        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (GameManager.gm.time - startTime > item.itemDuration) break;
            GameManager.gm.UserLevel2();
        }

        isItemUsing[item.itemID] = false;
    }

    IEnumerator AssaGodory(Item item)
    {
        GameManager.gm.fame += Random.Range(-2000, 4000);
        yield return new WaitForSeconds(0f);

        isItemUsing[item.itemID] = false;
    }


    IEnumerator DragonSwordPaper(Item item)
    {
        int r = Random.Range(0, 100);
        if (r < 10)
        {
            //팡파레sound
            Debug.Log("성공");
            GameManager.gm.ShowMessageBox("용마제구검 제작 성공!!");
            GameManager.gm.GetComponentInChildren<Inventory>().AddItem(specialItemDatabase[0]); // 용마제구검
        }
        else
        {
            //깨지는Sound;
            GameManager.gm.ShowMessageBox("제작 실패!");
            Debug.Log("실패");
        }
        yield return new WaitForSeconds(item.itemDuration);

        isItemUsing[item.itemID] = false;
    }

    IEnumerator DragonSword(Item item)
    {
        //빠빠빰!
        yield return new WaitForSeconds(0f);

        isItemUsing[item.itemID] = false;
    }

    IEnumerator YellowPaper(Item item)
    {
        GameManager.gm.timeLeft = Mathf.Max(0, GameManager.gm.timeLeft - 10);
        yield return new WaitForSeconds(0f);

        isItemUsing[item.itemID] = false;
    }

    IEnumerator Pumpkin(Item item)
    {
        GameManager.gm.userCount[User.level1] += GameManager.gm.userCount[User.level2];
        GameManager.gm.userCount[User.level2] = 0;

        yield return new WaitForSeconds(item.itemDuration);

        isItemUsing[item.itemID] = false;
    }

    IEnumerator MasterContract(Item item)
    {
        if (Unlockables.GetBool("UnlockBasic2") == true)
        {
            float startTime = GameManager.gm.time;
            int numChangeOfUser = Mathf.Min(GameManager.gm.userCount[User.level1], 1000);
            GameManager.gm.userCount[User.level2] += numChangeOfUser;

            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (GameManager.gm.time - startTime > item.itemDuration)
                {
                    GameManager.gm.userCount[User.level2] -= Mathf.Min(numChangeOfUser, GameManager.gm.userCount[User.level2]);
                    GameManager.gm.userCount[User.level1] += numChangeOfUser;
                    break;
                }
            }
        }
        else
        {
            GameManager.gm.ShowMessageBox("중급 유저가 개방되지 않았으므로 효과가 없었다!");
            yield return new WaitForSeconds(0f);
        }

        isItemUsing[item.itemID] = false;

    }

    IEnumerator AttackRival0(Item item)
    {
        float startTime = GameManager.gm.time;

        GameManager.gm.isenemyFameIncresing = false;

        while (true)
        {
            if (GameManager.gm.time - startTime > item.itemDuration)
                break;

            yield return new WaitForSeconds(0.1f);
        }

        GameManager.gm.isenemyFameIncresing = true;
        isItemUsing[item.itemID] = false;
    }

    IEnumerator AttackRival1(Item item)
    {
        float startTime = GameManager.gm.time;

        GameManager.gm.isenemyFameIncresing = false;
        GameManager.gm.enemyFameOuterConstant = -2;
        while (true)
        {
            if (GameManager.gm.time - startTime > item.itemDuration)
                break;


            yield return new WaitForSeconds(0.1f);
        }

        GameManager.gm.isenemyFameIncresing = true;
        GameManager.gm.enemyFameOuterConstant = 0;

        isItemUsing[item.itemID] = false;
    }

    IEnumerator ResetRival(Item item)
    {
        if((int)Random.Range(1,11) % 2 == 0)
            GameManager.gm.enemyFame = 0;

        yield return new WaitForSeconds(0f);

        isItemUsing[item.itemID] = false;
    }
}
