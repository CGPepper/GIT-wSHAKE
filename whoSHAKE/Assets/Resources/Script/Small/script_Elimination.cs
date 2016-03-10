using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class script_Elimination : MonoBehaviour 
{

    public void ShowStamp(float delay)
    {
        StartCoroutine(DelayCall(delay));
    }

    IEnumerator DelayCall(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        gameObject.GetComponent<DOTweenAnimation>().DOPlay();
    }
}
