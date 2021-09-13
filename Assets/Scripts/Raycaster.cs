using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    private bool hitDetectionEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.START_MINIGAME, this.disableHitDetection);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam.COMPLETE_TASK, this.enableHitDetection);
    }

    void disableHitDetection()
    {
        this.hitDetectionEnabled = false;
    } 

    void enableHitDetection()
    {
        this.hitDetectionEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // highlighting
        RaycastHit hit;

        bool objectHighlighted = Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hit,
                10f
            );

        Debug.Log("HIT_DETECTION: " + this.hitDetectionEnabled);
        // selection
        if (objectHighlighted)
        {
            // hit.collider.SendMessage("RayTargetHighlight", SendMessageOptions.DontRequireReceiver);
            if (Input.GetMouseButtonDown(0) && hitDetectionEnabled)
            {
                Debug.Log("bang!");
                hit.collider.SendMessage("RayTargetHit", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
