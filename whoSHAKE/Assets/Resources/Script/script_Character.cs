using UnityEngine;
using System.Collections;
using DG.Tweening;

public class script_Character : MonoBehaviour 
{

    public Sprite icon;
    public AudioClip[] Voices_Generic = new AudioClip[8];
    //private float Size = 0.5f;

    public void Spawn()
    {
        if (gameObject.GetComponent<DOTweenAnimation>())
        {
            DOTweenAnimation tween = gameObject.GetComponent<DOTweenAnimation>();
            tween.DORewind();
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            tween.StopAllCoroutines();
            tween.DOPlayById("Scale");
        }
        
    }

}
