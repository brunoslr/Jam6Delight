using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
    public float time;
    private Text timeText;
    private Transform boyz;
    private bool isGameOver;
    private UnityEngine.UI.Text text;

    void Start()
    {

        text = GameObject.Find("Canvas/GameOver").GetComponent<UnityEngine.UI.Text>();
        text.gameObject.SetActive(false);
        timeText = this.GetComponent<Text>();
        boyz = GameObject.Find("Boyz").transform;
    }

	void Update () 
    {
        if (isGameOver) { return;  }

		time = Time.timeSinceLevelLoad;    
        timeText.text = "Score: " + time.ToString("0.00");

        CheckGameOver();
	}

    void CheckGameOver()
    {
        if (boyz.childCount == 0)
        {
            isGameOver = true;
            text.gameObject.SetActive(true);
            text.text = string.Format("Game Over\nScore: {0:0.00}", time);
        }

    }
}
