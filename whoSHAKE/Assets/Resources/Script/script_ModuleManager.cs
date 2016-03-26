using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            sc_UI.ShowSlider();
            sc_ObjectCollector.InteractibleObjects[0].GetComponent<script_Asteroid>().Reset();
          
        }
        else if (condition == "Hide")
        { 
            
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
            sc_UI.HideUIFrame();
            PrepModule("Show");
        }
        else if (condition == "Show")
        {
            sc_UI.ShowOverview();
            sc_UI.SetupPlayerOverview();
            sc_UI.UpdateTotPopUI();
            sc_ObjectCollector.InteractibleObjects[0].GetComponent<script_Asteroid>().Miss();
        }
    }

    /* ***********************************************************
   ********************** Preparation Module *********************
   *********************************************************** */

    public void PrepModule(string condition)
    {
        if (condition == "Show")
        {
            script_GameManager.Instance.NextRound();
            sc_UI.UI_Elements[12].GetComponent<script_Preparation>().SetupPreparations();
            
        }
        if (condition == "Hide")
        {
            sc_UI.ShowUIFrames();
            SliderModule("Show");
        }
    }

    /* ***********************************************************
   ********************** Visual Module *********************
   *********************************************************** */
    public void VisualModule()
    { 
        
    }
    /* ***********************************************************
   ********************** Setup Player Props *********************
   *********************************************************** */
    public void SetupPlayerProps()
    {
        script_Player scPlayer;
        sc_ObjectCollector.ShuffleStructures();
        GameObject[] Structures = sc_ObjectCollector.Structures;
        List<GameObject> ActivePlayers = script_GameManager.Instance.GetActivePlayers();
        int index = 0;
        foreach (GameObject go in ActivePlayers)
        {
            scPlayer = go.GetComponent<script_Player>();
            scPlayer.SetProps(Structures[index]);
            index++;
        }
    }
    
    
    
    
}
