using Types;
using UnityEngine;

public class PowerNode : MonoBehaviour
{
    [SerializeField] private Transform _energyBarTransform;

    private float _energy;
    private bool _receivedEnergyThisFrame = false;
    
    public float CurrentEnergy => _energy;
    public bool IsFullyCharged => CurrentEnergy >= GameManager.Instance.PowerNodeMaxEnergy;
    private void Update()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Execute)
        {
            return;
        }
        if (!_receivedEnergyThisFrame && _energy > 0f)
        {
            _energy -= Time.deltaTime * GameManager.Instance.PowerNodeDecayRate;
            _energy = Mathf.Max(_energy, 0);
            Debug.Log($"PowerNode at {transform.position} decaying energy. Current energy: {_energy}");
        }

        UpdateEnergyBar();

        // Reset flag at end of frame
        _receivedEnergyThisFrame = false;
    }

    public void ReceiveEnergyFrom(Robot robot)
    {
        float power = robot.GetPowerOutput();
        _energy = Mathf.Min(_energy + power * Time.deltaTime, GameManager.Instance.PowerNodeMaxEnergy);
        _receivedEnergyThisFrame = true;

        if (_energy >= GameManager.Instance.PowerNodeMaxEnergy)
        {
            _energy = GameManager.Instance.PowerNodeMaxEnergy;
            Debug.Log($"PowerNode at {transform.position} is fully charged.");
        }

        Debug.Log($"PowerNode at {transform.position} received energy. Current energy: {_energy}");
    }

    private void UpdateEnergyBar()
    {
        float normalized = _energy / GameManager.Instance.PowerNodeMaxEnergy;
        _energyBarTransform.localScale = new Vector3(normalized, 1f, 1f);
    }
}