using UnityEngine;

public class CubeCollector : MonoBehaviour
{
    public float growthAmount = 0.01f;   // Amount by which the cube grows each time it collects a ball
    public string ballTag = "Ball";     // Tag to identify balls in the scene
 
    private AudioSource audioSource;     // Reference to the AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to the cube
        audioSource = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Log collision details for debugging
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // Check if the collided object is tagged as a ball
        if (collision.gameObject.CompareTag(ballTag))
        {
            Debug.Log("Ball collected: " + collision.gameObject.name);

            // Call method to grow the cube
            GrowCube();

            PlayCollectionSound();

            // Destroy the collected ball
            Destroy(collision.gameObject);
        }
    }

    private void GrowCube()
    {
        // Increase the size of the cube
        transform.localScale += new Vector3(growthAmount, growthAmount, 0f);
        Debug.Log("Cube size increased to: " + transform.localScale);
    }

    private void PlayCollectionSound()
    {
        audioSource.Play();
    }

}