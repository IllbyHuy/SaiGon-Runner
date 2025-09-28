using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneTransition : MonoBehaviour
{
    [Header("Cài đặt vị trí trigger")]
    [SerializeField] private float triggerX = 50f; // Vị trí x để kích hoạt cutscene
    
    [Header("Cài đặt hiệu ứng flash")]
    [SerializeField] private int flashCount = 3; // Số lần chớp
    [SerializeField] private float flashDuration = 0.2f; // Thời gian mỗi lần chớp
    [SerializeField] private float flashInterval = 0.3f; // Khoảng thời gian giữa các lần chớp
    [SerializeField] private float transitionDelay = 1f; // Thời gian chờ trước khi chuyển scene
    
    [Header("UI Elements")]
    [SerializeField] private GameObject flashPanel; // Panel trắng để tạo hiệu ứng flash
    
    private bool hasTriggered = false;
    private bool isTransitioning = false;
    
    private void Start()
    {
        
        // Tạo flash panel nếu chưa có
        if (flashPanel == null)
        {
            CreateFlashPanel();
        }
        else
        {
            flashPanel.SetActive(false);
        }
    }
    
    private void Update()
    {
        // Chỉ kiểm tra khi chưa trigger và chưa đang transition
        if (!hasTriggered && !isTransitioning)
        {
            Player player = FindAnyObjectByType<Player>();
            if (player != null && player.transform.position.x >= triggerX)
            {
                StartCutsceneTransition();
            }
        }
    }
    
    private void StartCutsceneTransition()
    {
        hasTriggered = true;
        isTransitioning = true;
        Debug.Log("Bắt đầu cutscene transition từ Map3 sang Map4");
        
        // Dừng player movement
        Player player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            player.enabled = false; // Tạm dừng script Player
        }
        
        // Bắt đầu coroutine flash effect
        StartCoroutine(FlashEffect());
    }
    
    private IEnumerator FlashEffect()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // Hiện flash panel
            flashPanel.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            
            // Ẩn flash panel
            flashPanel.SetActive(false);
            
            // Chờ interval (trừ lần cuối)
            if (i < flashCount - 1)
            {
                yield return new WaitForSeconds(flashInterval);
            }
        }
        
        // Chờ thêm một chút trước khi chuyển scene
        yield return new WaitForSeconds(transitionDelay);
        
        // Tự động chuyển sang Map4
        Debug.Log("Cutscene Map3 hoàn thành, chuyển sang Map4!");
        Time.timeScale = 1f; // Đảm bảo time scale về bình thường
        SceneManager.LoadScene("Map4");
    }
    
    private void CreateFlashPanel()
    {
        // Tạo Canvas nếu chưa có
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("FlashCanvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }
        
        // Tạo flash panel
        flashPanel = new GameObject("FlashPanel");
        flashPanel.transform.SetParent(canvas.transform, false);
        
        // Thêm RectTransform
        RectTransform rectTransform = flashPanel.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        // Thêm Image component
        UnityEngine.UI.Image image = flashPanel.AddComponent<UnityEngine.UI.Image>();
        image.color = Color.white;
        
        // Đặt thứ tự hiển thị cao nhất
        flashPanel.transform.SetAsLastSibling();
        
        // Ẩn panel ban đầu
        flashPanel.SetActive(false);
    }
    
    // Method để thiết lập vị trí trigger từ code khác
    public void SetTriggerPosition(float x)
    {
        triggerX = x;
    }
    
    // Method để thiết lập số lần flash từ code khác
    public void SetFlashCount(int count)
    {
        flashCount = count;
    }
    
    // Vẽ gizmo trong Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = new Vector3(triggerX, transform.position.y, 0);
        Gizmos.DrawWireCube(position, new Vector3(2f, 6f, 1f));
        
        // Hiển thị text
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(position + Vector3.up * 3f, $"Cutscene Trigger\n{flashCount} flashes → Map4");
        #endif
    }
}
