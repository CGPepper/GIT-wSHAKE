using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class script_UI : MonoBehaviour 
{
    public bool test = true;
    //this is a new line
    //change something else
    [SerializeField]
    private AudioClip[] wooshes = new AudioClip[6];
    [SerializeField]
    private AudioClip[] miscSounds = new AudioClip[2];
    //[SerializeField]
    //private AudioClip[] voiceSounds = new AudioClip[4];
    [SerializeField]
    private AudioClip[] clickSounds = new AudioClip[6];
    [SerializeField]
    private AudioClip[] buttonSounds = new AudioClip[6];
    [SerializeField]
    private AudioClip[] pageSounds = new AudioClip[4];
    [SerializeField]
    private AudioClip[] sliderEndSounds = new AudioClip[4];
    [SerializeField]
    private Material skybox;
    [SerializeField]
    private GameObject[] initializeUiElements = new GameObject[1];
    [SerializeField]
    private GameObject[] hud_PlayerDetails = new GameObject[5];
    [SerializeField]
    private Sprite[] NrPlayersSprite = new Sprite[2];
    [SerializeField]
    private GameObject[] NrPlayersButton = new GameObject[4];
    [SerializeField]
    private GameObject[] InteractibleElements = new GameObject[10];
    [SerializeField]
    private GameObject OverviewPrefab;
    
   
    private float sky_rotation = 300f;
    private AudioSource AudioSourceUI;
    private AudioSource AudioSourceVoice;
    delegate void DelayedMethod(string id); //used to delay sound playback with a coroutine
    private DelayedMethod tempDelegate;
    private script_objectCollector objectCollector;
    private int playerChooseState = 0;
    private int PlayerPopAvailable = 0;
    private bool SliderMaxed = false;
    private Color SliderMaxedColor = new Color32(141,141,141,255);
    private List<GameObject> OverviewList = new List<GameObject>();
    

    //public
    public bool clickSoundAllowed = false;
    public GameObject[] _hud_PlayerDetails { get { return hud_PlayerDetails; } }

    void Awake()
    {
        DOTween.Init();
        script_GameManager.Instance.SetupGameObjects(0, gameObject);
    }
    public void SetupObject(GameObject go)
    {
        objectCollector = go.GetComponent<script_objectCollector>();
        AudioSourceUI = GetComponent<AudioSource>();
        AudioSourceVoice = gameObject.transform.Find("VoiceSource").GetComponent<AudioSource>();
        if (!test)
        {
            UI_Start();
        }
        else
        {
            Debug.Log("Executing in test mode - script_UI - test=true");
            CallAction("test");
        }
    }

    void Update()
    {
        RotateSky();
        if (clickSoundAllowed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                sound_PlayClick();
            }
        }
        
        
    }

    private void UI_Start()
    {
        foreach (GameObject go in initializeUiElements)
        {
            go.SetActive(true);
        }
        Camera.main.transform.rotation = Quaternion.Euler(37, 250, 350);
        Camera.main.transform.position = new Vector3(11.5f, 5f, -50f);  
        gameObject.transform.Find("Fadescreen/whoSHAKE").gameObject.SetActive(true);
        initializeUiElements[0].GetComponent<CanvasRenderer>().SetAlpha(1f);
        GameObject text_who = gameObject.transform.Find("Fadescreen/whoSHAKE/text_who").gameObject;
        text_who.GetComponent<DOTweenAnimation>().DOPlayById("whoShakeStart");
        StartCoroutine(delayMethod(0.5f,sound_PlayMisc,"0"));
    }

    /**
    Loops
    **/
    private void RotateSky()
    {
        sky_rotation += Time.deltaTime / 10;
        if (sky_rotation >= 360) sky_rotation -= 360; 
        skybox.SetFloat("_Rotation", sky_rotation);
    }


    /**
    Playing Sounds
    **/
    



    public void sound_PlayMisc(string index)
    {
        int i = int.Parse(index);
        AudioSourceUI.PlayOneShot(miscSounds[i],1f);
    }

    public void sound_PlayVoice(string index)
    {
        int i = int.Parse(index);
        AudioSourceVoice.PlayOneShot(miscSounds[i], 1f);
    }

    public void sound_Charater(AudioClip[] clipArray) 
    {
        AudioSourceVoice.Stop();
        int rand = Random.Range(0, clipArray.Length);
        AudioSourceVoice.PlayOneShot(clipArray[rand]);
    
    }

    //Random generic sounds
    public void sound_PlayClick()   { sound_Randomizer(clickSounds, 1f); }
    public void sound_PlayButton()  { sound_Randomizer(buttonSounds, 1f);  }
    public void sound_PlayPaper()   { sound_Randomizer(pageSounds, 0.5f);  }
    public void sound_Woosh()       { sound_Randomizer(wooshes, 0.2f);  }
    public void sound_SliderEnd()  { sound_Randomizer(sliderEndSounds, 0.1f); }
    
    private void sound_Randomizer(AudioClip[] clipArray, float volume)
    {
        int rand = Random.Range(0, clipArray.Length);
        AudioSourceUI.PlayOneShot(clipArray[rand],volume);
    }

    /**
    Coroutines
    **/

    //Delay method
    IEnumerator delayMethod(float time, DelayedMethod del, string param)
    {
        yield return new WaitForSeconds(time);
        del(param);
    }

    /**
    Coroutines
    **/

    public void CallAction(string id)
    {
        if (id == "endWhoShake")
        {
            StartCoroutine(delayMethod(2.8f, gameObject.transform.Find("Fadescreen").gameObject.GetComponent<DOTweenAnimation>().DOPlayById, "fadescreen_in")); //fade in
            StartCoroutine(delayMethod(3f, gameObject.transform.Find("Fadescreen/whoSHAKE/text_who").gameObject.GetComponent<DOTweenAnimation>().DOPlayAllById, "whoShakeEnd")); //remove text
            StartCoroutine(delayMethod(3.1f, gameObject.transform.Find("Fadescreen/text_WhoShake").gameObject.GetComponent<DOTweenAnimation>().DOPlayById, "showWhoShake")); //start typing text
            StartCoroutine(delayMethod(6f, gameObject.transform.Find("Fadescreen/text_WhoShake/text_WhoShake_GGJ").gameObject.GetComponent<DOTweenAnimation>().DOPlayAllById, "showWhoShake_GGJ")); //show ggj2016
            StartCoroutine(delayMethod(2.2f,sound_PlayMisc,"3")); //play sound
            StartCoroutine(delayMethod(8.5f, gameObject.transform.Find("Fadescreen/text_WhoShake").gameObject.GetComponent<DOTweenAnimation>().DOPlayById, "hideWhoShake")); //start typing text
            StartCoroutine(delayMethod(6.5f, Camera.main.GetComponent<DOTweenAnimation>().DOPlayById, "camera1"));
            //StartCoroutine(delayMethod(6.5f, sound_PlayMisc, "7"));
            StartCoroutine(delayMethod(10f, gameObject.transform.Find("UI_Main/frame_GoodLord").gameObject.GetComponent<DOTweenAnimation>().DOPlayById, "GoodLordStart"));

        }
        else if (id == "test")
        {
            foreach (GameObject go in initializeUiElements)
            {
                go.SetActive(true);
            }
            Camera.main.transform.rotation = Quaternion.Euler(0,0,0);
            Camera.main.transform.position = new Vector3(0, 1, -10);  
            initializeUiElements[0].SetActive(false); //Canvas - fadescreen
            SetPlayers(5);
            CallAction("StartSingle");
            //StartCoroutine(delayMethod(1f, gameObject.transform.Find("UI_Main/frame_GoodLord").gameObject.GetComponent<DOTweenAnimation>().DOPlayById, "GoodLordStart"));
        }
        else if (id == "GoodLord_Ok")
        {
            AudioSourceVoice.Stop();
            Q_Tween(0, "UI_Main/frame_GoodLord", "GoodLordEnd", false);//dont forget to disable
            Q_Tween(0, "UI_Main/frame_2Frame", "modeSelectIn", false);

        }
        else if (id == "singpleplayer")
        {
            Q_Tween(0, "UI_Main/frame_2Frame", "modeSelectOut", false);
            Q_Tween(0, "UI_Main/frame_PlayerSelect", "playerSelectIn", false);
            if (playerChooseState == 0)
                SetPlayers(5);
        }
        else if (id == "backToMode")
        {
            Q_Tween(0, "UI_Main/frame_2Frame", "modeSelectIn", false);
            Q_Tween(0, "UI_Main/frame_PlayerSelect", "playerSelectOut", false);
        }
        else if (id == "StartSingle")
        {
            Q_Tween(0, "UI_Main/frame_PlayerSelect", "playerSelectOut", false);
            Q_Tween(0, "Frame_Upper", "ShowFrames", true);
            Q_Tween(0.3f, "Button_Settings", "ShowSettings", false);
            Q_Tween(1f, "Sliders", "ShowSliders", false);
            TimerStart();
        }
        else if (id == "endSelection")
        {
            Q_Tween(0, "Sliders", "HideSliders", false);
            SetupPlayerOverview();
        }

    }

    private void Q_Tween(float time,string path, string id, bool groupTrigger)
    {
        DOTweenAnimation comp = gameObject.transform.Find(path).gameObject.GetComponent<DOTweenAnimation>();
        tempDelegate = groupTrigger ? new DelayedMethod(comp.DOPlayAllById) : new DelayedMethod(comp.DOPlayById);
        /**
        if (groupTrigger)
            tempDelegate = comp.DOPlayAllById;
        else
            tempDelegate = comp.DOPlayById;
        **/
        if (time > 0)
        {
            //StartCoroutine(delayMethod(time, comp.DOPlayById, id));
            StartCoroutine(delayMethod(time, tempDelegate, id));
        }
        else 
        {
            //comp.DOPlayAllById(id);
            tempDelegate(id);
        }  
    }

    //Selecting number of (AI) players
    public void SetPlayers(int number)
    {
        objectCollector.ShufflePlayerLocation();
        objectCollector.ShuffleCharacters();
        //assign names to hud
        List<string> names = objectCollector.InitAIPlayers();
        GameObject[] players = objectCollector.PLAYERS;
        for (int i = 0; i < 5; i++)
        {
            script_Player script = players[i].GetComponent<script_Player>();
            if (script.b_AI)
            {
                script.ChangeName(names[i]);
            }
            bool toHide = (i > number - 1) ? false : true;
            script.ShowHide(toHide);
        }
        
        //change button
        int count = 0;
        int indexReference = number - 2;
        Sprite tempSprite;
        foreach(GameObject go in NrPlayersButton)
        {
            tempSprite = (count == indexReference) ? NrPlayersSprite[1] : NrPlayersSprite[0];
            go.GetComponent<Image>().sprite = tempSprite;
            count++;
        }
        
    }

    public void SlidersSetup()
    {
        int playerNumber = 0;
        PlayerPopAvailable = objectCollector.PLAYERS[playerNumber].GetComponent<script_Player>().Stats[0];      

        for (int i = 1; i < 5; i++)
        {
            GameObject slider = InteractibleElements[i];
            //GameObject slider_text = slider.transform.FindChild("text_Value").gameObject;
            slider.GetComponent<Slider>().value = 0;
            slider.GetComponent<Slider>().maxValue = PlayerPopAvailable;  //set slider max to player population
            //slider_text.GetComponent<Text>().text = "0";
        }
        InteractibleElements[5].GetComponent<Text>().text = PlayerPopAvailable.ToString();
        
    }

    public void SliderMove(int index)
    {
        float totalValue=0; 
        float tempValue = 0;
        GameObject _slider;
        for (int i = 1; i < 5; i++)
        {
            _slider = InteractibleElements[i];
            tempValue = _slider.GetComponent<Slider>().value;
            totalValue += tempValue;
        }
        GameObject slider = InteractibleElements[index];
        GameObject slider_text = slider.transform.FindChild("text_Value").gameObject;
        float value = slider.GetComponent<Slider>().value;
        float difference = 0;
        if (totalValue >= PlayerPopAvailable)
        {
            difference = PlayerPopAvailable - totalValue;
            value += difference;
            slider.GetComponent<Slider>().value = value;
            if (!SliderMaxed)
            {

                SliderMaxed = true;
                SliderSetHandle(SliderMaxedColor);
            }
        }
        else if (SliderMaxed)
        {
            SliderMaxed = false;
            SliderSetHandle(Color.white);
        }
        slider_text.GetComponent<Text>().text = value.ToString();
        int available = PlayerPopAvailable - (int)totalValue - (int)difference;
        InteractibleElements[5].GetComponent<Text>().text = available.ToString();
        //Store Values
        GameObject Me = script_GameManager.Instance.GetCurrentPlayer(objectCollector.PLAYERS);
        Me.GetComponent<script_Player>().Stats[index + 4] = (int)value;
        //Tooltip
        script_Tooltip scTooltip = InteractibleElements[8].GetComponent<script_Tooltip>();
        int stock = Me.GetComponent<script_Player>().Stats[index];
        script_GameManager.Instance.HideTooltipsText(scTooltip.ReferenceTooltip);
        scTooltip.ReferenceTooltip[index - 1].SetActive(true);
        //IF Food
        if (index == 2) 
        {
            scTooltip.ReferenceFood[0].GetComponent<TextMeshProUGUI>().text = value.ToString();
            int[] calc = script_GameManager.Instance.calculateByFood(PlayerPopAvailable, value, stock);
            scTooltip.ReferenceFood[1].GetComponent<TextMeshProUGUI>().text = calc[3] + "%";
            scTooltip.ReferenceFood[2].GetComponent<TextMeshProUGUI>().text = calc[2].ToString();
            scTooltip.ReferenceFood[3].GetComponent<TextMeshProUGUI>().text = calc[0].ToString();
            scTooltip.ReferenceFood[4].GetComponent<TextMeshProUGUI>().text = calc[1].ToString();
        }
        //IF Rest
        if (index == 3)
        {
            int[] calc = script_GameManager.Instance.calculateByRest(Me);
            for (int r = 0; r < 4; r++)
            {
                scTooltip.ReferenceRest[r].GetComponent<TextMeshProUGUI>().text = calc[r].ToString();
            }
        }
        //IF Dance
        if (index == 4)
        {
            string[] calc = script_GameManager.Instance.calculateByDance(Me);
            //0 Combined Dance 1 Player Total Dance 2 Most points name 3 most points points 4 Least points Name 5 Least points points 6 Dance Gain
                scTooltip.ReferenceDance[0].GetComponent<TextMeshProUGUI>().text = calc[0];
                scTooltip.ReferenceDance[1].GetComponent<TextMeshProUGUI>().text = calc[1];

                scTooltip.ReferenceDance[2].GetComponent<TextMeshProUGUI>().text = calc[4] + " has least dancing points " + calc[5];
                scTooltip.ReferenceDance[3].GetComponent<TextMeshProUGUI>().text = calc[2] + " has Most dancing points " + calc[3];
        }
        //IF Army
        if (index == 1)
        {
            int[] calc = script_GameManager.Instance.calculateByArmy(Me);
            scTooltip.ReferenceArmy[0].GetComponent<TextMeshProUGUI>().text = calc[0].ToString();
            scTooltip.ReferenceArmy[1].GetComponent<TextMeshProUGUI>().text = calc[1].ToString();
            scTooltip.ReferenceArmy[2].GetComponent<TextMeshProUGUI>().text = calc[2].ToString();
            scTooltip.ReferenceArmy[3].GetComponent<TextMeshProUGUI>().text = calc[3].ToString();
        }
    }


    private void SliderSetHandle(Color color)
    {
        for (int z = 1; z < 5; z++)
        {
            GameObject _handle = InteractibleElements[z].transform.Find("Handle Slide Area/Handle").gameObject;
            _handle.GetComponent<Image>().color = color;
            sound_SliderEnd();
        }

    }

    public void SlidersDone()
    {
        GameObject _slider;
        float tempValue;
        float[] values = new float[5];
        int playerNumber = 0;
        for (int i = 1; i < 5; i++)
        {
            _slider = InteractibleElements[i];
            tempValue = _slider.GetComponent<Slider>().value;
            objectCollector.PLAYERS[playerNumber].GetComponent<script_Player>().SetValues(i, values[i]);
            values[i] = tempValue;
        }
        
    }


    IEnumerator SetupCountdown(float time, float step, DelayedMethod loopMethod, DelayedMethod endMethod )
    {
        if (step == 0) step = Time.deltaTime; 
        while (true)
        {
            
            if (time <= 0)
            {
                endMethod("");
                yield break;
            }
            else
            {
                loopMethod(step.ToString());
                yield return new WaitForSeconds(step);
            }
            time -= step;
        }
    }

    private void TimerStart()
    {
        
        float time = 60f;
        Slider timer = InteractibleElements[6].GetComponent<Slider>();
        timer.maxValue = time;
        timer.value = time;
        DelayedMethod loopMethod = TimerLoop;
        DelayedMethod endMethod = TimerEnd;
        StartCoroutine(SetupCountdown(time, 0f, loopMethod, endMethod));
    }

    private void TimerLoop(string param)
    {
        Slider timer = InteractibleElements[6].GetComponent<Slider>();
        timer.value -= float.Parse(param);
    }

    private void TimerEnd(string param)
    { 
        
    }


    private void SetupPlayerOverview()
    {
        //Get qualified players
        GameObject[] PLAYERS = objectCollector.PLAYERS;
        List<GameObject> Players = new List<GameObject>();
        script_Player script;
        foreach (GameObject player in PLAYERS)
        {
            script = player.GetComponent<script_Player>();
            if (script.b_Active && !script.b_Eliminated)
            {
                Players.Add(player);
            }
        }
        //calculations
        float[] total = SetRandomAiValues(Players);
        //Cards
        int playersCount = Players.Count;
        float width = 198f;
        float totalWidth = width *(float)playersCount;
        float x = 0f;
        GameObject frame;
        Vector3 position = new Vector3(0,0,0);
        int index = 0;
        foreach (GameObject player in Players)
        {
            x = -totalWidth / 2 + (width/2) + index * width;
            
            position.Set(x, 0, 0);
            script = player.GetComponent<script_Player>();
            frame = (GameObject)Instantiate(OverviewPrefab, position, Quaternion.identity);
            OverviewList.Add(frame);
            frame.transform.SetParent(InteractibleElements[7].transform,false);
            frame.name = "PlayerFrame";
            SetCardValues(script, frame,total);
            index++;
        } 
    }

    private void SetCardValues(script_Player player, GameObject frame,float[] total)
    {
        Text _text = frame.transform.Find("NameFrame/Name").gameObject.GetComponent<Text>();
        _text.text = player.Name;
        Outline _outline = frame.GetComponent<Outline>();
        if (player.b_Me) _outline.enabled = true;
        int pop = player.Stats[0];
        //Calculate and set food
        int foodSet = player.Stats[6];
        int foodStored = player.Stats[2];
        int[] FoodCalc = script_GameManager.Instance.calculateByFood(pop, foodSet, foodStored);
        Debug.Log(player.Name + " " + foodSet.ToString());
        int popByFood = FoodCalc[0];
        string addonString = "+";
        if (popByFood < 0) addonString = "";
        string s_popByFood = addonString + popByFood.ToString();
        _text = frame.transform.Find("PopGain/byFood/Food").gameObject.GetComponent<Text>();
        _text.text = s_popByFood;
        //Calculate and set Rest

    }

    private float[] SetRandomAiValues(List<GameObject> Players)
    { 
        script_Player script;
        float[] values = new float[5];
        float[] total = new float[4];
        int random;
        int population;

        foreach (GameObject player in Players)
        {
            script = player.GetComponent<script_Player>();
            if (script.b_AI)
            {
                values[0] = 0;
                int i;
                for (i = 1; i <= 4; i++)
                {
                    random = Random.Range(0, 10);
                    values[0] += random;
                    values[i] = random;
                }
                population = script.Stats[0];
                for (i = 1; i <= 4; i++)
                {
                    values[i] = population * (values[i] / values[0]);
                    script.Stats[i + 4] = (int)values[i];
                    //Debug.Log(script.Name + " int " + i + " is " + (int)values[i]);
                }
            }
            for (int x = 1; x <= 4; x++)
            {
                total[x - 1] += script.Stats[x + 4];
            }
        }
        return total;
    }
}
