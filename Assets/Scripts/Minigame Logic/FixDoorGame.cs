using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDoorGame : MonoBehaviour
{
    public GameObject doorObject;

    private int unscrewed = 4;
    // OTHER GAME LOGIC GOES HERE
    public void fixScrew()
    {
        unscrewed -= 1;

        if (unscrewed == 0)
        {
            doorObject.GetComponent<Animator>().enabled = false;
            doorObject.transform.rotation = Quaternion.Euler(-90, 0, 90);
            finishTask();
        }
    }

    public void finishTask()
    {
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.Param.TASK_NAME, "FixDoor"); // Change name to your minigame

        EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
    }
}
