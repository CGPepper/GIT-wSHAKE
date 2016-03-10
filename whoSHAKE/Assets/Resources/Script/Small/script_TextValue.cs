using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class script_TextValue : MonoBehaviour 
{
    private float StartValue;
    private Text scText;

    public void SetupTween(int value, float delay )
    {
        scText = gameObject.GetComponent<Text>();
        StartValue = float.Parse(scText.text);
        StartCoroutine(RunTween(value, delay));
    }

    IEnumerator RunTween(int targetValue, float delay)
    {
        float duration = 2;
        float TimeLeft = duration;
        float stepTime = 0.1f;

        float TimeFraction = TimeLeft / duration;
        float ValueDifference = targetValue - StartValue;
        
        yield return new WaitForSeconds(delay);
        while (TimeLeft > 0)
        {
            yield return new WaitForSeconds(stepTime);
            TimeLeft -= stepTime;

            TimeFraction = 1f - TimeLeft / duration;

            
            scText.text = Mathf.Round(StartValue+ValueDifference*TimeFraction).ToString();

        }
    }

}
