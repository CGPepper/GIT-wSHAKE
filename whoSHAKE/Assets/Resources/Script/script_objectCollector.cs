using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class script_objectCollector : MonoBehaviour 
{
    public GameObject[] InteractibleObjects = new GameObject[3];
    public GameObject[] spownPoints = new GameObject[5];
    public GameObject[] spownNameTag = new GameObject[5];
    //public int[] GlobalValues = new int[4]; // 1 Round 2 Total Dance required
    private List<GameObject> spownAvailable = new List<GameObject>();

    private Color[] colors = new Color[5] { Color.red, Color.blue,Color.yellow,Color.green,Color.cyan }; 
    private List<Color> colorsAvailable = new List<Color>();

    [SerializeField]
    private string[] namesAI = new string[10] { "Spidergentleman", "X-Menly", "SchwarzeNooger", "HairyPotter", "NewbSlayer", "LoveBieber", "ChrisRoberts", "PinkEye", "YouKnowWho", "ThanksObama" };
    private List<string> namesAI_available = new List<string>();

    public GameObject[] nameTags = new GameObject[5];
    public GameObject[] PLAYERS = new GameObject[5];
    public GameObject[] Characters = new GameObject[7];
    public GameObject[] Structures = new GameObject[5];
    public GameObject[] StructuresSpown = new GameObject[5];
    public List<GameObject> charactersAvailabe = new List<GameObject>();
    //public GameObject[] _PLAYERS { get { return PLAYERS; } }
    [SerializeField]
    private GameObject prefab_PLayer;
    [SerializeField]
    private GameObject go_UI;
    //private GameObject go_SoundManager;

	// Use this for initialization
	void Awake()
    {
        script_GameManager.Instance.SetupGameObjects(1, gameObject);
	}

    public void SetupObject(GameObject goUI,GameObject goSoundManager)
    {
        go_UI = goUI;
        //go_SoundManager = goSoundManager;
        SetupObjects();
        AssignPlayerToSpown();
        script_GameManager.Instance.objectCollector = gameObject;
    }
	

    public void SetupObjects()
    {
        spownAvailable.Clear();
        foreach (GameObject go in spownPoints)
        {
            spownAvailable.Add(go);
            go.SetActive(false);
            if (go.name == "SpownPoint 1")
            {
                GameObject go_child = go.transform.FindChild("dummyspown").gameObject;
                go_child.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }

        colorsAvailable.Clear();
        foreach (Color co in colors)
        {
            colorsAvailable.Add(co);
        }

        ResetAiNames();
        //Setup Players
        for (int i = 0; i < 5; i++)
        {
            PLAYERS[i] = Instantiate(prefab_PLayer);
            script_Player script = PLAYERS[i].GetComponent<script_Player>();

            //Assign 2D name inputfield
            GameObject[] inputField = go_UI.GetComponent<script_UI>()._hud_PlayerDetails;
            script.Name_Tag_Hud = inputField[i].transform.Find("holder_1/InputField").gameObject;
            script.Icon_Hud = inputField[i].transform.Find("holder_1/Button").gameObject;
            //set index 0 to player
            if (i == 0) { script.b_AI = false; }
            //Assign Spown Point
            script.setSpown(spownPoints[i]);
            //set 
            if (i == 0) { script.b_Me = true; }
        }
        script_Player.SetupRefObjects(go_UI, gameObject);
    }

    public static void ShuffleArray<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    public void ResetAiNames()
    {
        namesAI_available.Clear();
        foreach (string s in namesAI)
        {
            namesAI_available.Add(s);
        }
    }
        
    public void AssignPlayerToSpown()
    {
        int count = 5;
        while (count > 0)
        {
            //get random spown
            int randSpown = Random.Range(0, spownAvailable.Count);
            GameObject selectedSpown = spownAvailable[randSpown];
            selectedSpown.SetActive(true);

            //get random color
            int randColor = Random.Range(0, colorsAvailable.Count);
            Color selectedColor = colorsAvailable[randColor];
            GameObject  child = selectedSpown.transform.FindChild("dummyspown").gameObject;
            child.GetComponent<Renderer>().material.SetColor("_Color", selectedColor);

            //cleanup
            spownAvailable.Remove(selectedSpown);
            colorsAvailable.Remove(selectedColor);
            count--;
        }  
    }

    public List<string> InitAIPlayers()
    {
        List<string> nameList = new List<string>();
        ResetAiNames();
        for (int i = 1; i <= PLAYERS.Length; i++)
        {
            
            int rand = Random.Range(0, namesAI_available.Count);
            string s = namesAI_available[rand];
            namesAI_available.RemoveAt(rand);
            nameList.Add(s);

        }
        return nameList;
    }


    public void HideUnusedGameTags(int number)
    {
        int count = 0;
        foreach (GameObject go in nameTags)
        {
            if (count >= number)
                go.SetActive(false);
            else
                go.SetActive(true);
            count++;
        }
    }

    public void ShufflePlayerLocation()
    {
        ShuffleArray<GameObject>(spownPoints);
        foreach (GameObject go in spownNameTag)
            go.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            script_Player player = PLAYERS[i].GetComponent<script_Player>();
            player.setSpown(spownPoints[i]);
        }
    }

    public void ShuffleStructures()
    {
        ShuffleArray<GameObject>(Structures);
    }
    /** ---------------------------------------------
     * Characters
     * --------------------------------------------**/
    public void ShuffleCharacters()
    {
        ShuffleArray<GameObject>(Characters);
        charactersAvailabe.Clear();
        foreach (GameObject go in Characters)
        {
            charactersAvailabe.Add(go);
            go.SetActive(false);
        }
        script_Player script_player;
        GameObject random_Character;
        for (int i = 0; i < 5; i++)
        {
            script_player = PLAYERS[i].GetComponent<script_Player>();
            random_Character = charactersAvailabe[Random.Range(0, charactersAvailabe.Count)];
            charactersAvailabe.Remove(random_Character);
            script_player.SetCharacter(random_Character);
        }
    }
    

}
  