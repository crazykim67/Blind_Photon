using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum State
{
    Walk,
    Run,
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PhotonView pv;
    public CharacterController ch;
    public HeadBobController hc;

    public float speed = 1f;
    public float gravity = -9.8f; 
    //public float jumpHeight = 1f;

    private Vector3 velocity;

    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    private float moveX, moveZ;

    public State currentState = State.Walk;

    //[Header("Input Action")]
    //private Controls input = null;

    private void Start()
    {
        ch = GetComponent<CharacterController>();
        pv = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!pv.IsMine)
            return;

        if (InGameMenu.Instance.IsMenu)
        {
            moveDir = Vector3.zero;
            moveX = moveZ = 0;
            return;
        }

        InputKey();
        Move();
        InputSprint();
        UpdateState();
    }

    //private void OnMove(InputAction.CallbackContext value)
    //{
    //    moveX = value.ReadValue<Vector2>().x;
    //    moveZ = value.ReadValue<Vector2>().y;
    //}

    //private void OnCancel(InputAction.CallbackContext value)
    //{
    //    moveX = 0;
    //    moveZ = 0;
    //}

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
        if (ch.isGrounded)
            velocity.y = 0f;

        // 점프
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

        hc.SetValue(currentState);
    }
}
