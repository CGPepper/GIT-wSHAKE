using UnityEngine;
using System.Collections;
using Fabric;

public class script_SoundManager : MonoBehaviour 
{
    // /////////////////
    //       Setup
    // /////////////////
    //private GameObject go_UI;
    //private GameObject go_objectCollector;

    void Awake()
    {
        //will cross reference this object with UI_Canvas and go_Models
        script_GameManager.Instance.SetupGameObjects(2, gameObject);
    }

    //Start function, activated when all sub-scenes are ready onAwake
    public void SetupObject(GameObject UI, GameObject objectCollector)
    {
        //go_UI = UI;
        //go_objectCollector = objectCollector;
    }

    // /////////////////
    //       Methods
    // /////////////////

    public void PlayUI(string param)
    {
		Debug.Log (param);
		string eventName = "ui/" + param;
		Fabric.EventManager.Instance.PostEvent (eventName);
    }

    public void Play3D(string param, GameObject go)
    {
		string eventName = "3D/" + param;
		Fabric.EventManager.Instance.PostEvent (eventName, go);
    }

    public void PlayVO(string CharType, string SoundGroup)
    {
        string eventName = "vo/" + CharType + "/" + SoundGroup;
		Fabric.EventManager.Instance.PostEvent(eventName);
    }

	public void StopVO(string CharType, string SoundGroup)
	{
		string eventName = "vo/" + CharType + "/" + SoundGroup;
		Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.StopSound);
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
