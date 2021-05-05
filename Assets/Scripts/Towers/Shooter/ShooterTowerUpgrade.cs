using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shooter Tower Upgrade", menuName = "TowerUpgrade/Shooter Tower", order = 1)]
public class ShooterTowerUpgrade : TowerUpgrade
{
    public float range;
    public float projectileSpeed;
    public int pierce;
}
