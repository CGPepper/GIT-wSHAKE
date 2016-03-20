using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class script_Camera : MonoBehaviour 
{
    private Vector3 vNeutralPos = new Vector3(0, 1, -10);
    private Vector3 vNeutralRot = new Vector3(0, 0, 0);
    private Vector3 vCenteredPos = new Vector3(-3, 1, -7);
    private Vector3[,] vZoomPoints = new Vector3[3,2];
    private List<int> listZoom = new List<int>();

    void Awake()
    {
        vZoomPoints[0, 0] = new Vector3(-3,0,-6);
        vZoomPoints[0, 1] = new Vector3(-10, 2, 0);
        vZoomPoints[1, 0] = new Vector3(-0.2f, -0.2f, -7f);
        vZoomPoints[1, 1] = new Vector3(-7, -23, 4);
        vZoomPoints[2, 0] = new Vector3(-1.3f, 2.3f, -5.5f);
        vZoomPoints[2, 1] = new Vector3(16.5f, -12,-2);
    }

    public void SetupPrep(float duration)
    {
        listZoom.Clear();
        for (int i = 0; i <= 2; i++)
        {
            listZoom.Add(i);
        }
        SetCentered(duration);
    }


    public void SetNeutral(float duration)
    {
        Kill();
        transform.DOMove(vNeutralPos, duration).SetEase(Ease.OutExpo);
        transform.DORotate(vNeutralRot, duration).SetEase(Ease.OutExpo);
    }

    public void SetCentered(float duration)
    {
        Kill();
        transform.DOMove(vCenteredPos, duration).SetEase(Ease.OutExpo);
        transform.DORotate(vNeutralRot, duration).SetEase(Ease.OutExpo);
    }

    public void SetZoomPoints(float duration)
    {
        Kill();
        int Index = listZoom[Random.Range(0, listZoom.Count)];
        listZoom.Remove(Index);
        transform.DOMove(vZoomPoints[Index,0], duration).SetEase(Ease.OutExpo);
        transform.DORotate(vZoomPoints[Index,1], duration).SetEase(Ease.OutExpo);
    }

    private void Kill()
    {
        DOTween.Kill(transform);
    }

	
}
