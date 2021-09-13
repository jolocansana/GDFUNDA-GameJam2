using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KettleGame : MonoBehaviour
{
    public GameObject slideDisplay;
    public GameObject gameInstructions;
    public Slider tempSlider;

    private int slideNumber;
    private int slideCorrect;


    public void Start()
    {
        slideCorrect = Random.RandomRange(0,100);
        gameInstructions.GetComponent<Text>().text = "Adjust the temperature to " + slideCorrect;

        tempSlider.onValueChanged.AddListener(delegate { checkTemperature(); });
    }

    // OTHER GAME LOGIC GOES HERE
    public void checkTemperature()
    {
        int value = (int) (tempSlider.value * 100);
        slideDisplay.GetComponent<Text>().text = value.ToString();

        if (value == slideCorrect)
        {
            finishTask();
        }
    }

    public void finishTask()
    {
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.Param.TASK_NAME, "KettleGame"); // Change name to your minigame

        EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
    }
}
