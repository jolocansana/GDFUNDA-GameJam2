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
    public GameObject gameOverCanvas;
    public GameObject gameOverCondition;

    public GameObject playerObject;

    // GameObject for Minigame Canvases
    public GameObject sampleMinigameCanvas;

    public class Task
    {
        public string id;
        public string taskDesc;

        public Task(string id, string taskDesc)
        {
            this.id = id;
            this.taskDesc = taskDesc;
        }
    }

    private List<Task> taskList;

    private float timeLeft = 60; // in seconds, default 5 minutes = 300s
    public bool isGamePaused = false;
    public bool isGameDone = false;
    private Vector3 playerOrigPos;



    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.COMPLETE_TASK, this.CompleteTask);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.START_MINIGAME, this.StartMinigame);

        taskList = new List<Task>();
        taskList.Add(new Task("SampleMinigame", "Do sample minigame"));
        playerOrigPos = playerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTasklist();
        PauseHandler();

        Debug.Log(playerOrigPos);

        if (!isGamePaused && !isGameDone)
        {
            if (timeLeft > 0)
            {
                float minutes = Mathf.FloorToInt(timeLeft / 60);
                float seconds = Mathf.FloorToInt(timeLeft % 60);
                timerCanvas.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
                timeLeft -= Time.deltaTime;
            }
            else // TIME FAIL GAME OVER
            {
                GameOver(false);
            }
        }

        if (taskList.Count == 0)
        {
            GameOver(true);
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
                param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, !isGamePaused); // false stops character
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

        foreach(Task task in taskList)
        {
            taskString = taskString + "- " + task.taskDesc + "\n"; 
        }
        
        taskListCanvas.GetComponent<Text>().text = taskString;
    }

    void GameOver(bool isSuccess) // if true, then win; if false, then lose
    {
        Parameters param = new Parameters();
        param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, false);
        EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);

        if (isSuccess)
        {
            gameOverCondition.GetComponent<Text>().text = "GG! You did it!";
        } else
        {
            gameOverCondition.GetComponent<Text>().text = "Oh no, you better hide.";
        }

        playerCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        isGameDone = true;

        playerObject.transform.position = playerOrigPos;
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
                sampleMinigameCanvas.SetActive(true);
                break;
            // add more cases for your minigames here
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
                Debug.Log("IZ DONE");
                sampleMinigameCanvas.SetActive(false);
                break;
            // add more cases for your minigames here
            default:
                break;
        }

        foreach (Task task in taskList)
        {
            if (task.id == taskName)
            {
                taskList.Remove(task);
            }
        }
    }
}
