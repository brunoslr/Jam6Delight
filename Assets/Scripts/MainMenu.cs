using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//public AudioClip start;
    public AudioSource source;

	public void StartGame()
	{

        source.Play();	
        StartCoroutine("LoadTime");

	}
	
	public void QuitGame()
	{
		Application.Quit ();
	}

    private IEnumerator LoadTime() {
        print(Time.time);
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel ("MainScene");
        print(Time.time);
    }

}
