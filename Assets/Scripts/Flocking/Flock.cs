using UnityEngine;
using System.Collections.Generic;

public class Flock : MonoBehaviour {

    public GameObject boidPrefab;
    public IList<Boid> boids;
    private Color color = Color.white;

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
        var pos = transform.position;
        pos += new Vector3(Random.value, Random.value, 0);

        var boidGameObject = (GameObject)Instantiate(boidPrefab, pos, Quaternion.identity);

        var boid = boidGameObject.GetComponent<Boid>();
        UpdateColor(color, boid.transform);

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

    public void UpdateColor(Color color)
    {
        this.color = color;
        foreach (var boid in boids)
        {
            UpdateColor(color, boid.transform);
        }
    }

    void UpdateColor(Color color, Transform target)
    {
        var spriteRenderer = target.GetComponent<SpriteRenderer>();

        if (spriteRenderer)
        {
            spriteRenderer.color = color;
        }

        foreach (Transform child in target)
        {
            UpdateColor(color, child);
        }
    }

    public void Dispose()
    {
        foreach (var boid in boids)
        {
            Destroy(boid.gameObject);
        }

        Destroy(gameObject);
    }
}
