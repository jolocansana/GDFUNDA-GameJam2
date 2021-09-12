using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateLogic : MonoBehaviour
{
    [SerializeField] private int plates = 0;

    // Start is called before the first frame update
    void Start()
    {
        plates = 0;
        EventBroadcaster.Instance.AddObserver(EventNames.Plates.PLATE_COLLECTED, this.addPlates);
    }

    // Update is called once per frame
    void addPlates()
    {
        plates += 1;

        if (plates == 4)
        {
            Parameters parameters = new Parameters();
            parameters.PutExtra(EventNames.Param.TASK_NAME, "PlateCollecting");
            EventBroadcaster.Instance.PostEvent(EventNames.GameJam.COMPLETE_TASK, parameters);
        }
    }
    void Update()
    {
        
    }
}
