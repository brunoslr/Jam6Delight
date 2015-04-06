using UnityEngine;
using System.Collections.Generic;

public class Flock : MonoBehaviour {

    public GameObject boidPrefab;
    public IList<Boid> boids;

	// Use this for initialization
	void Awake () {
        boids = new List<Boid>();
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var boid in boids)
        {
            boid.Flock();
        }
	}
    public void SpawnBoid()
    {
        var boidGameObject = (GameObject)Instantiate(boidPrefab, transform.position, Quaternion.identity);

        var boid = boidGameObject.GetComponent<Boid>();
        boid.flock = this;
        boids.Add(boid);
    }

    public void DestroyBoid()
    {
        if (boids.Count == 0)
        {
            return;
        }

        var index = Random.Range(0, boids.Count);
        var boid = boids[index];

        boids.RemoveAt(index);
        Destroy(boid.gameObject);
    }
}
