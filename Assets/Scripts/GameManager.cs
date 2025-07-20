using System;
using Types;
using Unity.VisualScripting;
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
    
    [Header("General Settings")]
    [SerializeField] private Vector2 hotspot = Vector2.zero;
    [SerializeField] private Texture2D customCursor;

    [SerializeField] private GameObject bottomUI;
    [SerializeField] private GameObject topUI;
    [SerializeField] private GameObject winUI;
    
    private GamePhase _currentPhase = GamePhase.None;
    
    private Vector2? _hoveredTilePosition = null;
    
    public GamePhase CurrentPhase => _currentPhase;
    public int GridWidth => _gridWidth;
    public int GridHeight => _gridHeight;
    
    public int PowerNodeCount => _powerNodeCount;
    public float PowerNodeMaxEnergy => _powerNodeMaxEnergy;
    public float PowerNodeDecayRate => _powerNodeDecayRate;
    public Vector2? HoveredTilePosition => _hoveredTilePosition;

    private void Start()
    {
        _currentPhase = GamePhase.Plan;
        UpdateUI();
        Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto);
    }
    
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

    private void UpdateUI()
    {
        switch (_currentPhase)
        {
            case GamePhase.Plan:
                bottomUI.SetActive(true);
                topUI.SetActive(false);
                winUI.SetActive(false); 
                break;
            case GamePhase.Execute:
                topUI.SetActive(true);
                winUI.SetActive(false);
                bottomUI.SetActive(false);
                break;
            case GamePhase.Finish:
                topUI.SetActive(false);
                bottomUI.SetActive(false);
                winUI.SetActive(true);
                break;
        }
    }
    
    public void SetPhaseAndUpdateUI(int newPhase)
    {
        _currentPhase = (GamePhase)newPhase;
        UpdateUI();
    }
    
    public void SetHoveredTilePosition(Vector2 position)
    {
        _hoveredTilePosition = position;
    }
    
    public void ClearHoveredTilePosition()
    {
        _hoveredTilePosition = null;
    }
}