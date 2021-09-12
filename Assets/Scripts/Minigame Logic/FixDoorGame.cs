using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMinigame : MonoBehaviour
{

    // OTHER GAME LOGIC GOES HERE


    public void finishTask()
    {
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.Param.TASK_NAME, "SampleMinigame"); // Change name to your minigame

        EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
    }
}
