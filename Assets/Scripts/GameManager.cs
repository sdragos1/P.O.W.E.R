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
    
    private GamePhase _currentPhase = GamePhase.None;
    
    public GamePhase CurrentPhase => _currentPhase;
    public int GridWidth => _gridWidth;
    public int GridHeight => _gridHeight;
    
    public int PowerNodeCount => _powerNodeCount;
    public float PowerNodeMaxEnergy => _powerNodeMaxEnergy;
    public float PowerNodeDecayRate => _powerNodeDecayRate;

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
                break;
            case GamePhase.Execute:
                topUI.SetActive(true);
                bottomUI.SetActive(false);
                break;
            case GamePhase.Finish:
                break;
        }
    }
    
    public void SetPhaseAndUpdateUI(int newPhase)
    {
        _currentPhase = (GamePhase)newPhase;
        UpdateUI();
    }
}