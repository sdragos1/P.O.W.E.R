using System.Collections.Generic;
using Types;
using UI;
using UnityEngine;

public class RobotSelectionManager : MonoBehaviour
{
    public static RobotSelectionManager Instance { get; private set; }

    public RobotType SelectedRobot { get; private set; } = RobotType.None;

    private readonly List<SelectableButtonUI> _buttons = new();

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