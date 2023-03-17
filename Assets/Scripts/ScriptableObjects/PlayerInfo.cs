using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "PlayerData/Player")]
public class PlayerInfo : ScriptableObject
{
    //Puntos base
    public int totalLifePoints = 4;
    public int currentLifePoints = 4;
    public int totalAbilityPoints = 3;
    public int normalPunchDamage = 2;


    //Mejoras recolectadas 
    public int lifePointsCollected = 0;
    public int abilityLeafs = 0;
    public int leafFragments = 0;

    public bool doubleJump = false;

    public bool widePunch = false;
    public int widePunchDamage = 6;


}
