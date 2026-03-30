using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        WrapScreen();
    }

    void WrapScreen()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // Wrap horizontally
        if (viewportPos.x < 0f)
            viewportPos.x = 1f;
        else if (viewportPos.x > 1f)
            viewportPos.x = 0f;

        // Wrap vertically
        if (viewportPos.y < 0f)
            viewportPos.y = 1f;
        else if (viewportPos.y > 1f)
            viewportPos.y = 0f;

        transform.position = mainCamera.ViewportToWorldPoint(viewportPos);
    }
}
