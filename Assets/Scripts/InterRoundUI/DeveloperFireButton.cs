using UnityEngine;
using System.Collections;

public class DeveloperFireButton : MonoBehaviour {

    private Post modifyingDeveloper = Developer.dev.FindPostByPostID(Developer.dev.FindPostIDByName("Debugging"));

    public void SetPost(Post post)
    {
        modifyingDeveloper = post;
    }

    public void Fire()
    {
        if (EndInterRound.CheckTutorial())
            return;
        Developer.dev.FireDeveloper(modifyingDeveloper);
        //DeveloperCheckup.devChkup.RefreshPostTooltip();
    }
    
}
