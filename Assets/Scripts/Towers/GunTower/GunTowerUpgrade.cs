using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Tower Upgrade", menuName = "TowerUpgrade/Gun Tower", order = 1)]
public class GunTowerUpgrade : TowerUpgrade
{
    public float gunSpeed;
    public float projectileSpeed;
    public float range;
    public int pierce;

}