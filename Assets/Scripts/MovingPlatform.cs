using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public bool moveHorizontally = true;
    public bool moveVertically = false;
    
    [Header("Movement Pattern")]
    public MovementPattern pattern = MovementPattern.SineWave;
    
    [Header("Timing")]
    public float delay = 0f;
    
    private Vector3 startPosition;
    private float timeOffset;
    
    public enum MovementPattern
    {
        SineWave,        // Chuyển động mượt mà theo sóng sin
        PingPong,        // Di chuyển qua lại
        Circular,        // Chuyển động tròn
        Floating         // Bay lên xuống như đang nổi
    }
    
    void Start()
    {
        startPosition = transform.position;
        timeOffset = delay;
    }
    
    void Update()
    {
        float time = Time.time + timeOffset;
        Vector3 newPosition = startPosition;
        
        switch (pattern)
        {
            case MovementPattern.SineWave:
                if (moveHorizontally)
                {
                    newPosition.x += Mathf.Sin(time * moveSpeed) * moveDistance;
                }
                if (moveVertically)
                {
                    newPosition.y += Mathf.Sin(time * moveSpeed * 1.5f) * moveDistance;
                }
                break;
                
            case MovementPattern.PingPong:
                if (moveHorizontally)
                {
                    newPosition.x += Mathf.PingPong(time * moveSpeed, moveDistance * 2) - moveDistance;
                }
                if (moveVertically)
                {
                    newPosition.y += Mathf.PingPong(time * moveSpeed * 1.2f, moveDistance * 2) - moveDistance;
                }
                break;
                
            case MovementPattern.Circular:
                if (moveHorizontally && moveVertically)
                {
                    newPosition.x += Mathf.Cos(time * moveSpeed) * moveDistance;
                    newPosition.y += Mathf.Sin(time * moveSpeed) * moveDistance;
                }
                break;
                
            case MovementPattern.Floating:
                if (moveVertically)
                {
                    newPosition.y += Mathf.Sin(time * moveSpeed * 0.8f) * moveDistance * 0.5f;
                }
                if (moveHorizontally)
                {
                    newPosition.x += Mathf.Cos(time * moveSpeed * 0.6f) * moveDistance * 0.3f;
                }
                break;
        }
        
        transform.position = newPosition;
    }
    
    // Để nhân vật có thể di chuyển cùng với platform
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}

