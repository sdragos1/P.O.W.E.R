using UnityEngine;

public class Robot : MonoBehaviour
{
    private void Update()
    {
        DetectNearbyPowerNodes();
    }
    
    private void DetectNearbyPowerNodes()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (var hit in hits)
        {
            PowerNode node = hit.GetComponent<PowerNode>();
            if (node != null)
            {
                node.ReceiveEnergyFrom(this);
            }
        }

    }
    
    public float GetPowerOutput()
    {
        return 10f;
    }
}
