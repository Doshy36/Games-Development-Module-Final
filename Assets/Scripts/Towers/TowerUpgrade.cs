using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TowerUpgrade : ScriptableObject
{

    public string upgradeName;
    public GameObject upgradePrefab;
    public int upgradePrice;
    public int damage;
    public float fireRate;

    [System.Serializable]
    public class TowerUpgradeData<T>
    {
        public string key;
        public T value;
    }

}