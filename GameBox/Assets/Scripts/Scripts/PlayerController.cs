using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private float speedMove;
    [SerializeField] private float speedJump;
    [SerializeField] private int maxRoads;

    private Rigidbody rb;
    private Animator anim;

    private float speedZero = 0;
    private float speedNorm;
    private float verticalMove = 0;
    private float targetPos = 0;

    private int roads = 0;

    private bool isGrounded;
    private bool leftPos;
    private bool rightPos;
    private bool isDie;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        var res = GetComponent<DistanceController>();
        res.LoadMAxDis();
    }


    void Start()
    {
        StopSpeed();
        targetPos = 0;
        transform.position = new Vector3(targetPos, 0, 0);
        isGrounded = false;
    }

    void LateUpdate()
    {
        PlayerMove();

        if (transform.position.x >= targetPos && rightPos)
        {
            verticalMove = 0;
            rightPos = false;
            transform.position = new Vector3(targetPos, transform.position.y, transform.position.z);
        }

        if (transform.position.x <= targetPos && leftPos)
        {
            verticalMove = 0;
            leftPos = false;
            transform.position = new Vector3(targetPos, transform.position.y, transform.position.z);
        }
    }

    public void PlayerMove()
    {
        transform.Translate(verticalMove, 0, speedNorm * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            LeftMove();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RightMove();
        }
    }

    public void LeftMove()
    {
        if (roads > -((maxRoads - 1) / 2))
        {
            verticalMove = -speedNorm * Time.fixedDeltaTime * 1.1f;
            targetPos -= 2;
            leftPos = true;
            roads -= 1;
        }
    }

    public void RightMove()
    {
        if (roads < (maxRoads - 1) / 2)
        {
            verticalMove = speedNorm * Time.fixedDeltaTime * 1.1f;
            targetPos += 2;
            rightPos = true;
            roads += 1;
        }
    }

    public void Jump()
    {
        if (isGrounded && !isDie)
        {
            rb.AddForce(Vector3.up * (speedJump * 100));
            MusicController audio = GetComponent<MusicController>();
            audio.JumpMusic();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") && !isDie)
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") && !isDie)
        {
            isGrounded = false;
            anim.SetBool("isJump", true);
        }
    }

    public void GetSpeed(float speedNew)
    {
        speedNorm += speedNew;
        speedMove = speedNorm;
    }

    public void NormSpeed()
    {
        speedNorm = speedMove;

        anim.SetBool("isRun", true);
        anim.SetBool("isIdle", false);
    }

    public void StopSpeed()
    {
        speedNorm = speedZero;

        anim.SetBool("isIdle", true);
        anim.SetBool("isIdle", false);
    }

    public void StartDeath()
    {
        anim.SetBool("isDie", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isRun", false);

        isDie = true;
        isGrounded = false;
        dieEffect.SetActive(true);
    }

    public int MaxRoads()
    {
        return maxRoads;
    }

}
