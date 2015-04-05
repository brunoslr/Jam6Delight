using UnityEngine;
using System.Collections;
 
public class ResourceSpawner : MonoBehaviour {

    public GameObject resourcePrefab;
    private Transform resourceContainer;
	public Sprite[] womanSprites;

	void Start () {
        resourceContainer = GameObject.Find("Resources").transform;

	}


	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var obj = (GameObject)Instantiate(resourcePrefab, pos, Quaternion.identity);
            obj.transform.parent = resourceContainer;
	//		obj.AddComponent<SpriteRenderer>();
			obj.GetComponentInChildren<SpriteRenderer>().sprite = womanSprites[Random.Range(0,womanSprites.Length)];
        }	
	}
}
