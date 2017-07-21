using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text.RegularExpressions;

public class StageClear : MonoBehaviour {

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    public void OnClick()
    {
        string text = GameManager.gm.currentStageScene;
        string pattern = @"^Stage(-?\d+)$";
        Match match = Regex.Match(text, pattern);
        if (match.Success)
        {
            GameManager.gm.clearedLevel = int.Parse(match.Groups[1].Value);
            SaveLoad.Save();
            GameManager.gm.SetBGM(1);
            SceneManager.LoadScene("ChooseStages");
        }
        else
            Debug.LogError("스테이지 파싱 실패");
    }
}
