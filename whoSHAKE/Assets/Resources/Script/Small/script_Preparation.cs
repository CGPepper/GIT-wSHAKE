using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class script_Preparation : MonoBehaviour 
{
    public GameObject[] Elements = new GameObject[3];
    private int Round;
    private int TotDance;
    private int Time;

    public void SetupPreparations()
    {
        Round = script_GameManager.Instance.GlobalValues[0];
        TotDance = script_GameManager.Instance.GlobalValues[2];
        Time = script_GameManager.Instance.GlobalValues[3];
    }


	
}
