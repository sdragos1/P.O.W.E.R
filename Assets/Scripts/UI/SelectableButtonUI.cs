using Types;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectableButtonUI : MonoBehaviour
    {
        [SerializeField] private RobotType _robotType;

        private Button _button;
        
        private void Start()
        {
            RobotSelectionManager.Instance.Register(this);
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (RobotSelectionManager.Instance.SelectedRobot == _robotType)
            {
                RobotSelectionManager.Instance.ClearSelection();
            }
            else
            {
                RobotSelectionManager.Instance.SelectRobot(_robotType);
            }
        }
    }
}

