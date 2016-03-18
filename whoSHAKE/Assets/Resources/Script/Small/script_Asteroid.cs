using UnityEngine;
using System.Collections;
using DG.Tweening;

public class script_Asteroid : MonoBehaviour 
{
    private bool InitialState = true;

    public GameObject[] WayPoints = new GameObject[2];



    public void Reset()
    {
        DOTween.Kill(gameObject.transform);
        if (InitialState)
        {
            Move();
            InitialState = false;
        }
        else
        {
            Rewind();
            transform.DOMove(WayPoints[1].transform.position, 0.5f).SetEase(Ease.OutBack).OnComplete(Move);
        }
        
    }

    public void Move()
    {
        float duration = script_GameManager.Instance.GlobalValues[3];
        transform.DOMove(WayPoints[2].transform.position, duration*2);
    }

    public void Miss()
    {
        DOTween.Kill(gameObject.transform);
        transform.DOMove(WayPoints[3].transform.position, 1f).SetEase(Ease.OutQuad).OnComplete(Rewind);
    }

    private void Rewind()
    {
        transform.position = WayPoints[0].transform.position;
    }

}
