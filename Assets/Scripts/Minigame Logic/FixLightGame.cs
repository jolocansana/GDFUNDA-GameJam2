using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixLightGame : MonoBehaviour
{
    private string wireColor = "";
    private int doneColorsSize = 0;
    private string[] doneColors = new string[4];

    public GameObject red_left;
    public GameObject red_right;
    public GameObject blue_left;
    public GameObject blue_right;
    public GameObject green_left;
    public GameObject green_right;
    public GameObject yellow_left;
    public GameObject yellow_right;

    public GameObject point_light;
    public GameObject spot_light;

    // OTHER GAME LOGIC GOES HERE

    public void checkWire(string color)
    {
        if (!wireColor.Equals(color))
        {
            if (wireColor.Equals("")) // means no color is selected
            {
                wireColor = color.Split('_')[0];
                Debug.Log(wireColor);
            }
            else // means a wire is selected and now you have to select the correct color
            {
                if (wireColor.Equals(color.Split('_')[0]))
                {
                    Debug.Log(wireColor + "is complete");
                    doneColors[doneColorsSize] = wireColor;
                    doneColorsSize += 1;
                } else
                {
                    Debug.Log("Wrong color reset");
                }
                wireColor = "";
            }
        }

        // change button color when done
        foreach (string doneColor in doneColors)
        {
            switch (doneColor)
            {
                case "red":
                    red_left.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    red_right.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                case "blue":
                    blue_left.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    blue_right.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                case "green":
                    green_left.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    green_right.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                case "yellow":
                    yellow_left.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    yellow_right.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                default:
                    break;
            }
        }

        if(doneColorsSize == 4) // minigame done
        {
            spot_light.GetComponent<Animator>().enabled = false;
            spot_light.GetComponent<Light>().color = Color.white;
            finishTask();
        }
    }

    public void finishTask()
    {
        point_light.SetActive(false);
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.Param.TASK_NAME, "LightFix"); // Change name to your minigame

        EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
    }
}
