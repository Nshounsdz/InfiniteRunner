using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    [SerializeField]
    Transform Mid;
    [SerializeField]
    Transform Left;
    [SerializeField]
    Transform Right;

    float speed = 5f;
    float trans = 1f;

    Animator animator;

    [SerializeField]
    Rigidbody rb;
    float force = 7f;
    bool canJump;
    bool canSlide;
    int jumpHash = Animator.StringToHash("Jump");
    int slideHash = Animator.StringToHash("Slide");
    public float animSpeed = 1.0f;

    public BoxCollider m_Collider;
    public BoxCollider normalBoxSize;
    public Vector3 sizeBase = new Vector3(1.15f, 3.15f, 1f);
    public Vector3 centerBase = new Vector3(0f, 1.6f, 0f);

    public Vector3 sizeDown = new Vector3(1f, 1f, 1f);
    public Vector3 centerDown = new Vector3(0f, 0.5f, 0f);

    public bool increaseSpeed = true;
    public bool animIncreaseSpeed = true;

    int i = 0;

    public SpawnerManager spawnerManager;
    float timeToDestroy;

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider>();
        normalBoxSize = GameObject.Find("NormalBoxSize").GetComponent<BoxCollider>();
        m_Collider.size = sizeBase;
        m_Collider.center = centerBase;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpeedControl());
        StartCoroutine(AnimSpeedControl());
        timeToDestroy = spawnerManager.timeToDestroy;
    }
    private void FixedUpdate()
    {
        if (canJump)
        {
            rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            animator.SetTrigger(jumpHash);
            canJump = false;
        }
        if (canSlide)
        {
            StartCoroutine(BoxReturn());
            animator.SetTrigger(slideHash);
            canSlide = false;
        }
    }

    void VitesseJeu()
    {
        speed = speed + Time.deltaTime / 6;
        //Debug.Log(speed);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("size " + m_Collider.size);
        Debug.Log("center " + m_Collider.center);
        //Debug.Log("RIGIDBODY " + rb.velocity.y);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        animator.speed = animSpeed;
        


        if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && (transform.position.x >= Mid.position.x -1 && transform.position.x <= Mid.position.x +1))
        {
            //transform.position = new Vector3(Right.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(Right.position.x, transform.position.y, transform.position.z), trans);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (transform.position.x >= Left.position.x -1 && transform.position.x <= Left.position.x +1))
        {
            transform.position = new Vector3(Mid.position.x, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (transform.position.x >= Mid.position.x - 1 && transform.position.x <= Mid.position.x + 1))
        {
            transform.position = new Vector3(Left.position.x, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (transform.position.x >= Right.position.x - 1 && transform.position.x <= Right.position.x + 1))
        {
            transform.position = new Vector3(Mid.position.x, transform.position.y, transform.position.z);
        }

        if (!canJump && !canSlide)
        {
            animator.SetTrigger(jumpHash);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && rb.velocity.y == 0 && !canJump)
        {
            canSlide = true;
        }
    }

    private IEnumerator SpeedControl()
    {
        yield return new WaitForSeconds(0.005f);
        if(increaseSpeed == true)
        {
            speed = speed + Time.deltaTime / 6;
            StartCoroutine(SpeedControl());
        }
        if (speed >= 20)
        {
            increaseSpeed = false;
        }
        //Debug.Log(Speed);
    }
    private IEnumerator AnimSpeedControl()
    {
        yield return new WaitForSeconds(0.005f);
        if (animIncreaseSpeed == true)
        {
            animSpeed = animSpeed + Time.deltaTime / 128;
            StartCoroutine(AnimSpeedControl());
        }
        if (animSpeed >= 1.4)
        {
            animIncreaseSpeed = false;
        }
        Debug.Log(animSpeed);
    }

    private IEnumerator BoxReturn()
    {
        m_Collider.size = sizeDown;
        m_Collider.center = centerDown;

        yield return new WaitForSeconds(1.5f);
        m_Collider.size = sizeBase;
        m_Collider.center = centerBase;
    }

}
