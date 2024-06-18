using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public bool RotateAroundObject = true;
    public float RotationSpeed = 5.0f;
    private Vector3 cameraOffset;
    public Transform PlanetTransform;
    public bool LookAtPlayer = false;

    [Range(0.1f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public float ScrollSpeed = 10f;

    private bool isMousePressed = false;

    public float DesiredDistance = 10.0f;

    public float MinDistance = 2.0f; 
    public float MaxDistance = 20.0f; 

    public void Start()
    {
        cameraOffset = (transform.position - PlanetTransform.position).normalized * DesiredDistance;
    }

    public void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        if (RotateAroundObject && isMousePressed)
        {
            float horizontalInput = Input.GetAxis("Mouse X") * RotationSpeed;
            float verticalInput = Input.GetAxis("Mouse Y") * RotationSpeed;

            Quaternion horizontalRotation = Quaternion.AngleAxis(horizontalInput, Vector3.up);
            cameraOffset = horizontalRotation * cameraOffset;

            Quaternion verticalRotation = Quaternion.AngleAxis(-verticalInput, transform.right);
            cameraOffset = verticalRotation * cameraOffset;

            cameraOffset = cameraOffset.normalized * DesiredDistance;
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            DesiredDistance -= scrollInput * ScrollSpeed;
            DesiredDistance = Mathf.Clamp(DesiredDistance, MinDistance, MaxDistance);
            cameraOffset = cameraOffset.normalized * DesiredDistance;
        }

        Vector3 newPos = PlanetTransform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundObject)
        {
            transform.LookAt(PlanetTransform);
        }


    }
}
