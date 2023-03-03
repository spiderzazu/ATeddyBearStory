using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerInfo : ScriptableObject
{
    //Puntos base
    public int totalLifePoints;
    public int totalAbilityPoints;

    //Mejoras recolectadas 
    public int lifePointsCollected;
    public int abilityLeafs;
    public int leafFragments;

    public bool doubleJump;


}
