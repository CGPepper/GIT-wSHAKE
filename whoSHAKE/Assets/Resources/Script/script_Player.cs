using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class script_Player : MonoBehaviour {

    public string       Name            { get; set; }
    public GameObject   Name_Tag_Hud    { get; set; }
    public GameObject   Name_Tag_World  { get; set; }
    public GameObject   Icon_Hud        { get; set; }

    public int Color;
    public GameObject location { get; set; }
    public bool b_Active = true;
    public bool b_Eliminated = false;
    public bool b_AI = true;
    public bool b_Me = false;
    public GameObject go_SpownPoint;
    public GameObject Character         { get; set; }
    public AudioSource AudioSource      { get; set; }

    public static GameObject go_UI;
    public static GameObject go_Collector;

    // 0 Population 1 Army 2 Food 3 Rest 4 Dance 5 SetArmy 6 SetFood 7 SetRest 8 SetDance
    public int[] Stats = new int[9];
    //
    void Awake()
    {
        Name = "Player1";
        Stats[0] = 1000;
    }

    public static void SetupRefObjects(GameObject ui, GameObject col)
    {
        go_UI = ui;
        go_Collector = col;
    }

    public void setSpown(GameObject go)
    {
        go_SpownPoint = go;
        Name_Tag_World = go_SpownPoint.transform.Find("Canvas/name_Spown").gameObject;
        Name_Tag_World.SetActive(b_Active);
        DrawName();
    }


    public void ChangeName(string name)
    {
        Name = name;
        DrawName();
    }

    public void DrawName()
    {
        Name_Tag_World.GetComponent<Text>().text = Name;
        Name_Tag_Hud.GetComponent<InputField>().text = Name;
    }

    public void ShowHide(bool state)
    {
        b_Active = state;
        Name_Tag_World.SetActive(state);
        //Name_Tag_Hud.SetActive(state);
        Name_Tag_Hud.transform.parent.gameObject.SetActive(state);
        Character.SetActive(state);
        if (b_Active)
        {
            Character.GetComponent<script_Character>().Spawn();
        }
    }

    public void SetCharacter(GameObject character)
    {
        Character = character;
        Transform spown_Dummy = go_SpownPoint.transform.FindChild("dummyspown");
        Character.transform.position = spown_Dummy.transform.position;
        Character.transform.rotation = spown_Dummy.transform.rotation;
        Icon_Hud.GetComponent<Image>().sprite = Character.GetComponent<script_Character>().icon;
        //Play sound
        if(b_Me)
        {
            AudioClip[] clip = Character.GetComponent<script_Character>().Voices_Generic;
            
			// FIXME
			//go_UI.GetComponent<script_UI>().sound_Charater(clip);
        }        
    }

    public void SetValues(int index, float value)
    {
        index = index + 4;
        Stats[index] = (int)value;
    }

}
