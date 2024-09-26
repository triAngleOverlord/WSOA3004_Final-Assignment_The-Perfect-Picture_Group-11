using UnityEngine;

public class CameraController : MonoBehaviour
{
    internal CameraState state;
    public enum CameraState
    {
        onPlayer, panning
    }

    private Vector3 dragOrigin;
    private Vector3 onLeftShiftDownPos;
    private Transform player;
    [SerializeField] private float lerpTime;

    [SerializeField] private bool camOnInitialPos;

    [SerializeField] private float tiltAmount;
    [SerializeField] private float tiltSpeed;

    private float tiltInput;

    //cam clamp values
    [SerializeField] private float minX, maxX, minY,maxY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        GetInput();
    }

    void LateUpdate()
    {
        HandleCameraState(); 
        HandleScreenRotation();
    }

    private void GetInput()
    {
        tiltInput = Input.GetAxis("Horizontal");
    }
    private void HandleScreenRotation()
    {
        float currentAngle = transform.eulerAngles.z;

        if (currentAngle > 180f)
        {
            currentAngle -= 360;
        }

        float targetAngle = -tiltInput;
        targetAngle = Mathf.Clamp(targetAngle, -tiltAmount, tiltAmount);

        float lerpedAngle = Mathf.Lerp (currentAngle, targetAngle, Time.deltaTime * tiltSpeed);
        transform.rotation = Quaternion.Euler (0, 0, lerpedAngle);
    }

    private void HandleCameraState()
    {
        state = Input.GetKey (KeyCode.LeftShift) ? CameraState.panning : CameraState.onPlayer;
        
        if (state == CameraState.panning)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                onLeftShiftDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                camOnInitialPos = true;          
            }

            if (camOnInitialPos)
            {
                Vector3 camNewPos = onLeftShiftDownPos;
                transform.position = Vector3.Lerp(transform.position, camNewPos, Time.deltaTime * lerpTime);

                if (Vector3.Distance (transform.position, camNewPos) < 0.1f)
                {
                    camOnInitialPos = false;
                    dragOrigin = Camera.main.ScreenToWorldPoint (-Input.mousePosition);
                }
            }

            else if (Input.GetKey(KeyCode.LeftShift))
           {
                Vector3 diff = dragOrigin - Camera.main.ScreenToWorldPoint(-Input.mousePosition);
                Vector3 lerpCamPos = transform.position + diff;
                transform.position = Vector3.Lerp(transform.position, lerpCamPos, Time.deltaTime * lerpTime);
            }
        }
        else
        {
            Vector3 playerPos = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerPos, Time.smoothDeltaTime * lerpTime);
            camOnInitialPos = false;
        }
    }
}
