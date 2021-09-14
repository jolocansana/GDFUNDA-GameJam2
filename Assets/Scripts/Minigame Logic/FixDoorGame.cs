using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixDoorGame : MonoBehaviour
{
    public GameObject doorObject;
    public GameObject doorParent;

    public GameObject point_light;

    private int unscrewed = 4;
    private List<string> buttonIds;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    // OTHER GAME LOGIC GOES HERE

    private void Start()
    {
        buttonIds = new List<string>();
        buttonIds.Add("button1");
        buttonIds.Add("button2");
        buttonIds.Add("button3");
        buttonIds.Add("button4");
    }

    public void fixScrew(string buttonId)
    {
        bool isNotDone = false;

        Debug.Log(buttonId);

        foreach (string id in buttonIds.ToArray())
        {   
            if (id == buttonId)
            {
                Debug.Log("INSIDE" + id);
                isNotDone = true;
                buttonIds.Remove(id);
            }
        }

        Debug.Log("ISDONE: " + isNotDone);

        if (isNotDone)
        {
            unscrewed -= 1;

            switch (buttonId)
            {
                case "button1":
                    button1.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                case "button2":
                    button2.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                case "button3":
                    button3.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                case "button4":
                    button4.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
                    break;
                default:
                    break;
            }

            if (unscrewed == 0)
            {
                doorObject.GetComponent<Animator>().enabled = false;
                doorParent.GetComponent<BoxCollider>().enabled = false;
                doorObject.transform.rotation = Quaternion.Euler(-90, 0, 180);

                point_light.SetActive(false);
                finishTask();
            }
        }
    }

    public void finishTask()
    {
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.Param.TASK_NAME, "FixDoor"); // Change name to your minigame

        EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
    }
}
