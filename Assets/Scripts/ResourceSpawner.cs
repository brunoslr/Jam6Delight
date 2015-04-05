using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour {

    public GameObject resourcePrefab;
    private Transform resourceContainer;

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
        }	
	}
}
