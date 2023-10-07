using UnityEngine;
public class CameraRotation : MonoBehaviour
{
    [SerializeField] Transform target;
    [Range(0.1f, 5f)]
    [Tooltip("How sensitive the mouse drag to camera rotation")]
    [SerializeField] float mouseRotateSpeed = 0.8f;
    [SerializeField] float slerpValue = 0.25f; //Smaller positive value means smoother rotation, 1 means no smooth apply


    private Camera mainCamera;
    private Quaternion cameraRot;
    private float distanceBetweenCameraAndTarget;

    private float minXRotAngle = -80; 
    private float maxXRotAngle = 80; 

    private float rotX; 
    private float rotY; 
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }


    }
    void Start()
    {
        distanceBetweenCameraAndTarget = Vector3.Distance(mainCamera.transform.position, target.position);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed;
            rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
        }

        if (rotX < minXRotAngle)
        {
            rotX = minXRotAngle;
        }
        else if (rotX > maxXRotAngle)
        {
            rotX = maxXRotAngle;
        }
    }

    private void LateUpdate()
    {

        Vector3 dir = new Vector3(0, 0, -distanceBetweenCameraAndTarget); 

        Quaternion newQ = Quaternion.Euler(rotX, rotY, 0); 
        cameraRot = Quaternion.Slerp(cameraRot, newQ, slerpValue); 
        mainCamera.transform.position = target.position + cameraRot * dir;
        mainCamera.transform.LookAt(target.position);

    }

    public void SetCamPos()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        mainCamera.transform.position = new Vector3(0, 0, -distanceBetweenCameraAndTarget);
    }

}