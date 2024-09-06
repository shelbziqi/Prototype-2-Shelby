using UnityEngine;

public class ShakeControl : MonoBehaviour
{
    public float shakeLimitX = 1.0f;  // Maximum horizontal shake limit
    public float shakeLimitY = 1.0f;  // Maximum vertical shake limit
    public float shakeSpeed = 5.0f;   // Speed factor for the shake movement
    public float returnSpeed = 3.0f;  // Speed at which the object returns to the original position

    private Vector3 initialPosition;
    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Detect if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition; // Record initial mouse position
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            ShakeObjectWithMouse();
        }
        else
        {
            ReturnToInitialPosition();
        }
    }

    void ShakeObjectWithMouse()
    {
        var rb = transform.GetComponent<Rigidbody>();
        // Calculate mouse movement delta (difference in mouse position)
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = (currentMousePosition - lastMousePosition) * shakeSpeed * Time.deltaTime;

        // Update the last mouse position for the next frame
        lastMousePosition = currentMousePosition;

        // Calculate the new position of the object
        Vector3 newPosition = transform.localPosition + new Vector3(mouseDelta.x, mouseDelta.y, 0);

        // Clamp the movement within shake limits
        newPosition.x = Mathf.Clamp(newPosition.x, initialPosition.x - shakeLimitX, initialPosition.x + shakeLimitX);
        newPosition.y = Mathf.Clamp(newPosition.y, initialPosition.y - shakeLimitY, initialPosition.y + shakeLimitY);

        // Apply the new position to the object
        //transform.localPosition = newPosition;

        rb.MovePosition(newPosition);
    }

    void ReturnToInitialPosition()
    {
        // Smoothly return to the initial position when the mouse is released
        //transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * returnSpeed);
        var rb = transform.GetComponent<Rigidbody>();
        rb.MovePosition(Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * returnSpeed));
    }
}