using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    public bool isGround = true;

    private float movement;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    private bool isRunning = false;
    private bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        //FLIP
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        //JUMP
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
            animator.SetBool("Jump", true);
        }

        //RUN by SHIFT
        if (Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(movement) > 0f)
        {
            isRunning = true;
            animator.SetBool("Run", true);
        }
        else
        {
            isRunning = false;
            animator.SetBool("Run", false);
        }

        //WALK
        if (Mathf.Abs(movement) > 0f)
        {
            animator.SetFloat("Walk", 1f);
        }
        else if (movement < .1f)
        {
            animator.SetFloat("Walk", 0f);
        }

        //ATTACK
        if
            (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        float speed = isRunning ? runSpeed : moveSpeed;
        // Sử dụng Rigidbody2D để di chuyển thay vì transform.position
        // Điều này giúp tránh bị stuck khi collision
        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // Kiểm tra xem có điểm tiếp xúc nào ở dưới nhân vật không
            bool hasGroundBelow = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y < transform.position.y - 0.2f) // Cho phép một chút tolerance
                {
                    hasGroundBelow = true;
                    break;
                }
            }
            
            // Chỉ set isGround = true khi có ground thực sự ở dưới và đang rơi xuống
            if (hasGroundBelow && rb.linearVelocity.y <= 0.1f)
            {
                isGround = true;
                animator.SetBool("Jump", false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // Kiểm tra liên tục khi đang ở trên ground
            bool hasGroundBelow = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y < transform.position.y - 0.2f)
                {
                    hasGroundBelow = true;
                    break;
                }
            }
            
            if (hasGroundBelow && rb.linearVelocity.y <= 0.1f)
            {
                isGround = true;
                animator.SetBool("Jump", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // Luôn set isGround = false khi rời khỏi ground
            isGround = false;
        }
    }

}
