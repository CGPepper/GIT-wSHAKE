using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class script_Settings : MonoBehaviour 
{
    public GameObject[] Elements = new GameObject[6];
    private script_SoundManager scSound;
    private bool bActive = false;
    private bool bLockEscape = false;

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
        LockEscape();
        Elements[0].GetComponent<CanvasGroup>().DOFade(1, 0.3f).OnComplete(LockEscape);
        Elements[2].GetComponent<DOTweenAnimation>().DOPlayById("SettingsShow");
        Elements[7].GetComponent<DOTweenAnimation>().DOPlayById("HideSettings");
    }
    public void Hide()
    {
        PlayClick();
        bActive = false;
        Elements[2].GetComponent<DOTweenAnimation>().DOPlayById("SettingsHide");
        LockEscape();
        Elements[0].GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(Disable);
        
    }

    public void Escape()
    {
        if (!bLockEscape)
        {
            if (bActive)
            {
                if (Elements[8].activeInHierarchy && !Elements[2].activeInHierarchy)
                    CreditsClose();
                else if (Elements[2].activeInHierarchy)
                    Hide();
            }
            else
                Show();
        }
    }

    

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        script_GameManager.Instance.Reset();
    }

    public void Credits()
    {
        PlayClick();
        //Elements[2].GetComponent<DOTweenAnimation>().DOPlayById("SettingsHide");
        Elements[2].SetActive(false);
        Elements[8].SetActive(true);
        Elements[8].GetComponent<CanvasGroup>().alpha = 0;
        Elements[8].GetComponent<CanvasGroup>().DOFade(1, 0.6f);

    }

    public void CreditsClose()
    {
        PlayClick();
        Elements[8].GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(DisableCredits);
        

    }

    public void LockEscape() //prevents bugs when player presses escapes before the animation is finished
    {
        bLockEscape = !bLockEscape;
        Debug.Log(bLockEscape);
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
        LockEscape();
        Elements[0].SetActive(false);
        Elements[7].GetComponent<DOTweenAnimation>().DOPlayById("ShowSettings");
    }

    private void DisableCredits()
    {
        Elements[8].SetActive(false);
        Elements[2].SetActive(true);
        Elements[2].GetComponent<DOTweenAnimation>().DOPlayById("SettingsShow");
    }
}
