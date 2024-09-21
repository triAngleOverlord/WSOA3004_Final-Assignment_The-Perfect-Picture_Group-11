using UnityEngine;

public class CameraController : MonoBehaviour
{
    internal CameraState state;
    public enum CameraState
    {
        onPlayer, panning
    }

    private Vector3 dragOrigin;

    private Transform player;
    [SerializeField] private float lerpTime;
    [SerializeField] private bool hasSetInitialPos;
    [SerializeField] private bool camOnInitialPos;

    [SerializeField] private float tiltAmount;
    [SerializeField] private float tiltSpeed;

    private float tiltInput;

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
                hasSetInitialPos = true;

                if (hasSetInitialPos && !camOnInitialPos)
                {
                    Vector3 onLeftShiftDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = onLeftShiftDownPos;
                    camOnInitialPos = true;

                    dragOrigin = Camera.main.ScreenToWorldPoint(-Input.mousePosition);
                }

            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 diff = dragOrigin - Camera.main.ScreenToWorldPoint(-Input.mousePosition);
                transform.position += diff;
            }
        }
        else
        {
            Vector3 playerPos = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerPos, Time.smoothDeltaTime * lerpTime);

            hasSetInitialPos = false;
            camOnInitialPos = false;
        }
    }
}
