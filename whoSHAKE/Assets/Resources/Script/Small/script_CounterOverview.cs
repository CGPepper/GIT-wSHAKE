using UnityEngine;
using System.Collections;
using TMPro;

public class script_CounterOverview : MonoBehaviour 
{
    private float TimeMax;
    private float TimeLeft;
    private TextMeshProUGUI script;
    public GameObject go_UI;

    void Start()
    {
        script = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetupTimer(float time)
    {
        TimeMax = time;
        TimeLeft = time;
        StartCoroutine(Counting());

    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    IEnumerator Counting()
    {
        while (TimeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            TimeLeft -= 1;
            script.text = TimeLeft.ToString();
        }
        go_UI.GetComponent<script_UI>().CallAction("OverviewTimerEnd");
    }


}
