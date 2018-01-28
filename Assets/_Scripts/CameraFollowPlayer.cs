using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    private Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        target = PlayerController.instance.transform;    
    }


    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}