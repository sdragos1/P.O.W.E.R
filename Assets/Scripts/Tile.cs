using System;
using Types;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    private Vector2 _position;

    public void Init(bool isOffset, Vector2 position)
    {
        _position = position;
        if (isOffset)
        {
            _renderer.color = _offsetColor;
        }
    }

    void OnMouseEnter()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Plan)
            return;
        GameManager.Instance.SetHoveredTilePosition(_position);
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        GameManager.Instance.ClearHoveredTilePosition();
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (RobotSelectionManager.Instance.SelectedRobot == RobotType.None)
            return;

        if (!RobotSelectionManager.Instance.CanPlaceSelectedRobot())
        {
            // Show the limit reached message
            LimitReachedUI limitUI = FindObjectOfType<LimitReachedUI>();
            if (limitUI != null)
                limitUI.ShowMessage();

            return;
        }

        SpawnRobot(RobotSelectionManager.Instance.GetCurrentSelectedRobotPrefab());
        RobotSelectionManager.Instance.RegisterPlacedRobot(RobotSelectionManager.Instance.SelectedRobot);
        RobotSelectionManager.Instance.ClearSelection();
    }

    private void SpawnRobot(GameObject robotPrefab)
    {
        Transform robotParent = GameObject.Find("Robots")?.transform;
        Console.WriteLine("Robot spawned at: " + transform.position);
        if (robotPrefab == null) return;

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1f);
        var robot = Instantiate(robotPrefab, spawnPosition, Quaternion.identity);
        if (robotParent != null)
            robot.transform.SetParent(robotParent);
    }
}