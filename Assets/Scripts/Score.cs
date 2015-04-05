using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
    public float time;
    private Text timeText;

    void Start()
    {
        timeText = this.GetComponent<Text>();
    }

	void Update () 
    {

		time = Time.timeSinceLevelLoad;    
        timeText.text = "Score: " + time.ToString("#.##");  
	}
}
