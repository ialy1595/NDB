using UnityEngine;
using System.Collections;

public class DeveloperHireButton : MonoBehaviour {
    
    private Post modifyingDeveloper = Developer.dev.FindPostByPostID(Developer.dev.FindPostIDByName("Debugging"));
    /*
    public void SetPost(Post post)
    {
        modifyingDeveloper = post;
    }
    */
    public void Hire()
    {
        if (EndInterRound.CheckTutorial())
            return;
        Developer.dev.HireDeveloper(modifyingDeveloper);
        //DeveloperCheckup.devChkup.RefreshPostTooltip();
    }
}
