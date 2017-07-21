﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UpgradeCheckup : MonoBehaviour {

    public GameObject UpgradeListTemplate;
    static public UpgradeCheckup upgradeChkup;

    private GameObject upgradePanel;
    private GameObject upgradePanel2;
    private GameObject upgradeScrollPanel;
    private Text upgradeStatusText;
    private UpgradeDatabase database;
    private RectTransform upgradeScrollPanelrect;
    private int imageIconSize = 256;

    private List<GameObject> upgradeList = new List<GameObject>();

    void Start()
    {
        upgradeChkup = this;
        database = GameManager.gm.GetComponentInChildren<UpgradeDatabase>();
        upgradePanel = GameObject.Find("UpgradePanel");
        upgradePanel2 = GameObject.Find("UpgradePanel2");
        upgradeScrollPanel = GameObject.Find("UpgradeScrollPanel");
        upgradeStatusText = GameObject.Find("UpgradeStatus").GetComponentInChildren<Text>();
        upgradeScrollPanelrect = upgradeScrollPanel.GetComponent<RectTransform>();
        upgradePanel.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        upgradePanel.SetActive(false);
        MakeUpgradeList();
        upgradeStatusText.text = "남은 돈 : " + GameManager.gm.Money();
    }

    public void MakeUpgradeList()
    {
        foreach (GameObject go in upgradeList)
            Destroy(go);
        upgradeList.Clear();
        SetListSize(upgradeScrollPanelrect);
        int listcount = 0;
        foreach (Upgrade Upgrade in database.upgradeDatabase)
        {
            GameObject newupgrade = Instantiate(UpgradeListTemplate, new Vector3(0f, (upgradeScrollPanelrect.rect.height / 2f) - 120f * listcount - 20f, 0f), Quaternion.identity) as GameObject;
            newupgrade.name = Upgrade.upgradeName;

            /* 다른 children이 추가되면 아래 코드에서 에러가 발생할 수도? */
            newupgrade.GetComponentInChildren<Image>().sprite = Sprite.Create(Upgrade.upgradeImage, new Rect(0, 0, imageIconSize, imageIconSize), new Vector2(0f, 0f));
            newupgrade.transform.SetParent(upgradeScrollPanel.transform, false);

            newupgrade.GetComponentInChildren<UpgradeBuyButton>().SetUpgrade(Upgrade);

            upgradeList.Add(newupgrade);
            listcount++;

        }
        RefreshTooltip();
    }

    public void ShowUpgrades()
    {
        //이유는 모르겠지만 처음에 위치 조정을 안해주면 스크롤바랑 이미지 표시가 이상해짐
        upgradePanel2.GetComponent<ScrollRect>().verticalScrollbar.value = 0;
        upgradeScrollPanelrect.localPosition = new Vector2(upgradeScrollPanelrect.localPosition.x, -upgradeScrollPanelrect.rect.height / 2f);
        upgradePanel.SetActive(true);
        RefreshTooltip();
    }

    void SetListSize(RectTransform rect)
    {
        rect.sizeDelta = new Vector2(rect.rect.width, database.upgradeDatabase.Count * 120f + 20f);
    }

    // 새로고침 할 수 있게 변경함
    public void RefreshTooltip()
    {
        foreach (GameObject go in upgradeList)
        {
            Upgrade Upgrade = database.Find(go.name);
            if (Upgrade != null)
            {
                string tooltip = "<color=#ffffff>" + Upgrade.upgradeName + "</color>\n\n";
                if (Upgrade.upgradeName == "서버 증설") // 이걸 만들기가 너무 힘들어서 예외처리함
                    tooltip += "<color=#000000>" + "서버 한 대당 " + Unlockables.GetInt("ServerEff") + "명의 유저를 추가로 수용합니다." + "</color>\n\n";
                else
                    tooltip += "<color=#000000>" + Upgrade.upgradeDescription + "</color>\n\n";
                tooltip += "<color=#990282>" + "가격 : " + Upgrade.upgradePrice + "</color>\t\t";
                //tooltip += "<color=#990282>" + "필요 개발자 수 : " + Upgrade.upgradeRequiredDev + "</color>\t\t";
                if (Upgrade.upgradeQuantity != 0)
                    tooltip += "<color=#990282>" + Upgrade.upgradeQuantityName + " : "+ Unlockables.GetInt(Upgrade.upgradeTooltipName) + "</color>";
                go.GetComponentInChildren<Text>().text = tooltip;
            }
        }
        upgradeStatusText.text = "남은 돈 : " + GameManager.gm.Money();
    }

    void Update()
    {
        RefreshTooltip();
    }
}
