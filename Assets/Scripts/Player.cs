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
    
    // Fly cheat reference
    private FlyCheat flyCheat;

    void Start()
    {
        // Phát background music khi player start
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }
        
        // Lấy reference đến FlyCheat component
        flyCheat = GetComponent<FlyCheat>();
    }

    void Update()
    {
        // Kiểm tra nếu đang ở chế độ bay thì không xử lý input bình thường
        if (flyCheat != null && flyCheat.IsFlyModeActive())
        {
            return;
        }
        
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
        // Kiểm tra nếu đang ở chế độ bay thì không xử lý movement bình thường
        if (flyCheat != null && flyCheat.IsFlyModeActive())
        {
            return;
        }
        
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

    // Trigger events
    private void OnTriggerEnter2D(Collider2D other)
    {

    }
}