using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVital : MonoBehaviour
{
    Player player;
    public Slider sliderHealth;
    public float healthFallRate;
    
    public Slider sliderHunger;
    public float hungerFallRate;

    public Slider sliderThirst;
    public float thirstFallRate;
    void Start()
    {
        player = GetComponent<Player>();
        sliderHealth.maxValue = player.maxHealth;
        sliderHealth.value = player.health;

        sliderThirst.maxValue = player.maxThirst;
        sliderThirst.value = player.thirst;

        sliderHunger.maxValue = player.maxHunger;
        sliderHunger.value = player.hunger;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliderHunger.value == 0 && sliderThirst.value == 0)
        {
            sliderHealth.value -= Time.deltaTime / healthFallRate * 2;
        }
        else if (sliderThirst.value == 0 || sliderHunger.value == 0)
        {
            sliderHealth.value -= Time.deltaTime / healthFallRate;
        }
        else if (sliderHealth.value >= sliderHealth.maxValue)
        {
            sliderHealth.value = player.maxHealth;
        }
        else
        {
            sliderHealth.value += 0.01f * player.maxHealth * Time.deltaTime;
        }

        if (sliderHunger.value >= 0)
        {
            sliderHunger.value -= Time.deltaTime / hungerFallRate;
        }
        else if (sliderHunger.value <= 0)
        {
            sliderHunger.value = 0;
        }
        else if (sliderHunger.value >= sliderHunger.maxValue)
        {
            sliderHunger.value = player.maxHunger;
        }

        if (sliderThirst.value >= 0)
        {
            sliderThirst.value -= Time.deltaTime / thirstFallRate;
        }
        else if (sliderThirst.value <= 0)
        {
            sliderThirst.value = 0;
        }
        else if (sliderThirst.value >= sliderThirst.maxValue)
        {
            sliderThirst.value = player.maxThirst;
        }
    }
}
