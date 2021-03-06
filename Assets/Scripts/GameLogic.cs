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

    public GameObject sampleMinigameCanvas;
    public GameObject playerObject;

    // GameObject for Minigame Canvases
    public GameObject kettleGameCanvas;
    public GameObject cockroachMinigameCanvas;
    public GameObject wireCanvas;
    public GameObject fixDoorCanvas;
    public GameObject turnOffFanCanvas;

    public GameObject doneSoundEffect;
    public GameObject winningSoundEffect;

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
    private bool isInMinigame = false;



    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.COMPLETE_TASK, this.CompleteTask);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.START_MINIGAME, this.StartMinigame);

        taskList = new List<Task>();
        taskList.Add(new Task("KettleGame", "Adjust the kettle"));
        taskList.Add(new Task("PlateCollecting", "Pickup the plates"));
        taskList.Add(new Task("LightFix", "Fix the broken light"));
        taskList.Add(new Task("FixDoor", "Fix Mom's Door"));
        taskList.Add(new Task("TurnOffFanGame", "Turn off Mom's fan"));
        taskList.Add(new Task(EventNames.Minigame.MINIGAME_COCKROACH, "Kill the spider in the bathroom"));
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
                isGamePaused = false;

                if (!isInMinigame) // not in minigame
                {
                    playerCanvas.SetActive(true);
                    param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, !isGamePaused); // false stops character, this case TRUE
                    EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);
                }

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
        kettleGameCanvas.SetActive(false);
        cockroachMinigameCanvas.SetActive(false);
        wireCanvas.SetActive(false);
        fixDoorCanvas.SetActive(false);
        turnOffFanCanvas.SetActive(false);

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
        bool isMinigameInTask = true;

        string taskName = param.GetStringExtra(EventNames.Param.TASK_NAME, "defaultValue");
        Debug.Log(taskName);

        // check if the minigame is done to stop from redoing a game
        foreach (Task task in taskList)
        {
            if (task.id == taskName)
            {
                isMinigameInTask = false;
            }
        }

        if (!isMinigameInTask)
        {
            playerCanvas.SetActive(false);

            isInMinigame = true;

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
                        //cockroachMinigameCanvas.GetComponent<CockroachMinigame>().initGame();
                        cockroachMinigameCanvas.SetActive(true);
                        break;
                    }
                case "KettleGame":
                    kettleGameCanvas.SetActive(true);
                    break;
                case "LightFix":
                    wireCanvas.SetActive(true);
                    Debug.Log("Light Hit");
                    break;
                case "FixDoor":
                    fixDoorCanvas.SetActive(true);
                    break;
                case "TurnOffFanGame":
                    turnOffFanCanvas.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    void CompleteTask(Parameters param)
    {
        string taskName = param.GetStringExtra(EventNames.Param.TASK_NAME, "defaultValue");

        playerCanvas.SetActive(true);
        isInMinigame = false;

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
            case "KettleGame":
                Debug.Log("IZ DONE");
                kettleGameCanvas.SetActive(false);
                break;
            // add more cases for your minigames here
            case "PlateCollecting":
                Debug.Log("Plates complete");
                break;
            case "FixDoor":
                fixDoorCanvas.SetActive(false);
                break;
            case "LightFix":
                wireCanvas.SetActive(false);
                break;
            case "TurnOffFanGame":
                turnOffFanCanvas.SetActive(false);
                break;
            default:
                break;
        }

        foreach (Task task in taskList.ToArray())
        {
            if (task.id == taskName)
            {
                taskList.Remove(task);
            }
        }

        Debug.Log(taskList.Count);

        if (taskList.Count > 0)
        {
            doneSoundEffect.GetComponent<AudioSource>().Play();
        } else if (taskList.Count == 0)
        {
            winningSoundEffect.GetComponent<AudioSource>().Play();
        }
    }
}
