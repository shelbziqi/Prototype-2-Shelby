using UnityEngine;

public class FollowBox : MonoBehaviour
{
    public Transform targetObject; 
    private Vector3 initialpositionOffset;
    private Quaternion initialrotationOffset;

    private void Start()
    {
        initialpositionOffset = transform.position - targetObject.position;
        initialrotationOffset = Quaternion.Inverse(targetObject.rotation) * transform.rotation;
    }

    void Update()
    {
        if (targetObject != null)
        {

            transform.position = targetObject.TransformPoint(initialpositionOffset);
            transform.rotation = targetObject.rotation * initialrotationOffset;

        }
    }
}