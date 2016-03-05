using UnityEngine;
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
    //       Logic
    // /////////////////

    //Will receive calls from other scripts saying: I want to play a sound with this ID, so many seconds
	// FIXME duration? lets talk about that later
	public void PlaySound(string eventName, GameObject obj = null)
    { 
		Fabric.EventManager.Instance.PostEvent (eventName, obj);
    }







}
