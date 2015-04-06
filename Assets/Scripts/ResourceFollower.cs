using UnityEngine;
using System.Linq;

public class ResourceFollower : MonoBehaviour {

    Transform resourceContainer;
    Transform boyzContainer;
    Transform target;
    AudioManager audioManager;

    Flock flock;

    float health = 1;

    void Awake()
    {
        resourceContainer = GameObject.Find("Resources").transform;
        boyzContainer = GameObject.Find("Boyz").transform;
        audioManager = GameObject.Find("AudioManagerPrefab").GetComponent<AudioManager>();

        flock = GetComponent<Flock>();
    }

	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
            audioManager.setLevel(audioManager.displayedLevel + 1);
            return;
        }
        
        
        UpdateTarget();
        
        if (target != null)
        {
            MoveTowardsTarget();
            CheckCollision();
        }

        health -= 0.005f;
        transform.localScale = Vector3.one * health;

        var count = flock.boids.Count;
        var desired = health * 5;
        Debug.Log(desired);


        if (count > desired)
        {
            flock.DestroyBoid();
        }
	}

    void UpdateTarget()
    {
        target = resourceContainer.Cast<Transform>().OrderBy((t) => (t.position - transform.position).sqrMagnitude).FirstOrDefault();

        if (target == null)
        {
            target = boyzContainer.Cast<Transform>().OrderBy((t) => {
                    if (t == transform)
                        return float.MaxValue;
                    return (t.position - transform.position).sqrMagnitude;
                }).FirstOrDefault();
            }
    }

    void MoveTowardsTarget()
    {
        var dir = (target.position - transform.position).normalized;
        transform.position = transform.position + (dir * 0.05f);
    }

    void CheckCollision()
    {
        var colliders = Physics.OverlapSphere(transform.position, .2f);

        if (colliders.Length > 1) {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform != transform)
                {
                    UpdateHealth(colliders[i].transform);
                }
            }
        }
    }

    void UpdateHealth(Transform other) {
        if (other.tag == "Resource")
        {
            health += 0.1f;

            var count = flock.boids.Count;
            var desired = health * 5;

            if (count < desired)
            {
                flock.SpawnBoid();
            }
        }

        other.SendMessage("Hit");
    }

    void Hit()
    {
        health -= 0.1f;

    }
}
