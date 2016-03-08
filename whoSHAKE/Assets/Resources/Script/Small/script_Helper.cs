using UnityEngine;
using System.Collections;

public class script_Helper : MonoBehaviour {

    public void test()
    {
        script_GameManager.Instance.MainObjects[0].GetComponent<script_UI>().sound_Woosh();
    }
}
