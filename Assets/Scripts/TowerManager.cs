using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{

    public TowerInfo[] towers;
    public List<Tower> placedTowers = new List<Tower>();
    public Button mortarTargetButton;
    public GameObject radiusPrefab;

    public void StartPlacingTower(int index)
    {
        GameManager gameManager = GameManager.instance;
        TowerInfo info = towers[index];

        if (info == null)
        {
            return;
        }

        if (!gameManager.playerManager.HasCoins(info.price))
        {
            return;
        }

        Vector3 towerPosition = gameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        towerPosition.z = 0;

        GameObject gameObject = Instantiate(info.prefab, towerPosition, new Quaternion());
        Tower tower = gameObject.GetComponent<Tower>();
        tower.price = info.price;
        tower.moving = true;

        gameManager.shopManager.StartPlacingTower(info);
        gameManager.shopManager.UpdateButtonColors();
    }

}

[System.Serializable]
public class TowerInfo
{

    public string name;
    public GameObject prefab;
    public Button buyButton;
    public Text priceText;
    public int price;
    public TowerUpgrade[] upgrades;

}

public enum TowerType
{
    GunTower,
    Mortar,
    Shooter
}