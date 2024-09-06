using UnityEngine;

public class FastMovingObjectCollision : MonoBehaviour
{
    public float rayDistance = 2.0f;  // Distance to check for further collisions after the initial impact
    public LayerMask collisionLayers;  // Layer mask to specify which layers to check for collisions
    private RaycastHit[] hitResults = new RaycastHit[10];  // Array to store raycast hits (adjust size as needed)

    void OnCollisionEnter(Collision collision)
    {
        // When the object collides with something, cast a ray to check further collisions
        CheckRaycastCollision(collision.contacts[0].point, collision.contacts[0].normal);
    }

    void CheckRaycastCollision(Vector3 contactPoint, Vector3 collisionNormal)
    {
        // Cast a ray in the direction opposite to the collision normal (i.e., check "in front" of the object)
        int hitCount = Physics.RaycastNonAlloc(contactPoint, -collisionNormal, hitResults, rayDistance, collisionLayers);

        if (hitCount > 0)
        {
            // Handle the first hit object (you can iterate through all hits if needed)
            for (int i = 0; i < hitCount; i++)
            {
                RaycastHit hit = hitResults[i];
                Debug.Log("Further hit detected: " + hit.collider.name);

                // Handle the hit (e.g., log it, apply damage, trigger effects, etc.)
                HandleRaycastHit(hit);
            }
        }
    }

    void HandleRaycastHit(RaycastHit hit)
    {
        // Custom logic for handling the raycast hit (e.g., stopping movement, damage, etc.)
        Debug.Log("Hit detected at point: " + hit.point + " on object: " + hit.collider.name);
    }
}