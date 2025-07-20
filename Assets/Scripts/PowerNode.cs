using System;
using UnityEngine;

public class PowerNode : MonoBehaviour
{
    private float _energy;
    
    public void ReceiveEnergyFrom(Robot robot)
    {
        float power = robot.GetPowerOutput();
        _energy = Mathf.Min(_energy + power * Time.deltaTime, GameManager.Instance.PowerNodeMaxEnergy);
        Console.WriteLine($"PowerNode at {transform.position} received energy. Current energy: {_energy}");
    }
}
