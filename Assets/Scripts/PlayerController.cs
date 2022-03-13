using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    private Vector3 moveDirection;
    public CharacterController charController;
    private Camera theCam;
    public GameObject playerModel;
    public float rotateSpeed;
    public Animator anim;
    public bool isJumping = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main;
    }

    private void Update()
    {
        PlayerJumpInput();
    }

    void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", charController.isGrounded);
    }

    private void PlayerMove()
    {
        float yStore = moveDirection.y;
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        //нормализация движения по диагонали
        moveDirection.Normalize();
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y = yStore;

        //добавление гравитации
        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        //transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed);
        charController.Move(moveDirection * Time.deltaTime);

        //особенность перемещения от 3лица
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            //playerModel.transform.rotation = newRotation;
            //плавный поворот модели
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }
    
    private void PlayerJumpInput()
    {
        if (Input.GetButtonDown("Jump") && charController.isGrounded)
        {
            isJumping = true;
            moveDirection.y = jumpForce;
        }

        if (!charController.isGrounded)
        {
            isJumping = false;
        }
    }

    private void PlayerJump()
    {
        if (charController.isGrounded && !isJumping)
        {
            moveDirection.y = 0f;
        }
    }
}

