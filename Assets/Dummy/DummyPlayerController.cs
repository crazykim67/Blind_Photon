using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DummyPlayerController : MonoBehaviour
{
    public CharacterController ch;
    public DummyHeadBobController dhc;

    public float speed = 5f;
    public float gravity = -9.8f;
    //public float jumpHeight = 1f;

    private Vector3 velocity;

    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    [SerializeField]
    private float moveX, moveZ;

    public State currentState = State.Walk;

    //[Header("Input Action")]
    //private Controls input = null;

    private void Start()
    {
        ch = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        InputKey();
        Move();
        InputSprint();
        UpdateState();
    }

    private void InputKey()
    {
        if (Input.GetKey(OptionManager.Instance.keyManager.FORWARD.currentKey))
            moveZ = 1;
        else if (Input.GetKey(OptionManager.Instance.keyManager.BACK.currentKey))
            moveZ = -1;

        if (Input.GetKey(OptionManager.Instance.keyManager.LEFT.currentKey))
            moveX = -1;
        else if (Input.GetKey(OptionManager.Instance.keyManager.RIGHT.currentKey))
            moveX = 1;

        if (Input.GetKeyUp(OptionManager.Instance.keyManager.FORWARD.currentKey))
            moveZ = 0;
        else if (Input.GetKeyUp(OptionManager.Instance.keyManager.BACK.currentKey))
            moveZ = 0;

        if (Input.GetKeyUp(OptionManager.Instance.keyManager.LEFT.currentKey))
            moveX = 0;
        else if (Input.GetKeyUp(OptionManager.Instance.keyManager.RIGHT.currentKey))
            moveX = 0;
    }

    public void Move()
    {
        //moveX = Input.GetAxis("Horizontal");
        //moveZ = Input.GetAxis("Vertical");

        if (ch.isGrounded)
            velocity.y = 0f;

        // มกวม
        //if(Input.GetKeyDown(KeyCode.Space) && ch.isGrounded)
        //    velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        moveDir = transform.right * moveX + transform.forward * moveZ;

        ch.Move(moveDir.normalized * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        ch.Move(velocity * Time.deltaTime);
    }

    public void InputSprint()
    {
        if (Input.GetKeyDown(OptionManager.Instance.keyManager.SPRINT.currentKey))
            currentState = State.Run;
        else if (Input.GetKeyUp(OptionManager.Instance.keyManager.SPRINT.currentKey))
            currentState = State.Walk;
    }

    public void UpdateState()
    {
        switch (currentState)
        {
            case State.Walk:
                {
                    speed = 1f;
                    break;
                }
            case State.Run:
                {
                    speed = 3f;
                    break;
                }
        }

        dhc.SetValue(currentState);
    }
}
