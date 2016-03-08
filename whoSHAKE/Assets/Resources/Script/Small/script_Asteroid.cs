using UnityEngine;
using System.Collections;

public class script_Asteroid : MonoBehaviour {

    private Vector3 vector = new Vector3(0,1,0);
    public float rate = 3f;
    public float time = 0;
	// Update is called once per frame
	void Update () 
    {
        time += Time.deltaTime;
        if (time > 3)
        {
            time = 0;
            rate = rate * (-1);
        }
            
        transform.Rotate(vector, rate*Time.deltaTime);
    }

}
