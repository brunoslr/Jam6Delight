using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	private int level;
	private int queuedLevel;
	public List<AudioComponent> components;
	public int displayedLevel;
	public bool increaseLevel;
	// Use this for initialization
	void Start () {
		level = -1;
		queuedLevel=0;
	}
	
	// Update is called once per frame
	void Update () {
		displayedLevel = level;
		if(queuedLevel>level){
			level = queuedLevel;
			setLevels();
		}
		if(increaseLevel){
			level = level+1;
			if(level > 2){level = 2;}
			increaseLevel = false;
			setLevels();
		}
	}

	// Safe, Set level method to change the current level of the audio and cause other tracks to play at different times 
	public void setLevel(int _l){
		if(_l<0)
			_l = 0;
		if(_l > 2)
			_l = 2;
		queuedLevel = _l;
	}

	// Resets all audio tracks for restarting of the game
	public void ResetGame(){
		foreach(AudioComponent a in components){
			a.source.Stop();
			level = 0;
			setLevels();
		}
	}

	// Starts all audio tracks
	public void StartGame(){
		setLevel(0);
		foreach(AudioComponent a in components){
			a.source.Play();
		}
	}

	private void setLevels(){
		if(level==0){
			setComponentVolume(components[0],0.5f);
			setComponentVolume(components[1],0.5f);
			setComponentVolume(components[2],0.044f);
			setComponentVolume(components[3],0.0f);
		}
		else if(level==1){
			setComponentVolume(components[0],1.0f);
			setComponentVolume(components[1],0.8f);
			setComponentVolume(components[2],0.8f);
			setComponentVolume(components[3],0.044f);
		} 
		else if(level==2){
			setComponentVolume(components[0],1.0f);
			setComponentVolume(components[1],1.0f);
			setComponentVolume(components[2],0.8f);
			setComponentVolume(components[3],0.35f);
		}
	}

	// Sets the component volume for a track of the song currently being played
	private void setComponentVolume(AudioComponent a, float volume){
		a.volume = volume;
	}
}