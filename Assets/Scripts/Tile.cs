using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    
    public void Init(bool isOffset) {
        if (isOffset)
        {
            _renderer.color = _offsetColor;
        }
    }
}
