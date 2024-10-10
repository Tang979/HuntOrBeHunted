using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enitities : MonoBehaviour
{
    public float jumpForce;
    public float jumpTime;
    public float maxHealth;
    public float health;
    public float hunger;
    public float maxHunger;
    public float thirst;
    public float maxThirst;
    public float dame;
    public float speed;
    public float weight;
    public float maxWeight;
    public float oxy;
    public float torpidity;

    protected abstract void Move();
    protected abstract void Atack();
        
}
