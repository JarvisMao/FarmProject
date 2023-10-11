using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Quaternion initialRotation;


    private Rigidbody2D rb;

    public float speed;
    private float inputX;
    private float inputY;


    private Vector2 movementInput;

    private Animator[] animators;
    private bool isMoving;
    private bool inputDisabled;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition += OnMoveToPosition;
    }
    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition -= OnMoveToPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!inputDisabled)
        {
            PlayerInput();
        }
        else
        {
            isMoving = false;
        }
        SwicthAnimation();
    }

    private void FixedUpdate()
    {
        if (!inputDisabled)
        {
            Movement();
        }

    }

    private void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 || inputY != 0)
        {
            inputX = inputX * 0.55f;
            inputY = inputY * 0.55f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = inputX * 2;
            inputY = inputY * 2;
        }
        movementInput = new Vector2(inputX, inputY);

        isMoving = movementInput != Vector2.zero;
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movementInput * (speed * Time.deltaTime));
    }

    private void SwicthAnimation()
    {
        foreach (var anim in animators)
        {
            anim.SetBool("IsMoving", isMoving);
            if (isMoving)
            {
                anim.SetFloat("InputX", inputX);
                anim.SetFloat("InputY", inputY);
            }
        }
    }

    private void OnBeforeSceneUnloadEvent()
    {
        inputDisabled = true;
    }

    private void OnAfterSceneLoadedEvent()
    {
        inputDisabled = false;
    }

    private void OnMoveToPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
}
