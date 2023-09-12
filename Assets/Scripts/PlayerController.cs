using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 50f)]
    [SerializeField] float Speed;
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] CharacterController cc;
    [SerializeField] Transform cam;
    float turnSmoothVelocity;
    float smooth_Time = 0.1f;
    private Vector3 direction;
    public bool canMove;

    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;
    private void Awake()
    {

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    private void Start()
    {

        canMove = true;
        cc = GetComponent<CharacterController>();
        basicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        basicMultiChannelPerlin.m_AmplitudeGain = .5f;
        basicMultiChannelPerlin.m_FrequencyGain = .5f;

    }

    private void Update()
    {
        if (canMove)
        {

            float horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");

            direction = new Vector3(horizontal, 0, Vertical);
        }




    }

    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smooth_Time);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Vector3 move = transform.right * horizontal + transform.forward * Vertical;
            cc.Move(moveDir.normalized * Speed * Time.deltaTime);

            basicMultiChannelPerlin.m_AmplitudeGain = 1;
            basicMultiChannelPerlin.m_FrequencyGain = 1;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Speed = RunSpeed;

                basicMultiChannelPerlin.m_AmplitudeGain = 2;
                basicMultiChannelPerlin.m_FrequencyGain = 2;
            }
            else
            {
                Speed = WalkSpeed;
            }
        }
        else
        {
            basicMultiChannelPerlin.m_AmplitudeGain = .5f;
            basicMultiChannelPerlin.m_FrequencyGain = .5f;
        }

    }
}
