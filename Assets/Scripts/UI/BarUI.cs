using UnityEngine;

public class BarUI : MonoBehaviour
{
    public float Pollution, MaxBar, Width, Height;

    [SerializeField] private RectTransform pollutionBar;

    public void SetMaxBar(float maxBar)
    {
        MaxBar = maxBar;
    }


    public void SetPollution(float pollution)
    {
        Pollution = pollution;
        float newWidth = (Pollution / MaxBar) * Width;

        pollutionBar.sizeDelta = new Vector2(newWidth, Height);
    }
}