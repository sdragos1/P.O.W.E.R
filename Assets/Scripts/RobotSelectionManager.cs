using System;
using System.Collections.Generic;
using Types;
using UI;
using UnityEngine;

public class RobotSelectionManager : MonoBehaviour
{
    public static RobotSelectionManager Instance { get; private set; }

    [Serializable]
    public class RobotData
    {
        public RobotType type;
        public Sprite icon;
        public GameObject prefab;
    }

    [SerializeField] private List<RobotData> _robots;

    public RobotType SelectedRobot { get; private set; } = RobotType.None;

    private readonly List<SelectableButtonUI> _buttons = new();


    public Sprite GetSpriteFor(RobotType type)
    {
        return _robots.Find(r => r.type == type)?.icon;
    }

    public GameObject GetPrefabFor(RobotType type)
    {
        return _robots.Find(r => r.type == type)?.prefab;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Register(SelectableButtonUI btn)
    {
        _buttons.Add(btn);
    }

    public void SelectRobot(RobotType robotType)
    {
        Instance.SelectedRobot = robotType;
    }

    public void ClearSelection()
    {
        Instance.SelectedRobot = RobotType.None;
    }
}