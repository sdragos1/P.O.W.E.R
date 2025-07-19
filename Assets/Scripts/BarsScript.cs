using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsScript : MonoBehaviour
{
    public float Bar, MaxBar;

    [SerializeField]
    private BarUI healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            setHealth(-20f);
        }

        if (Input.GetKey("e"))
        {
            setHealth(20f);
        }

    }

    public void setHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
}