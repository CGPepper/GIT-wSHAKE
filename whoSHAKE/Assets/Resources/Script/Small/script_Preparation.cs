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
    private int ShowIndex;
    private script_SoundManager sc_Sound;

    public void SetupObject(GameObject goSound)
    {
        sc_Sound = goSound.GetComponent<script_SoundManager>();
    }

    public void SetupPreparations()
    {
        Round = script_GameManager.Instance.GlobalValues[0];
        TotDance = script_GameManager.Instance.GlobalValues[2];
        Time = script_GameManager.Instance.GlobalValues[3];
        ShowIndex = 1;
        Show(ShowIndex);

    }

    private void Show(int index)
    {
        switch (index)
        { 
            case 1:
                SetComponent("ROUND", Round.ToString());
                Camera.main.GetComponent<script_Camera>().SetupPrep(3f);
                break;
            case 2:
                SetComponent("TOTAL DANCE", TotDance.ToString());
                Camera.main.GetComponent<script_Camera>().SetZoomPoints(3f);
                break;
            case 3:
                SetComponent("TIME",Time + "s");
                Camera.main.GetComponent<script_Camera>().SetZoomPoints(3f);
                break;
        }
        
    }


    private void SetComponent(string name, string number)
    {
        Elements[2].GetComponent<TextMeshProUGUI>().text = name;
        Elements[1].GetComponent<TextMeshProUGUI>().text = number;
        Elements[0].transform.localScale = new Vector3(0, 0, 0);
        Elements[0].SetActive(true);
        //Play
        float tweenTime = 0.4f;
        Elements[0].transform.DOScale(1f, tweenTime).SetEase(Ease.OutBack).OnComplete(Hold);
        Elements[0].GetComponent<CanvasGroup>().alpha = 0;
        Elements[0].GetComponent<CanvasGroup>().DOFade(1, tweenTime);
        sc_Sound.PlayUI("prepPopIn");
    }

    private void Hold()
    {
        Elements[0].transform.DOScale(1.2f,1f).SetEase(Ease.Linear).OnComplete(Out);
    }

    private void Out()
    {
        Elements[0].transform.DOScale(10, 0.3f).SetEase(Ease.Linear).OnComplete(Reset);
        Elements[0].GetComponent<CanvasGroup>().DOFade(0, 0.3f);
        sc_Sound.PlayUI("prepPopOut");
    }

    private void Reset()
    {
        Elements[0].SetActive(false);
        int NrOfPopups = 3;
        if (ShowIndex < NrOfPopups)
        {
            ShowIndex++;
            Show(ShowIndex);
        }
        else if (ShowIndex == NrOfPopups)
        {
            ShowIndex++;
            SetComponent("", "GO");
            Camera.main.GetComponent<script_Camera>().SetNeutral(1f);
            script_GameManager.Instance.MainObjects[3].GetComponent<script_ModuleManager>().PrepModule("Hide");
        }
        
    }

	
}
