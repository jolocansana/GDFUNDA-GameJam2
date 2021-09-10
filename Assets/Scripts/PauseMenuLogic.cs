using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuLogic : MonoBehaviour
{
    public bool isGamePaused = false;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Parameters param = new Parameters();
            if (isGamePaused) // if game is paused
            {
                pauseMenu.SetActive(false);
                isGamePaused = false;
                param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, !isGamePaused);
                EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);
            }
            else // if game is not paused
            {
                pauseMenu.SetActive(true);
                isGamePaused = true;
                param.PutExtra(EventNames.Param.TOGGLE_CHARACTER, !isGamePaused);
                EventBroadcaster.Instance.PostEvent(EventNames.Param.TOGGLE_CHARACTER, param);
            }
        }
    }
}
