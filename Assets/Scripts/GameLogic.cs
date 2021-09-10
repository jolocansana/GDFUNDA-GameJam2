using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    public GameObject sampleMinigameCanvas;


    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.COMPLETE_TASK, this.completeTask);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.START_MINIGAME, this.startMinigame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameJam.COMPLETE_TASK);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameJam.START_MINIGAME);
    }

    void startMinigame(Parameters param)
    {
        string taskName = param.GetStringExtra(EventNames.Param.TASK_NAME, "defaultValue");
        Debug.Log(taskName);
        Cursor.lockState = CursorLockMode.None;

        switch (taskName)
        {
            case "SampleMinigame":
                sampleMinigameCanvas.SetActive(true);
                break;
            // add more cases for your minigames here
            default:
                break;
        }
    }

    void completeTask(Parameters param)
    {
        string taskName = param.GetStringExtra(EventNames.Param.TASK_NAME, "defaultValue");
        Cursor.lockState = CursorLockMode.Locked;

        switch (taskName)
        {
            case "SampleMinigame":
                Debug.Log("IZ DONE");
                sampleMinigameCanvas.SetActive(false);
                break;
            // add more cases for your minigames here
            default:
                break;
        }
    }
}
