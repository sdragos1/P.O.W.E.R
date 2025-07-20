using Types;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Grid Settings")]
    [SerializeField] private int _gridWidth = 10;
    [SerializeField] private int _gridHeight = 10;
    [Header("Power Node Settings")]
    [SerializeField] private int _powerNodeCount = 5;
    [SerializeField] private float _powerNodeMaxEnergy = 100;
    [SerializeField] private float _powerNodeDecayRate = 5f;
    
    public int GridWidth => _gridWidth;
    public int GridHeight => _gridHeight;
    
    public int PowerNodeCount => _powerNodeCount;
    public float PowerNodeMaxEnergy => _powerNodeMaxEnergy;
    public float PowerNodeDecayRate => _powerNodeDecayRate;
    
    
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