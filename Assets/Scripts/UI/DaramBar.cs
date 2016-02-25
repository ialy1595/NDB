using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DaramBar : MonoBehaviour {

    //public GameObject PositiveArea;
    public GameObject Arrow;
    public int UserLevel;   // User 클래스 참조
    public string DaramType;
    public float MaxDiff;     // 게이지 Top에서 Bottom까지의 다람쥐 수
    public bool DetailedInfo = true;
    public bool ArrowDirection;

    private Image arrowImage;

    private const int BarHeight = 140;
    private Slider slider;
    //private RectTransform rect;
    private RectTransform arrowrect;
    private Quadric DaramFunc;
    private string UnlockKey;

    void Start()
    {
        arrowImage = GetComponent<Image>();
        slider = GetComponent<Slider>();
        //rect = PositiveArea.GetComponent<RectTransform>();
        arrowrect = Arrow.GetComponent<RectTransform>();
        DaramFunc = GameManager.gm.DaramFunction[UserLevel];

        SetUnlockKey();
        /*
        switch (UserLevel)
        {
            case User.level1:
                UnlockKey = "UnlockBasic1";
                break;
            case User.level2:
                UnlockKey = "UnlockBasic2";
                break;
        }
        */

        // NaN exception이 뜨지 않게 대충 초기화
        DaramFunc.x = 0;
        DaramFunc.a = 0;
        DaramFunc.max = 1;
        DaramFunc.min = -1;
        DaramFunc.solution = 2;
    }


    void Update()
    {
        if (Unlockables.GetBool(UnlockKey) == false)
            transform.localScale = Vector3.zero;
        else
        {
            if (DaramFunc.diff >= 0) // 다람쥐 수가 적정 수준 이상이면 화살표가 아래를 향함
            {
                arrowrect.localScale = new Vector3(1, -1, 1);
                arrowImage.color = new Color(0, 0, 255, 255);

            }
            else // 다람쥐 수가 적정 수준보다 적으면 화살표가 위를 향함
            {
                arrowrect.localScale = new Vector3(1, 1, 1);
                arrowImage.color = new Color(255, 255, 255, 255);
            }

            float tempValue;
            if (ArrowDirection)
            {
                tempValue = Mathf.Min((slider.value + 0.01f) + 0.003f * (DaramFunc.max - DaramFunc.value) * Mathf.Cos((Mathf.PI / 3f) * slider.value / slider.maxValue), slider.maxValue);
                slider.value = tempValue;
                if (slider.maxValue - tempValue < float.Epsilon) ArrowDirection = !ArrowDirection;
            }
            else
            {
                tempValue = Mathf.Max((slider.value - 0.01f) - 0.003f * (DaramFunc.max - DaramFunc.value) * Mathf.Cos((Mathf.PI / 3f) * slider.value / slider.maxValue), slider.minValue);
                slider.value = tempValue;
                if (tempValue - slider.minValue < float.Epsilon) ArrowDirection = !ArrowDirection;
            }
        }

            //Vector3 v = rect.localScale;
            //v.y = DaramFunc.solution / MaxDiff;
            //rect.localScale = v;

            /* Original Code

            if (DaramFunc.diff >= 0) // 다람쥐 수가 적정 수준 이상이면 화살표가 아래를 향함
            {
                if (DetailedInfo == false)
                    slider.value = DaramFunc.solution / MaxDiff + 0.1f * Mathf.Sin(3.0f * GameManager.gm.time);
                else
                    slider.value = DaramFunc.diff / MaxDiff;

                arrowrect.localScale = new Vector3(1, -1, 1);
            }
            else // 다람쥐 수가 적정 수준보다 적으면 화살표가 위를 향함
            {

                if (DetailedInfo == false)
                    slider.value = (-1) * DaramFunc.solution / MaxDiff - 0.1f * Mathf.Sin(3.0f * GameManager.gm.time);
                else
                    slider.value = DaramFunc.diff / MaxDiff;

                arrowrect.localScale = new Vector3(1, 1, 1);
            }

            */

        }

    void SetUnlockKey()
    {
        if (UserLevel == User.level1)
        {
            if (DaramType == "Basic") UnlockKey = "UnlockBasic1";
            else if (DaramType == "Rainbow") UnlockKey = "UnlockRainbow1";
        }
        else if (UserLevel == User.level2)
        {
            if (DaramType == "Basic") UnlockKey = "UnlockBasic2";
            else if(DaramType == "Rainbow") UnlockKey = "UnlockRainbow2";
        }
    }
}
