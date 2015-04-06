using UnityEngine;

public class Boid : MonoBehaviour
{
    public Flock flock;
    Vector3 vel;
    Vector3 acc;

    float r;
    float maxforce = .01f;    // Maximum steering force
    float maxspeed = .1f;    // Maximum speed

    void Update()
    {
        // Update velocity
        vel += acc;

        // Limit speed
        vel = Limit(vel, maxspeed);
        
        var pos = transform.position;
        pos += vel;
        pos.z = -9;
        transform.position = pos;
        
        // Reset accelertion to 0 each cycle
        acc = Vector3.zero;
    }

    public void Flock()
    {
        // Arbitrarily weight these forces
        var separation = Separate();
        var alignment = Vector3.zero;// Align();
        var cohesion = Cohesion() * 1.05f;
        var follow = Follow() * 1.5f;

        // Add the force vectors to acceleration
        acc += separation + alignment + cohesion + follow;
    }

    // A method that calculates a steering vector towards a target
    // Takes a second argument, if true, it slows down as it approaches the target
    Vector3 Steer(Vector3 target, bool slowdown)
    {
        Vector3 steer = Vector3.zero;  // The steering vector
        var desired = target - transform.position;  // A vector pointing from the location to the target
        float d = desired.sqrMagnitude; // Distance from the target is the magnitude of the vector
        // If the distance is greater than 0, calc steering (otherwise return zero vector)
        if (d > 0)
        {
            // Normalize desired
            desired.Normalize();
            desired *= maxspeed;

            // Two options for desired vector magnitude (1 -- based on distance, 2 -- maxspeed)
            if ((slowdown) && (d < 100.0f))
            {
                desired *=  d / 100.0f; // This damping is somewhat arbitrary
            }

            // Steering = Desired minus Velocity
            steer = desired - vel;

            // Limit to maximum steering force
            steer = Limit(steer, maxforce);
        }
        
        return steer;
    }

    Vector3 Separate()
    {
        var desiredseparation = 25.0f;
        var steer = Vector3.zero;
        var count = 0;

        // For every boid in the system, check if it's too close
        foreach (var other in flock.boids)
        {
            // Calculate vector pointing away from neighbor
            var diff = transform.position - other.transform.position;

            var d = diff.magnitude;

            // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself)
            if ((d > 0) && (d < desiredseparation))
            {
                steer += diff.normalized / d;
                count++;            // Keep track of how many
            }
        }

        // Average -- divide by how many
        if (count > 0)
        {
            steer /= count;
        }

        // As long as the vector is greater than 0
        if (steer.sqrMagnitude > 0)
        {
            // Implement Reynolds: Steering = Desired - Velocity
            steer = (steer.normalized * maxspeed) - vel;

            //TODO clamp by maxforce
            steer = Limit(steer, maxforce);
        }

        return steer;
    }

    // Alignment
    // For every nearby boid in the system, calculate the average velocity
    Vector3 Align()
    {
        var neighbordist = 50.0f;
        var steer = Vector3.zero;

        int count = 0;

        foreach (var other in flock.boids)
        {
            var d = (other.transform.position - transform.position).magnitude;
            if ((d > 0) && (d < neighbordist))
            {
                steer += other.vel;
                count++;
            }
        }

        // Average -- divide by how many
        if (count > 0)
        {
            steer /= count;
        }

        // As long as the vector is greater than 0
        if (steer.sqrMagnitude > 0)
        {
            // Implement Reynolds: Steering = Desired - Velocity
            steer = (steer.normalized * maxspeed) - vel;

            //TODO clamp by maxforce
            steer = Limit(steer, maxforce);
        }

        return steer;
    }

    // Cohesion
    // For the average location (i.e. center) of all nearby boids, calculate steering vector towards that location
    Vector3 Cohesion()
    {
        var neighbordist = 50.0f;
        var sum = Vector3.zero;   // Start with empty vector to accumulate all locations

        int count = 0;
        //sum += flock.transform.position;

        foreach (var other in flock.boids)
        {
            var d = (other.transform.position - transform.position).magnitude;
            if ((d > 0) && (d < neighbordist))
            {
                sum += other.transform.position; // Add location
                count++;
            }
        }

        if (count > 0)
        {
            sum /= count;
            return Steer(sum, false);  // Steer towards the location
        }

        return Vector3.zero;
    }


    // Cohesion
    // For the average location (i.e. center) of all nearby boids, calculate steering vector towards that location
    Vector3 Follow()
    {
        var steer = Steer(flock.transform.position, false);

        return steer; // Steer towards the location
    }


    Vector3 Limit(Vector3 vec, float limit)
    {
        vec.x = Mathf.Clamp(vec.x, -limit, limit);
        vec.y = Mathf.Clamp(vec.y, -limit, limit);
        vec.z = Mathf.Clamp(vec.z, -limit, limit);

        return vec;
    }
}
