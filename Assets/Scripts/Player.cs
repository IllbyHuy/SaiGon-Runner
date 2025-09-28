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

    // Audio tracking variables
    private bool wasWalking = false;
    private bool wasRunning = false;

    void Start()
    {
        // Phát background music khi player start
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }
    }

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
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
            animator.SetBool("Jump", true);

            // Phát âm thanh nhảy
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayJumpSound();
            }
        }

        //RUN by SHIFT
        if (Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(movement) > 0f)
        {
            isRunning = true;
            animator.SetBool("Run", true);

            // Phát âm thanh chạy (chỉ khi bắt đầu chạy)
            if (!wasRunning && AudioManager.instance != null)
            {
                AudioManager.instance.PlayRunSound();
            }
            wasRunning = true;
        }
        else
        {
            isRunning = false;
            animator.SetBool("Run", false);
            wasRunning = false;
        }

        //WALK
        if (Mathf.Abs(movement) > 0f)
        {
            animator.SetFloat("Walk", 1f);

            // Phát âm thanh đi bộ (chỉ khi bắt đầu đi và không chạy)
            if (!wasWalking && !isRunning && isGround && AudioManager.instance != null)
            {
                AudioManager.instance.PlayRunSound(); // Dùng tạm runSound cho walking
            }
            wasWalking = true;
        }
        else if (movement < .1f)
        {
            animator.SetFloat("Walk", 0f);
            wasWalking = false;
        }

        //ATTACK
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");

            // Phát âm thanh tấn công
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayEnemyHitSound(); // Dùng tạm EnemyHitSound cho attack
            }
        }
    }

    private void FixedUpdate()
    {
        float speed = isRunning ? runSpeed : moveSpeed;
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

        // Xử lý collision với các objects khác
        if (collision.gameObject.tag == "Enemy")
        {
            // Player bị enemy đánh
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayPlayerHitSound();
            }
        }

        if (collision.gameObject.tag == "Collectible" || collision.gameObject.tag == "Coin")
        {
            // Thu thập items
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayCollectSound();
            }
        }

        if (collision.gameObject.tag == "Train")
        {
            // Va chạm với tàu
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayTrainSound();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
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
            isGround = false;
        }
    }

    // Trigger events for collectibles
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible") || other.CompareTag("Coin"))
        {
            // Thu thập items
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayCollectSound();
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            // Player bị enemy đánh (trigger)
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayPlayerHitSound();
            }
        }
    }
}