using UnityEngine;

public class UI_Blade_Warning : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Warning Settings")]
    public float detectionDistance = 8f;

    [Header("UI")]
    public RectTransform warningUI;   // The icon or panel on screen
    public Canvas canvas;

    private Camera cam;
    public GameObject[] blades;

    private float screenMinY = 50f;    // Padding
    private float screenMaxY = -50f;

    void Start()
    {
        player = GameObject.Find("Body_Light_Shark_Purple").transform;
        cam = Camera.main;
        blades = GameObject.FindGameObjectsWithTag("Blade");

        if (warningUI != null)
            warningUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Body_Light_Shark_Purple").transform;
        }
        GameObject closestBlade = GetClosestBlade();

        if (closestBlade == null)
        {
            warningUI.gameObject.SetActive(false);
            return;
        }
        if (IsBladeVisible(closestBlade))
        {
            warningUI.gameObject.SetActive(false);
            return;
        }

        warningUI.gameObject.SetActive(true);

        // Convert blade world position to screen position
        Vector3 screenPos = cam.WorldToScreenPoint(closestBlade.transform.position);

        // Clamp vertically to screen
        float clampedY = Mathf.Clamp(screenPos.y, screenMinY, Screen.height - screenMinY);

        // If blade is above the screen
        if (screenPos.y > Screen.height)
            clampedY = Screen.height - screenMinY;

        // If blade is below the screen
        if (screenPos.y < 0)
            clampedY = screenMinY;

        // Apply clamped position to UI element
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            new Vector2(screenPos.x, clampedY),
            canvas.worldCamera,
            out uiPos
        );
        uiPos.x = warningUI.anchoredPosition.x;
        

        warningUI.anchoredPosition = uiPos;
    }

    GameObject GetClosestBlade()
    {
        GameObject closest = null;
        float closestDist = Mathf.Infinity;

        foreach (GameObject blade in blades)
        {
            if (blade == null) continue;

            float dist = Vector3.Distance(player.position, blade.transform.position);
            if (dist < detectionDistance && dist < closestDist)
            {
                closest = blade;
                closestDist = dist;
            }
        }

        return closest;
    }

    // Optional if blades spawn dynamically
    public void RefreshBlades()
    {
        blades = GameObject.FindGameObjectsWithTag("Blade");
    }
    bool IsBladeVisible(GameObject blade)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(blade.transform.position);

        // If behind camera
        if (viewportPos.z < 0)
            return false;

        // Visible if inside camera's 0–1 viewport rectangle
        return viewportPos.x >= 0f && viewportPos.x <= 1f &&
               viewportPos.y >= 0f && viewportPos.y <= 1f;
    }
}