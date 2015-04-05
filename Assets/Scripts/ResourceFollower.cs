using UnityEngine;
using System.Linq;

public class ResourceFollower : MonoBehaviour {

    Transform resourceContainer;
    Transform boyzContainer;
    Transform target;

    float health = 1;

    void Awake()
    {
        resourceContainer = GameObject.Find("Resources").transform;
        boyzContainer = GameObject.Find("Boyz").transform;
    }

	void Update () {
        if (health <= 0)
        {
            Destroy(transform);
            return;
        }
        
        
        UpdateTarget();
        
        if (target != null)
        {
            MoveTowardsTarget();
            CheckCollision();
        }

        transform.localScale = Vector3.one * health;
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
        transform.position = transform.position + (dir * 0.1f);
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
        }

        other.SendMessage("Hit");
    }

    void Hit()
    {
        health -= 0.1f;
    }
}
