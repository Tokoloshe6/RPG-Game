using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public Slider playerHealthSlider;
    public float playerMaxHealth = 100f; 
    public float health = 100f;
    public float healthRegen = 1f; 
   
    void Start()
    {
        
    }


    void Update()
    {
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = health;
        }
    }
}
