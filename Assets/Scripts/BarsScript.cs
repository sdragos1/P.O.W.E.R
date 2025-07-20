using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsScript : MonoBehaviour
{
    public float Pollution, MaxBar;

    [SerializeField]
    private BarUI pollutionBar;

    // Start is called before the first frame update
    void Start()
    {
        pollutionBar.SetMaxBar(MaxBar);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            setPollution(-20f);
        }

        if (Input.GetKeyDown("e"))
        {

            setPollution(+20f);
        }

    }

    public void setPollution(float pollutionChange)
    {
        Pollution += pollutionChange;
        Pollution = Mathf.Clamp(Pollution, 0, MaxBar);

        pollutionBar.SetPollution(Pollution);
    }
    
}