using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CockroachMinigame : MonoBehaviour
{
    public int timesToHit = 0;
    [SerializeField] GameObject counter;
    [SerializeField] GameObject cockroach;

    // Start is called before the first frame update
    void Start()
    {
        timesToHit = Random.Range(5, 9);
        this.randomizePos();
        this.updateCounter();
        Debug.Log("CockroachStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initGame()
    {
        timesToHit = Random.Range(5, 9);
        this.randomizePos();
        this.updateCounter();
    }

    public void cockroachHit()
    {
        Debug.Log("cockroach hit");
        this.timesToHit -= 1;
        this.updateCounter();

        if (this.timesToHit <= 0)
        {
            this.finishTask();
        } 
        else
        {
            this.randomizePos();
        }
    }

    public void finishTask()
    {
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.Param.TASK_NAME, EventNames.Minigame.MINIGAME_COCKROACH); // Change name to your minigame

        EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
    }

    public void randomizePos()
    {
        RectTransform rt = cockroach.GetComponent<RectTransform>();
        Vector3 pos = rt.localPosition;
        //x: -200, 200, y -150, 150
        pos.x = Random.Range(-200f, 200f);
        pos.y = Random.Range(-150f, 150f);
        rt.localPosition = pos;


        Vector3 rot = rt.eulerAngles;
        rot.z = Random.Range(0f, 360f);
        rt.eulerAngles = rot;
    }

    public void updateCounter()
    {
        Text txtComponent = this.counter.GetComponent<Text>();
        txtComponent.text = string.Format("Hit the spider {0} times to complete!", timesToHit);
    }
}
