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
    public bool isKnocking;
    public float knockBackLength = 0.5f;
    private float knockBackCounter;
    public Vector2 knockBackPower;
    public GameObject[] playerPieces;
    public float bounceForce = 8f;

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
        if (!isKnocking)
        {
            PlayerMove();
            PlayerJump();
        }

        if(isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockBackPower.x;
            moveDirection.y = yStore;

            if(charController .isGrounded)
            {
                moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter<=0)
            {
                isKnocking = false;
            }
        }

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

    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("KnockBack");
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }
}

