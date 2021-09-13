using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject taskListCanvas;
    public GameObject timerCanvas;
    public GameObject pauseMenu;
    public GameObject playerCanvas;

    public GameObject sampleMinigameCanvas;
    public GameObject cockroachMinigameCanvas;

    private List<string> taskList;

    private float timeLeft = 5; // in seconds, default 5 minutes
    public bool isGamePaused = false;
    public bool isGameDone = false;



    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.COMPLETE_TASK, this.CompleteTask);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.START_MINIGAME, this.StartMinigame);

        taskList = new List<string>();
        taskList.Add("SampleMinigame");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTasklist();
        PauseHandler();

        if (!isGamePaused && !isGameDone)
        {
            if (timeLeft > 0)
            {
                float minutes = Mathf.FloorToInt(timeLeft / 60);
                float seconds = Mathf.FloorToInt(timeLeft % 60);
                timerCanvas.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
                timeLeft -= Time.deltaTime;
            }
            else // GAME OVER CONDITIONS
            {
                GameOver(false);
            }
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameJam.COMPLETE_TASK);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameJam.START_MINIGAME);
    }

    void PauseHandler()
    {
        if (Input.GetKeyDown("escape"))
        {
            Parameters param = new Parameters();
            if (isGamePaused) // if game is paused
            {
                pauseMenu.SetActive(false);
                playerCanvas.SetActive(true);
                isGamePaused = false;
                param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, !isGamePaused);
                EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);
            }
            else // if game is not paused
            {
                pauseMenu.SetActive(true);
                playerCanvas.SetActive(false);
                isGamePaused = true;
                param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, !isGamePaused);
                EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);
            }
        }
    }

    void UpdateTasklist()
    {
        string taskString = "";

        foreach(string task in taskList)
        {
            taskString = taskString + "- " + task + "\n"; 
        }
        
        taskListCanvas.GetComponent<Text>().text = taskString;
    }

    void GameOver(bool isSuccess) // if true, then win; if false, then lose
    {
        Debug.Log("GG KA BOI");
        isGameDone = true;
    }

    void StartMinigame(Parameters param)
    {
        string taskName = param.GetStringExtra(EventNames.Param.TASK_NAME, "defaultValue");
        Debug.Log(taskName);

        playerCanvas.SetActive(false);

        param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, false);
        EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);

        switch (taskName)
        {
            case "SampleMinigame":
                {
                    sampleMinigameCanvas.SetActive(true);
                    break;
                }
            // add more cases for your minigames here
            case EventNames.Minigame.MINIGAME_COCKROACH:
                {
                    cockroachMinigameCanvas.GetComponent<CockroachMinigame>().initGame();
                    cockroachMinigameCanvas.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    void CompleteTask(Parameters param)
    {
        string taskName = param.GetStringExtra(EventNames.Param.TASK_NAME, "defaultValue");

        playerCanvas.SetActive(true);

        param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, true);
        EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);

        switch (taskName)
        {
            case "SampleMinigame":
                {
                    Debug.Log("IZ DONE");
                    sampleMinigameCanvas.SetActive(false);
                    break;
                }
            // add more cases for your minigames here
            case EventNames.Minigame.MINIGAME_COCKROACH:
                {
                    cockroachMinigameCanvas.SetActive(false);
                    break;
                }
            default:
                break;
        }
    }
}
