using UnityEngine;

public class PositionTrigger : MonoBehaviour
{
    [Header("Cài đặt vị trí chuyển scene")]
    [SerializeField] private float targetX = 50f; // Vị trí x mà player cần đạt đến
    [SerializeField] private string targetSceneName = "Map4"; // Tên scene đích
    
    [Header("Tùy chọn")]
    [SerializeField] private bool useExactPosition = false; // Nếu true, phải chính xác tại vị trí x
    [SerializeField] private float tolerance = 1f; // Khoảng dung sai nếu không dùng exact position
    
    private GameManager gameManager;
    private bool hasTriggered = false; // Đảm bảo chỉ trigger một lần
    
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Không tìm thấy GameManager trong scene!");
        }
    }
    
    private void Update()
    {
        // Tìm player trong scene
        Player player = FindAnyObjectByType<Player>();
        if (player != null && !hasTriggered)
        {
            CheckPlayerPosition(player.transform);
        }
    }
    
    private void CheckPlayerPosition(Transform playerTransform)
    {
        float playerX = playerTransform.position.x;
        bool shouldTrigger = false;
        
        if (useExactPosition)
        {
            // Phải chính xác tại vị trí x
            shouldTrigger = Mathf.Abs(playerX - targetX) <= tolerance;
        }
        else
        {
            // Chỉ cần vượt qua vị trí x
            shouldTrigger = playerX >= targetX;
        }
        
        if (shouldTrigger)
        {
            TriggerSceneTransition();
        }
    }
    
    private void TriggerSceneTransition()
    {
        hasTriggered = true;
        Debug.Log($"Player đã đến vị trí x = {targetX}, chuyển sang scene {targetSceneName}");
        
        // Gọi method tương ứng trong GameManager
        switch (targetSceneName)
        {
            case "Map1":
                gameManager.LoadMap1();
                break;
            case "Map2":
                gameManager.LoadMap2();
                break;
            case "Map3":
                gameManager.LoadMap3();
                break;
            case "Map4":
                gameManager.LoadMap4();
                break;
            default:
                // Fallback: sử dụng SceneManager trực tiếp
                UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
                break;
        }
    }
    
    // Method để thiết lập vị trí từ code khác
    public void SetTargetPosition(float x)
    {
        targetX = x;
    }
    
    // Method để thiết lập scene đích từ code khác
    public void SetTargetScene(string sceneName)
    {
        targetSceneName = sceneName;
    }
    
    // Vẽ gizmo trong Scene view để dễ nhìn thấy vị trí trigger
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 position = new Vector3(targetX, transform.position.y, 0);
        Gizmos.DrawWireCube(position, new Vector3(1f, 5f, 1f));
        
        // Hiển thị text
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(position + Vector3.up * 2f, $"Trigger: {targetSceneName}");
        #endif
    }
}
