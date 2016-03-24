using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class script_Settings : MonoBehaviour 
{
    public GameObject[] Elements = new GameObject[6];
    private script_SoundManager scSound;
    private bool bActive = false;

    public void SetupObject(GameObject ob_SoundManager)
    {
        scSound = ob_SoundManager.GetComponent<script_SoundManager>();
    }

    public void Show()
    {
        PlayClick();
        bActive = true;
        Elements[0].SetActive(true); //SettingsParent
        Elements[0].GetComponent<CanvasGroup>().alpha = 0;
        Elements[0].GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        Elements[2].GetComponent<DOTweenAnimation>().DOPlayById("SettingsShow");
        Elements[7].GetComponent<DOTweenAnimation>().DOPlayById("HideSettings");
    }

    public void Escape()
    {
        if (bActive)
            Hide();
        else
            Show();
    }

    public void Hide()
    {
        PlayClick();
        bActive = false;
        Elements[2].GetComponent<DOTweenAnimation>().DOPlayById("SettingsHide");
        Elements[0].GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(Disable);
    }

    public void Quit()
    {
        Application.Quit();
    }

    /** ***************************
     *      Helper Functions
     * ***************************/
    private void PlayClick()
    {
        scSound.PlayUI("button");
    }

    private void Disable()
    {
        Elements[0].SetActive(false);
        Elements[7].GetComponent<DOTweenAnimation>().DOPlayById("ShowSettings");
    }
}
