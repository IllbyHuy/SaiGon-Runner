using UnityEngine;

public class FlyCheat : MonoBehaviour
{
    [Header("Fly Cheat Settings")]
    public float flySpeed = 8f;
    public float flyHeight = 2f;
    public KeyCode[] cheatKeys = { KeyCode.U, KeyCode.I, KeyCode.O };
    
    private bool isFlyMode = false;
    private bool[] keyPressed = new bool[3];
    private Player player;
    private Rigidbody2D rb;
    private float originalGravityScale;
    private bool wasGrounded;
    
    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
    }
    
    void Update()
    {
        CheckCheatInput();
        
        if (isFlyMode)
        {
            HandleFlyMovement();
        }
    }
    
    void CheckCheatInput()
    {
        // Kiểm tra từng phím trong tổ hợp
        for (int i = 0; i < cheatKeys.Length; i++)
        {
            if (Input.GetKeyDown(cheatKeys[i]))
            {
                keyPressed[i] = true;
            }
            else if (Input.GetKeyUp(cheatKeys[i]))
            {
                keyPressed[i] = false;
            }
        }
        
        // Kiểm tra nếu tất cả 3 phím đều được nhấn cùng lúc
        bool allKeysPressed = true;
        for (int i = 0; i < keyPressed.Length; i++)
        {
            if (!keyPressed[i])
            {
                allKeysPressed = false;
                break;
            }
        }
        
        // Toggle fly mode khi nhấn tổ hợp phím
        if (allKeysPressed)
        {
            ToggleFlyMode();
            // Reset trạng thái phím để tránh spam
            for (int i = 0; i < keyPressed.Length; i++)
            {
                keyPressed[i] = false;
            }
        }
    }
    
    void ToggleFlyMode()
    {
        isFlyMode = !isFlyMode;
        
        if (isFlyMode)
        {
            // Bật chế độ bay
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            wasGrounded = player.isGround;
            player.isGround = false;
            
            Debug.Log("Cheat bay đã được kích hoạt! Nhấn U+I+O lần nữa để tắt.");
        }
        else
        {
            // Tắt chế độ bay
            rb.gravityScale = originalGravityScale;
            player.isGround = wasGrounded;
            
            Debug.Log("Cheat bay đã được tắt!");
        }
    }
    
    void HandleFlyMovement()
    {
        // Di chuyển ngang và dọc khi bay
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = 0f;
        
        // Di chuyển lên xuống khi bay (sử dụng Input.GetAxis cho mượt hơn)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            verticalInput = flySpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            verticalInput = -flySpeed;
        }
        
        // Cho phép di chuyển lên/xuống liên tục khi bay
        rb.linearVelocity = new Vector2(horizontalInput * flySpeed, verticalInput);
        
        // Bỏ giới hạn độ cao để có thể bay tự do
        // if (transform.position.y > flyHeight)
        // {
        //     transform.position = new Vector3(transform.position.x, flyHeight, transform.position.z);
        //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Min(rb.linearVelocity.y, 0f));
        // }
    }
    
    // Public method để kiểm tra trạng thái fly mode từ script khác
    public bool IsFlyModeActive()
    {
        return isFlyMode;
    }
}
