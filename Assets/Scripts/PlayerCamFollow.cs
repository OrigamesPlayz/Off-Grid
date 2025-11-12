using UnityEngine;

public class PlayerCamFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    [Header("Rotation Settings")]
    public float rotationSmoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        Vector3 direction = target.position - transform.position;

        Quaternion desiredRotation = Quaternion.Euler(0, 0, target.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
