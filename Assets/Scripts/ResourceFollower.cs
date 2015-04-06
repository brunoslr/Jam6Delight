using UnityEngine;
using System.Linq;

public class ResourceFollower : MonoBehaviour {

    Transform resourceContainer;
    Transform boyzContainer;
    Transform target;
    AudioManager audioManager;

    Flock flock;

    float hitTime = 0;
    float health = 1;

    void Awake()
    {
        resourceContainer = GameObject.Find("Resources").transform;
        boyzContainer = GameObject.Find("Boyz").transform;
        audioManager = GameObject.Find("AudioManagerPrefab").GetComponent<AudioManager>();

        flock = GetComponent<Flock>();
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

	void Update () {
        if (health <= 0)
        {
            flock.Dispose();
            Destroy(gameObject);
            audioManager.setLevel(audioManager.displayedLevel + 1);
            GameObject.Find("Spawner").GetComponent<ResourceSpawner>().nextCooldown += 0.75f;
            return;
        }
        
        
        UpdateTarget();
        
        if (target != null)
        {
            MoveTowardsTarget();
            CheckCollision();
        }

        health -= 0.008f;
        //transform.localScale = Vector3.one * health;

        var count = flock.boids.Count;
        var desired = health * 5;

        if (count > desired + 1)
        {
            flock.DestroyBoid();
        }

        if (count < desired)
        {
          flock.SpawnBoid();
        }

        if (hitTime > 0)
        {
            hitTime -= Time.deltaTime;

            if (hitTime <= 0)
            {
                UpdateColor(Color.white, transform);
                flock.UpdateColor(Color.white);
                hitTime = 0;
            }
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
            health += 0.02f;

        }

        other.SendMessage("Hit");
    }

    void Hit()
    {
        health -= 0.02f;
        hitTime = 0.4f;

        UpdateColor(Color.red, transform);
        flock.UpdateColor(Color.red);
    }
}
