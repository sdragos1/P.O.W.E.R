using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Robot : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private readonly List<Vector3> _connectionPoints = new();
    private const float _detectionRadius = 1.5f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.startColor = Color.yellow;
        _lineRenderer.endColor = Color.green;
    }

    private void Update()
    {
        DetectNearbyPowerNodes();
    }

    private void DetectNearbyPowerNodes()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _detectionRadius);

        _connectionPoints.Clear();

        foreach (var hit in hits)
        {
            PowerNode node = hit.GetComponent<PowerNode>();
            if (node != null)
            {
                node.ReceiveEnergyFrom(this);
                _connectionPoints.Add(node.transform.position);
            }
        }

        RenderConnections();
    }

    private void RenderConnections()
    {
        if (_connectionPoints.Count == 0)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        _lineRenderer.positionCount = _connectionPoints.Count * 2;

        for (int i = 0; i < _connectionPoints.Count; i++)
        {
            _lineRenderer.SetPosition(i * 2, transform.position);
            _lineRenderer.SetPosition(i * 2 + 1, _connectionPoints[i]);
        }
    }

    public float GetPowerOutput()
    {
        return 30f;
    }
}