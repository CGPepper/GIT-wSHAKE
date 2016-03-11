﻿using UnityEngine;
using System.Collections;
using Fabric;

public class script_SoundManager : MonoBehaviour 
{
    // /////////////////
    //       Setup
    // /////////////////
    private GameObject go_UI;
    private GameObject go_objectCollector;

    void Awake()
    {
        //will cross reference this object with UI_Canvas and go_Models
        script_GameManager.Instance.SetupGameObjects(2, gameObject);
    }

    //Start function, activated when all sub-scenes are ready onAwake
    public void SetupObject(GameObject UI, GameObject objectCollector)
    {
        go_UI = UI;
        go_objectCollector = objectCollector;
    }

    // /////////////////
    //       Methods
    // /////////////////

	public void PlayButton()
	{
		Fabric.EventManager.Instance.PostEvent ("ui/button");
	}

	public void PlaySlider()
	{
		Fabric.EventManager.Instance.PostEvent ("ui/slider");
	}

	public void PlayWhoosh()
	{
		Fabric.EventManager.Instance.PostEvent ("ui/whoosh");
	}

	public void PlayPageFlip()
	{
		Fabric.EventManager.Instance.PostEvent ("ui/pageflip");
	}
	
	public void PlayClick ()
	{
	}

	public void PlayIntro(string index)
	{
		//Fabric.EventManager.Instance.PostEvent ("ui/slider");
		switch (index) 
		{
			case "0":
			Fabric.EventManager.Instance.PostEvent ("vo/intro/whoshake");
				break;
			case "1":
				Fabric.EventManager.Instance.PostEvent ("vo/intro/whoshake_itsyou1");
				break;
			case "2":
				Fabric.EventManager.Instance.PostEvent ("vo/intro/distance_explosion");
				break;
			case "3":
				Fabric.EventManager.Instance.PostEvent ("vo/intro/whoshake_itsyou2");
				break;
			case "4":
				Fabric.EventManager.Instance.PostEvent ("vo/intro/type");
				break;
			case "5":
				Fabric.EventManager.Instance.PostEvent ("vo/intro/save_peasants");
				break;
			case "6":
				Fabric.EventManager.Instance.PostEvent ("vo/intro/too_kind");
				break;
			case "7":
				Fabric.EventManager.Instance.PostEvent ("vo/intro/camera_intro");
				break;
		}
	}
}
