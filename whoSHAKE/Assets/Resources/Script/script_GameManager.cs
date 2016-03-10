using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class script_GameManager : script_Singleton<script_GameManager> 
{
    public float FoodDivider = 10000f;
    public GameObject objectCollector;
    
    public GameObject[] MainObjects = new GameObject[3]; //0 UI, 1 objectCollector, 2 SoundManager
    public int[] GlobalValues = new int[4] {0,0,0,0}; // 0 Round 1 Total Population 2 Total Dance required
    

    public void SetupGameObjects(int index, GameObject go)
    {
        MainObjects[index] = go;
        if (MainObjects[0] && MainObjects[1] && MainObjects[2])
        {
            MainObjects[1].GetComponent<script_objectCollector>().SetupObject(MainObjects[0],MainObjects[2]);
            MainObjects[2].GetComponent<script_SoundManager>().SetupObject(MainObjects[0], MainObjects[1]);
            MainObjects[0].GetComponent<script_UI>().SetupObject(MainObjects[1], MainObjects[2]);
        }
    }





    public void NextRound()
    {
        int round = GlobalValues[0] + 1;
        int totPop = GetTotalStats(0);
        int danceMin = (int)( 200f * Mathf.Pow(2, round - 1) );
        GameObject player = GetCurrentPlayer();
        int ownPop = player.GetComponent<script_Player>().Stats[0];
        MainObjects[0].GetComponent<script_UI>().SetupRoundUI(round,totPop,ownPop,danceMin);
        GlobalValues[0] = round;
        GlobalValues[1] = totPop;
        GlobalValues[2] += danceMin; 

    }
    
    
    
   //-----------------------------
    //Calculations
    //-----------------------------

    //Army
    public int[] calculateByArmy(GameObject player)
    {
        int[] result = new int[5] { 0, 0, 0, 0, 0 }; // 0 new power 1 stock 2 
        script_Player scPlayer = player.GetComponent<script_Player>();
        int set = scPlayer.Stats[1];
        int stock = scPlayer.Stats[5];
        float foodFactor = 6f;
        float danceFactor = 13;
        result[0] = set + stock;
        result[1] = stock;
        result[2] = (int)(stock * foodFactor);
        result[3] = (int)(stock * danceFactor);
        result[4] = set;
        return result;
    }
    /**FOOD
     * If enough food is set by the slider, calculate surplus population and add overstock to currencies
     * If not enough food, but enough is stored as currency, subtrack currency
     * If not enough food and not enough stored, lose population, remove all currency
     * */
    public int[] calculateByFood(float population, float foodSet, float foodStored)
    {
        int[] number = new int[4] { 0, 0,0,0 }; //0 Addition population, 1 AddFood, 2 Minimum 3 percentage 
        float divider = population / FoodDivider;
        float minimum = population * divider;
        number[2] = (int)minimum;
        number[3] = (int)(divider * 100);
        float supporting =0;
        if (minimum <= foodSet)
        { 
            //enough food set
            //population gain
            supporting = Mathf.Sqrt(foodSet * FoodDivider);
            number[0] = (int)(supporting - population);
            //food currency gain
            number[1] = (int)(foodSet - minimum);  
        }
        else
        {
            //Not enough enough set, enough currency
            //currency lost
            if ((foodSet + foodStored) > minimum)
                number[1] = (int)(foodSet-minimum);
            else
                //not enough set, not enough currency
                //currency and population lost
                supporting = Mathf.Sqrt((foodSet + foodStored) * FoodDivider);
                number[0] = (int)(supporting - population);
                number[1] = -(int)foodStored;
        }

        return number;
    }

    //REST
    public int[] calculateByRest(GameObject player)
    {
        //presets
        int[] result = new int[5] { 0, 0, 0, 0,0 };
        script_Player scPlayer = player.GetComponent<script_Player>();
        int set = scPlayer.Stats[7];
        int stock = scPlayer.Stats[3];
        int ownPop = scPlayer.Stats[0];
        int totalRest = stock + set;
        int totPopAll = GetTotalStats(0);
        int totStockAll = GetTotalStats(3);
        int totSetAll = GetTotalStats(7);
        int totRestAllPlayersMax = totPopAll + totStockAll;
        float imigrationFactor = 0.5f;
        int totalImigrants = (int)(totPopAll * imigrationFactor);
        int ownImigrants = (int)(ownPop*imigrationFactor);
        float stockFactor = 0.08f;
        float playerRestFractionAtLeast = totalRest / ((float)totRestAllPlayersMax - (float)ownPop + (float)set);
        float playerRestFractionAtMost = (totStockAll+set > 0) ? totalRest / ((float)totStockAll + (float)set) : 1;
        float playerRestFraction = (totSetAll + totStockAll) > 0 ? totalRest / ((float)totSetAll + (float)totStockAll) : 1;
        //values
        result[0] = stock + totalRest; //total rest power
        result[1] = (int)(totalImigrants*playerRestFractionAtMost ); //gain at most - if everyone set 0
        result[2] = (int)(ownImigrants - playerRestFractionAtLeast*ownImigrants); //lose at most - if everyone set max
        result[3] = (int)(set * stockFactor); //add stock
        result[4] = (int)(totalImigrants*playerRestFraction) - ownImigrants; //population gain by fraction.
        return result;
    }

    //DANCE
    public string[] calculateByDance(GameObject player)
    {
        string[] result = new string[7] { "", "", "", "", "", "", "" }; //0 Combined Dance 1 Player Total Dance 2 Most points name 3 most points points 4 Least points Name 5 Least points points 6 Dance Gain
        script_Player scPlayer = player.GetComponent<script_Player>();
        int CombinedDance = GlobalValues[2];
        int totalDance = scPlayer.Stats[4] + scPlayer.Stats[8];
        result[0] = CombinedDance.ToString();
        result[1] = totalDance.ToString();
        //MostPoints
        List<GameObject> ActivePlayers = GetActivePlayers();
        int int_LastValue = -1;
        int int_Loop = 0;
        string name_Loop = "";
        foreach (GameObject go in ActivePlayers)
        {
            int_Loop = go.GetComponent<script_Player>().Stats[4];
            if (int_Loop > int_LastValue)
            {
                name_Loop = go.GetComponent<script_Player>().Name;
                int_LastValue = int_Loop;
            }
        }
        result[2] = name_Loop;
        result[3] = int_LastValue.ToString();
        //LeastPoints, remembers LastValue as reference from MostPoints
        int_Loop = int_LastValue;
        name_Loop = "";
        foreach (GameObject _go in ActivePlayers)
        {
            int_Loop = _go.GetComponent<script_Player>().Stats[4];
            if (int_Loop <= int_LastValue)
            {
                name_Loop = _go.GetComponent<script_Player>().Name;
                int_LastValue = int_Loop;
            }
        }
        result[4] = name_Loop;
        result[5] = int_LastValue.ToString();
        //Dance Gain
        result[6] = scPlayer.Stats[8].ToString();
        return result;
    }

    public GameObject GetCurrentPlayer()
    {
        GameObject[] PLAYERS = objectCollector.GetComponent<script_objectCollector>().PLAYERS;
        foreach (GameObject go in PLAYERS)
        {
            if (go.GetComponent<script_Player>().b_Me)
            {
                return go;
            }
        }
        Debug.Log("Warning: no ME object to return");
        return gameObject;
    }

    public int GetTotalStats(int index)
    {
        int total = 0;
        script_Player script;
        GameObject[] Players = objectCollector.GetComponent<script_objectCollector>().PLAYERS;
        foreach (GameObject go in Players)
        {
            script = go.GetComponent<script_Player>();
            if ((script.b_Active && !script.b_Eliminated) || index == 4)
            {
                total += script.Stats[index];
            }
        }
        return total;
    }

    public void HideTooltipsText(GameObject[] tooltips)
    {
        foreach (GameObject go in tooltips)
        {
            go.SetActive(false);
        }
    }

    public List<GameObject> GetActivePlayers()
    {
        List<GameObject> ActivePlayers = new List<GameObject>();
        GameObject[] Players = objectCollector.GetComponent<script_objectCollector>().PLAYERS;
        foreach (GameObject go in Players)
        {
            if (go.GetComponent<script_Player>().b_Active && !go.GetComponent<script_Player>().b_Eliminated)
            {
                ActivePlayers.Add(go);
            }
        }
        return ActivePlayers;
    }
}
