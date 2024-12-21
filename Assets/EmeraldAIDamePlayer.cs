using System.Collections;
using System.Collections.Generic;
using EmeraldAI;
using PolymindGames;
using UnityEngine;

public class EmeraldAIDamePlayer : MonoBehaviour
{
    public CharacterHitbox characterHitbox;
    public void Start(){
        characterHitbox.Character.HealthManager.ReceiveDamage(10);
    }
}
