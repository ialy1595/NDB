using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeDatabase : MonoBehaviour {

    public List<Upgrade> upgradeDatabase = new List<Upgrade>();

    private GameManager gm;

    //업그레이드 데이터베이스는 여기에 추가
    void Start()
    {
        gm = GameManager.gm;

        upgradeDatabase.Add(new Upgrade("서버 증설", 0, 2500, 1, "이 설명은 예외처리함", "Server", "UserLimit", "현재 최대 유저 수", 1, 2.0f));
        upgradeDatabase.Add(new Upgrade("Lv.1 다람쥐 체력 증가", 1, 1000, 1, "Basic Lv.1 다람쥐의 체력을 50 증가시킵니다.", "Basic1Health", "현재 최대 체력", 50, 2.0f));
        upgradeDatabase.Add(new Upgrade("Lv.2 다람쥐 체력 증가", 2, 2000, 1, "Basic Lv.2 다람쥐의 체력을 250 증가시킵니다.", "Basic2Health", "현재 최대 체력", 250, 4.0f));
        upgradeDatabase.Add(new Upgrade("Lv.1 다람쥐 많이 뿌리기", 3, 3000, 1, "Basic Lv.1 다람쥐를 한번에 10마리씩 뿌릴 수 있는 능력이 추가됩니다.", "UnlockBasic1_Amount10"));
        upgradeDatabase.Add(new Upgrade("Lv.2 다람쥐 많이 뿌리기", 4, 5000, 1, "Basic Lv.2 다람쥐를 한번에 10마리씩 뿌릴 수 있는 능력이 추가됩니다.", "UnlockBasic2_Amount10"));
        upgradeDatabase.Add(new Upgrade("무지개 다람쥐 많이 뿌리기", 5, 3000, 1, "Rainbow Lv.1 다람쥐를 한번에 10마리씩 뿌릴 수 있는 능력이 추가됩니다.", "UnlockRainbow1_Amount10"));
    }

    // 시간이 없으니 대충 만들게요
    public void RemoveUpgrade(int ID)
    {
        Upgrade u = new Upgrade();
        u.upgradeID = ID;
        upgradeDatabase.Remove(u);
    }


    //itemDatabase와는 달리 다른 script(e.g. inventory)가 없어 이곳에 함수를 넣음 나중에 다른 적절한 script가 생기면 옮기자.
    public void AddUpgrade(Upgrade upgrade)
    {
        if (gm.Money() < upgrade.upgradePrice)
        {
            GameManager.gm.ShowMessageBox("돈이 부족합니다.");
            return;
        }

        else
        {
            gm.ChangeMoneyInterRound(-upgrade.upgradePrice);
            if (upgrade.upgradeQuantity == 0)
            {
                Unlockables.SetBool(upgrade.upgradeInternalName, !Unlockables.GetBool(upgrade.upgradeInternalName));   // bool 값을 반전시킴
                upgradeDatabase.Remove(upgrade);
                upgradeDatabase.Remove(upgrade);
                UpgradeCheckup.upgradeChkup.MakeUpgradeList();
            }
            else
            {
                Unlockables.SetInt(upgrade.upgradeInternalName, Unlockables.GetInt(upgrade.upgradeInternalName) + upgrade.upgradeQuantity);
            }
            upgrade.upgradePrice = (int) (upgrade.upgradePrice * upgrade.upgradeModifier);
        }
    }

    public Upgrade Find(string name)
    {
        foreach (Upgrade up in upgradeDatabase)
            if (up.upgradeName == name)
                return up;
        return null;
    }

    void Update()
    {
        // 딱히 어디 두어야 할지 모르겠음
        Unlockables.SetInt("UserLimit", Unlockables.GetInt("Server") * Unlockables.GetInt("ServerEff"));
    }
}
