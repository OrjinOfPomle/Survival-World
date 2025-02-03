using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Movement_2D : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rb2d;
    private float screenWidth;
    private float hMoveSpeed = 100f;
    private bool alive = true;
    
    SpriteRenderer spriteRenderer;
    public GameObject GameObject;
    public int nextLv;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    void Update()
    {
        if (alive) { 
            int touchCount = Input.touchCount;
            int i = 0;

            Debug.Log("touchcount  =" + touchCount);
            if (touchCount == 0)
            {
                Debug.Log("this was hit but animator didnt play");
                animator.Play("robot_without_fire");
            }
            while (touchCount != 0 && touchCount <= 2 && i < Input.touchCount)
            {
                if (Input.GetTouch(i).position.x < screenWidth * 0.15f)
                {
                    if (touchCount > 1 && i == 0 && (Input.GetTouch(i + 1).position.x > screenWidth * 0.85f))
                    {
                        // if up is the other button
                        moveLeftAndRight(-1.0f);
                        moveUp(1.0f);
                        animator.Play("robot_fire_right_and_bottom");
                        i++;
                    }
                    else
                    {
                        //move left
                        moveLeftAndRight(-1.0f);
                        animator.Play("robot_fire_right");
                    }

                }
                else if (Input.GetTouch(i).position.x < screenWidth * 0.30f)
                {// move right
                    if (touchCount > 1 && i == 0 && (Input.GetTouch(i + 1).position.x > screenWidth * 0.85f))
                    {
                        moveLeftAndRight(1.0f);
                        moveUp(1.0f);
                        animator.Play("robot_fire_left_and_bottom");
                        i++;
                    }
                    else
                    {
                        moveLeftAndRight(1.0f);
                        animator.Play("robot_fire_left");
                    }
                }
                else if (Input.GetTouch(i).position.x > screenWidth * 0.85f)
                {//move up
                    if (touchCount > 1 && i == 0 && (Input.GetTouch(i + 1).position.x < screenWidth * 0.15f))
                    {//move left and up
                        moveLeftAndRight(-1.0f);
                        moveUp(1.0f);
                        animator.Play("robot_fire_right_and_bottom");
                        i++;
                    }
                    else if (touchCount > 1 && i == 0 && (Input.GetTouch(i + 1).position.x < screenWidth * 0.30f))
                    {//move up and right
                        moveLeftAndRight(1.0f);
                        moveUp(1.0f);
                        animator.Play("robot_fire_left_and_bottom");
                        i++;
                    }
                    else
                    {
                        moveUp(1.0f);
                        animator.Play("robot fire bottom");
                    }
                }
                else
                {
                    animator.Play("robot without fire");
                }
                i++;
            }
        }
    }
    private void FixedUpdate()
    {

    }

    private void moveLeftAndRight(float horizontalInput) {
        
        rb2d.AddForce(new Vector2(horizontalInput * hMoveSpeed * Time.deltaTime, 0));
    }
    private void moveUp(float verticalInput) {
        rb2d.AddForce(new Vector2(0,verticalInput * 320 * Time.deltaTime));
    }

    private void generalMovement(float x, float y) {
       // rb2d.AddForce(new Vector2(rb2d.velocity.x + x * hMoveSpeed * Time.deltaTime, verticalInput * 300 * Time.deltaTime));
    }


   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("unsafe")) {
            Player_Timer.stopTime = true;
            animator.Play("Explosion");
            rb2d.velocity = new Vector2(0,0);
            rb2d.angularVelocity = 0f;
            rb2d.gravityScale = 0.0f;
            alive = false;
            StartCoroutine(WaitAnimation());
        }
        else if(collision.gameObject.tag.Equals("safeNextLv")){
            animator.Play("robot_without_fire");
            
            Player_Timer.stopTime = true;
            rb2d.velocity = new Vector2(0, 0);
            rb2d.angularVelocity = 0f;
            rb2d.gravityScale = 0.0f;
            alive = false;
            StartCoroutine(smallgap());
        }
    }
    IEnumerator smallgap()
    {
        
        yield return new WaitForSeconds(3);
        Player_Timer.stopTime = false;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    IEnumerator WaitAnimation()
    {
        int currentlvl = SceneManager.GetActiveScene().buildIndex;

        //run animation
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        int livesLeft = PlayerPrefs.GetInt("lives", Lives.retrys);
        if (currentlvl != 0)
        {
            if (livesLeft <= 1) {
                SceneManager.LoadScene(0);
                gameObject.SetActive(true);
                alive = true;
                Player_Timer.stopTime = false;
            }
            else {
                PlayerPrefs.SetInt("lives", livesLeft);
                SceneManager.LoadScene(currentlvl);
                gameObject.SetActive(true);
                alive = true;
                Player_Timer.stopTime = false;
            }

        }
        else {
            SceneManager.LoadScene(0);
            gameObject.SetActive(true);
            alive = true;
            Player_Timer.stopTime = false;
        }

        

    }



}
