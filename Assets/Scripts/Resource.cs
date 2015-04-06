using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

    private float health = 1;
    bool wasHit = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);

        }

        transform.localScale = Vector3.one * health;
	}

    void Hit()
    {
        if (!wasHit)
        {
            wasHit = true;
            audio.Play();
        }
        health -= 0.02f;
    }
}
