using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class PlayerController : MonoBehaviour
{
    //Save Variables
    public int score = 0;
    public int currentcoins = 0;


    //Movement Variables
    private enum Side { Left, Mid, Right }
    private Side m_side = Side.Mid;
    private bool SwipeLeft, SwipeRight, SwipeUp;
    private Vector2 StartTouchPos, EndTouchPos;

    [SerializeField] private float zvalue;
    private float newzpos = 0f;
    private float z;
    
    public float dodgeSpeed;
    public float FwdSpeed = 10f;

    public int jumpgravity = -30;
    private Vector3 playerVelocity;
    public int jumpheight = 2;


    //GameObject Components
    private CharacterController m_char;
    private Animator anim;


    private void Start()
    {

        m_char = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }



    private void Update()
    {

        Movement();
        Jumping();

    }

    private void Movement()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartTouchPos = Input.GetTouch(0).position;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            EndTouchPos = Input.GetTouch(0).position;

            if (EndTouchPos.y < StartTouchPos.y && (StartTouchPos.y - EndTouchPos.y) > 10 )
            {

                SwipeLeft = true;
            }
            else if (EndTouchPos.y > StartTouchPos.y && (EndTouchPos.y - StartTouchPos.y) >10)
            {
                SwipeRight = true;

            }
            else
            {
                SwipeUp = true;
            }
        }


        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        //SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);





        if (SwipeLeft)
        {
            if (m_side == Side.Mid)
            {
                newzpos = -zvalue;
                m_side = Side.Left;
            }
            else if (m_side == Side.Right)
            {
                newzpos = 0;
                m_side = Side.Mid;
            }
            SwipeLeft = false;
        }

        if (SwipeRight)
        {
            if (m_side == Side.Mid)
            {
                newzpos = zvalue;
                m_side = Side.Right;
            }
            else if (m_side == Side.Left)
            {
                newzpos = 0;
                m_side = Side.Mid;
            }
            SwipeRight = false;
        }
        Vector3 MoveVector = new Vector3(-FwdSpeed * Time.deltaTime , 0, z - transform.position.z);


        z = Mathf.Lerp(z, newzpos, Time.deltaTime * dodgeSpeed);

        m_char.Move(MoveVector);

        playerVelocity.y += jumpgravity * Time.deltaTime;
        m_char.Move(playerVelocity * Time.deltaTime);


    }

    void Jumping()
    {
        if (m_char.isGrounded)
        {

            playerVelocity.y = 0f;

            if (SwipeUp)
            {
                anim.SetBool("jump", true);
                playerVelocity.y += Mathf.Sqrt(jumpheight * -3.0f * jumpgravity);
                SwipeUp = false;

            }
            anim.SetBool("fallen", true);
            anim.SetBool("fall", false);
        }


        else
        {
            anim.SetBool("fallen", false);
        }

        if (playerVelocity.y < 0)
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "obstacle")
        {
            Scores();
            SceneManager.LoadScene("Menu");

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectable")
        {
            currentcoins++;
            Destroy(other.gameObject);

        }
    }

    private void Scores()
    {
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        int coins = PlayerPrefs.GetInt("coins", 0);

        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);

        }

        PlayerPrefs.SetInt("coins", coins+currentcoins);
    }


}