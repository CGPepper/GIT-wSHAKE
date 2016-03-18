using UnityEngine;
using System.Collections;
using DG.Tweening;

public class script_Asteroid : MonoBehaviour 
{
    public GameObject[] WayPoints = new GameObject[2];

    public void Q_Move(int WayPointIndex, float duration, bool queue)
    {
        DOTween.Kill(gameObject.transform);
          transform.position = WayPoints[1].transform.position;
        transform.DOMove(WayPoints[WayPointIndex].transform.position, duration*2);
        Debug.Log("Moving asteroid over " + duration + " seconds");
    }
}
