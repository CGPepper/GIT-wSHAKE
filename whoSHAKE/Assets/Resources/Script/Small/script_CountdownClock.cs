using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class script_CountdownClock : MonoBehaviour 
{

    public void SetupCountdown(float time, float step)
    {
        StopAllCoroutines();
        StartCoroutine(Countdown(time, step));
    }

    IEnumerator Countdown(float time, float step)
    {
        if (step == 0) step = Time.deltaTime;
        while (true)
        {

            if (time <= 0)
            {
                TimerEnd();
                yield break;
            }
            else
            {
                gameObject.GetComponent<Slider>().value -= step;
                yield return new WaitForSeconds(step);
            }
            time -= step;
        }
    }


    private void TimerEnd()
    {

    }
}
