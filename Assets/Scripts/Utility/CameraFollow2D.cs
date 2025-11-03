using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Dead Zone Settings")]
    public float deadZoneWidth = 2f;
    public float deadZoneHeight = 1.5f;

    [Header("Smooth Follow")]
    public float smoothTime = 0.25f; // Lower = faster, Higher = smoother

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPos;

    private void Update()
    {
        target = GameObject.Find("Body_Light_Shark_Purple").transform;
    }
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 camPos = transform.position;
        targetPos = camPos; // start with current position

        // Compute distance from target
        float dx = target.position.x - camPos.x;
        float dy = target.position.y - camPos.y;

        // If outside horizontal dead zone → move targetPos.x toward target.x
        if (Mathf.Abs(dx) > deadZoneWidth / 2f)
        {
            float directionX = Mathf.Sign(dx);
            targetPos.x = target.position.x - directionX * (deadZoneWidth / 2f);
        }

        // If outside vertical dead zone → move targetPos.y toward target.y
        if (Mathf.Abs(dy) > deadZoneHeight / 2f)
        {
            float directionY = Mathf.Sign(dy);
            targetPos.y = target.position.y - directionY * (deadZoneHeight / 2f);
        }

        // Smoothly move the camera to the target position
        Vector3 smoothPos = Vector3.SmoothDamp(camPos, targetPos, ref velocity, smoothTime);

        // Keep Z fixed (for 2D)
        smoothPos.z = camPos.z;

        transform.position = smoothPos;
        
    }

#if UNITY_EDITOR
    // Visualize the dead zone
    void OnDrawGizmos()
    {
        if (target == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(deadZoneWidth, deadZoneHeight, 0));
    }
#endif
}
