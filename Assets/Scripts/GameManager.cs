using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Grid Settings")] [SerializeField]
    private int _gridWidth = 10;

    [SerializeField] private int _gridHeight = 10;
    public int GridWidth => _gridWidth;
    public int GridHeight => _gridHeight;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}