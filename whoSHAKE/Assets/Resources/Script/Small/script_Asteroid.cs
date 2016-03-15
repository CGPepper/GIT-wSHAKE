using UnityEngine;
using System.Collections;
using DG.Tweening;

public class script_Asteroid : MonoBehaviour 
{
    public GameObject[] WayPoints = new GameObject[2];

    public void Q_Move(int WayPointIndex, float duration, bool queue)
    {
        transform.DOMove(WayPoints[WayPointIndex].transform.position, duration);
    }
}
