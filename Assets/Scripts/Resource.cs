using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

    private float health = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        transform.localScale = Vector3.one * health;
	}

    void Hit()
    {
        health -= 0.1f;
    }
}
