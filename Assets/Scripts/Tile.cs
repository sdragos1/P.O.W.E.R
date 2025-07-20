using System;
using Types;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public void Init(bool isOffset)
    {
        if (isOffset)
        {
            _renderer.color = _offsetColor;
        }
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (RobotSelectionManager.Instance.SelectedRobot == RobotType.None)
            return;
        
        SpawnRobot(RobotSelectionManager.Instance.GetCurrentSelectedRobotPrefab());
        RobotSelectionManager.Instance.ClearSelection();
    }

    private void SpawnRobot(GameObject robotPrefab)
    {
        Console.WriteLine("Robot spawned at: " + transform.position);
        if (robotPrefab == null) return;

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1f);
        var robot = Instantiate(robotPrefab, spawnPosition, Quaternion.identity);
        robot.transform.SetParent(transform);
    }
}