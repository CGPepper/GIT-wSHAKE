using UnityEngine;
using System.Collections;

public class script_ModuleManager : MonoBehaviour 
{
    /* ***********************************************************
    ************************** Setup *****************************
    *********************************************************** */
    private script_UI sc_UI;
    private script_objectCollector sc_ObjectCollector;


	void Awake()
    {
        script_GameManager.Instance.SetupGameObjects(3, gameObject);
    }

    public void SetupObject(GameObject goUI, GameObject goObjectCollector)
    {
        sc_UI = goUI.GetComponent<script_UI>();
        sc_ObjectCollector = goObjectCollector.GetComponent<script_objectCollector>();
    }


    /* ***********************************************************
    ************************* Slider Module **********************
    *********************************************************** */
    public void SliderModule(string condition)
    {
        if (condition == "Show")
        {
            script_GameManager.Instance.NextRound();
            sc_UI.ShowSlider();
        }
    }

    /* ***********************************************************
    ************************ Overview Module *********************
    *********************************************************** */
    public void OverviewModule(string condition)
    {
        if (condition == "Hide")
        {
            sc_UI.UI_Elements[10].GetComponent<script_CounterOverview>().StopTimer();
            sc_UI.HideOverview();
        }
    }

}
