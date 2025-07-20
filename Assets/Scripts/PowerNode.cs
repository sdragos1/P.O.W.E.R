using System;
using UnityEngine;

public class PowerNode : MonoBehaviour
{
    [SerializeField] private Transform _energyBarTransform;
    
    private float _energy;
    
    public void ReceiveEnergyFrom(Robot robot)
    {
        float power = robot.GetPowerOutput();
        _energy = Mathf.Min(_energy + power * Time.deltaTime, GameManager.Instance.PowerNodeMaxEnergy);
        if (_energy >= GameManager.Instance.PowerNodeMaxEnergy)
        {
            _energy = GameManager.Instance.PowerNodeMaxEnergy;
            Debug.Log($"PowerNode at {transform.position} is fully charged.");
        }
        Debug.Log($"PowerNode at {transform.position} received energy. Current energy: {_energy}");
        
        UpdateEnergyBar();
    }
    
    private void UpdateEnergyBar()
    {
        float normalized = _energy / GameManager.Instance.PowerNodeMaxEnergy;
        _energyBarTransform.localScale = new Vector3(normalized, 1f, 1f);
    }
}
