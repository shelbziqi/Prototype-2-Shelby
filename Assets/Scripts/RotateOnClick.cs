using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
    public float rotationAngle = 90f;  // Angle to rotate (90 degrees per click)
    private float currentRotationZ = 0f; // Current rotation angle around Z-axis
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // Check for mouse clicks
        if (Input.GetKeyDown(KeyCode.A)) // Left mouse button
        {
            // Rotate the object 90 degrees clockwise
            RotateObject(rotationAngle);
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Right mouse button
        {
            // Rotate the object 90 degrees counterclockwise
            RotateObject(-rotationAngle);
        }
    }

    private void RotateObject(float angle)
    {
        // Calculate the new rotation angle
        float newRotationZ = currentRotationZ + angle;

        // Create the target rotation based on the new angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, newRotationZ);

        // Smoothly rotate the object to the target rotation
        StartCoroutine(RotateOverTime(targetRotation, 0.5f)); // Adjust duration as needed

        // Update the current rotation angle
        currentRotationZ = newRotationZ;
    }

    private System.Collections.IEnumerator RotateOverTime(Quaternion targetRotation, float duration)
    {
        Quaternion startRotation = transform.rotation;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            //transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / duration);
            rb.MoveRotation(Quaternion.Slerp(startRotation, targetRotation, timeElapsed / duration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is set
        //transform.rotation = targetRotation;
        rb.MoveRotation(targetRotation);
    }
}