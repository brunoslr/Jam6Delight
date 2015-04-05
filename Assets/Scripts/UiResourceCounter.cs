using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UiResourceCounter : MonoBehaviour {

	public Transform resourceContainer;
	private Text scoreText;

	void Start()
	{
		scoreText = this.GetComponent<Text>();
	}

	void Awake()
	{
		resourceContainer = GameObject.Find("Resources").transform;
	}
		
	void Update () 
	{
		
   
		scoreText.text = "Resources Available: " + resourceContainer.Cast<Transform>().Count();  
	}
}
