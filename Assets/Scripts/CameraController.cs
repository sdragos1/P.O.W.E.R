using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        SizeAndCenterCamera();
    }

    private void SizeAndCenterCamera()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is not set. Please ensure GameManager is initialized before CameraController.");
            return;
        }

        var gridWidth = GameManager.Instance.GridWidth;
        var gridHeight = GameManager.Instance.GridHeight;

        float aspectRatio = (float)Screen.width / Screen.height;

        float verticalSize = gridHeight / 2f;
        float horizontalSize = gridWidth / (2f * aspectRatio);

        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please ensure a camera is tagged as 'MainCamera'.");
            return;
        }

        mainCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);

        mainCamera.transform.position = new Vector3(
            gridWidth / 2f - 0.5f,
            gridHeight / 2f - 0.5f,
            -10f
        );
    }
}