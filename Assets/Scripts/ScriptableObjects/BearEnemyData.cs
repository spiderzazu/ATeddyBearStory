using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Common Enemy", menuName = "EnemyData/Common Enemy")]
public class BearEnemyData : ScriptableObject
{
    //Datos base
    public int initialLifePoints = 6;
    public int damageInflicted;
}
