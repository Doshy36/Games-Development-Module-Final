using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MortarTower : Tower
{

    public GameObject target;
    public float blastRadius;
    public float projectileSpeed;
    private bool movingTarget;

    protected override void Spawn()
    {
        target.transform.position = GameManager.instance.level.route[0].position;
    }

    protected override void OnUpdate()
    {
        if (movingTarget)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            target.transform.position = position;

            if (Input.GetMouseButtonDown(0))
            {
                movingTarget = false;
                target.SetActive(false);
            }
        }
    }

    protected override void Fire()
    {
        audioSource.Play();
        GameObject spawned = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation, GameManager.instance.level.projectileHolder.transform);
        MortarProjectile projectile = spawned.GetComponent<MortarProjectile>();

        projectile.damage = damage;
        projectile.range = blastRadius;
        projectile.speed = projectileSpeed;
        projectile.target = target.transform.position;
    }

    protected override bool HandleFiring()
    {
        return true;
    }

    public override void OnOpenUpgradeMenu()
    {
        GameManager.instance.towerManager.mortarTargetButton.gameObject.SetActive(true);
        GameManager.instance.towerManager.mortarTargetButton.onClick.RemoveAllListeners();
        GameManager.instance.towerManager.mortarTargetButton.onClick.AddListener(OnTargetButtonClick);

        target.SetActive(true);
    }

    public override void OnCloseUpgradeMenu()
    {
        GameManager.instance.towerManager.mortarTargetButton.gameObject.SetActive(false);

        target.SetActive(false);
        movingTarget = false;
    }

    public void OnTargetButtonClick()
    {
        target.SetActive(true);
        movingTarget = true;
    }

    public override void Upgrade(TowerUpgrade upgradeInfo)
    {
        base.Upgrade(upgradeInfo);

        MortarTowerUpgrade upgrade = (MortarTowerUpgrade)upgradeInfo;

        blastRadius = upgrade.blastRadius;
        projectileSpeed = upgrade.projectileSpeed;
        target.transform.localScale = Vector3.one * blastRadius;
    }

    public override List<Text> ShowValueText(TowerInfo info, TowerUpgrade nextUpgrade, Func<Text> createText)
    {
        List<Text> text = base.ShowValueText(info, nextUpgrade, createText);

        text.Add(createText());
        text.Add(createText());

        text[2].text = "Blast Radius: " + blastRadius;
        text[3].text = "Projectile Speed: " + projectileSpeed;

        if (nextUpgrade != null)
        {
            MortarTowerUpgrade upgrade = (MortarTowerUpgrade)nextUpgrade;

            text[2].text += " (-> " + upgrade.blastRadius + ")";
            text[3].text += " (-> " + upgrade.projectileSpeed + ")";
        }

        return text;
    }

}
