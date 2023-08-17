using System.Collections;
using UnityEngine;

public class TimerEndHandler : MonoBehaviour
{
    public GameObject timer;
    public GameObject Ending2;

    public void OnDisable()
    {
        if (timer != null)
            timer.SetActive(false);

        if (Ending2 != null)
            Ending2.SetActive(true);
    }


}
