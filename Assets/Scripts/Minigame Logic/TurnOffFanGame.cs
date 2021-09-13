using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffFanGame : MonoBehaviour
{
    public GameObject gameInstructions;
    public Slider tempSlider;

    public GameObject fan;

    private int numLeft;


    public void Start()
    {
        numLeft = Random.RandomRange(0, 5);
        gameInstructions.GetComponent<Text>().text = "Turn off the fan by pulling up and down " + numLeft + " times";

        tempSlider.onValueChanged.AddListener(delegate { checkIfPulled(); });
    }

    // OTHER GAME LOGIC GOES HERE
    public void checkIfPulled()
    {
        float value = tempSlider.value;

        if (value == 0)
        {
            numLeft -= 1;
            // tempSlider.value = 0.5f;
            gameInstructions.GetComponent<Text>().text = "Turn off the fan by pulling up and down " + numLeft + " times";
        }

        if (numLeft == 0)
        {
            fan.GetComponent<Animator>().enabled = false;
            finishTask();
        }
    }

    public void finishTask()
    {
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.Param.TASK_NAME, "TurnOffFanGame"); // Change name to your minigame

        EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
    }
}
