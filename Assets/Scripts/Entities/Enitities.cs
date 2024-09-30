using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enitities : MonoBehaviour
{
    public float jumpForce;
    public float jumpTime;
    public float health;
    public float dame;
    public float speed;
    public float weight;
    public float oxy;
    public float food;
    public float water;
    public float topor;

    protected abstract void Move();
    protected abstract void Atack();
        
}
