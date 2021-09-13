using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    private bool hitDetectionEnabled = true;

    // Start is called before the first frame update
    void Start()
    {

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

        // selection
        if (objectHighlighted)
        {
            // hit.collider.SendMessage("RayTargetHighlight", SendMessageOptions.DontRequireReceiver);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("bang!");
                hit.collider.SendMessage("RayTargetHit", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
