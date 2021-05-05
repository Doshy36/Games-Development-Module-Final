using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterTower : Tower
{

    public Transform[] firePoints;
    public float projectileSpeed;
    public float range;
    public int pierce;

    protected override void Spawn()
    {
        radius.transform.localScale = Vector3.one * (range / 2);
    }

    protected override bool HandleFiring()
    {
        return GameManager.instance.GetClosestEnemy(transform.position, range) != null;
    }

    protected override void Fire()
    {
        audioSource.Play();
        foreach (Transform transform in firePoints)
        {
            GameObject spawned = Instantiate(projectilePrefab, transform.position, transform.rotation, GameManager.instance.level.projectileHolder.transform);
            Projectile projectile = spawned.GetComponent<Projectile>();

            projectile.direction = () =>
            {
                return transform.rotation * Vector3.forward;
            };

            projectile.damage = damage;
            projectile.pierce = pierce;
            projectile.speed = projectileSpeed;
        }
    }

    public override void Upgrade(TowerUpgrade upgradeInfo)
    {
        base.Upgrade(upgradeInfo);

        ShooterTowerUpgrade upgrade = (ShooterTowerUpgrade)upgradeInfo;

        range = upgrade.range;
        projectileSpeed = upgrade.projectileSpeed;
        pierce = upgrade.pierce;

        radius.transform.localScale = Vector3.one * (range / 2);
    }

    public override List<Text> ShowValueText(TowerInfo info, TowerUpgrade nextUpgrade, Func<Text> createText)
    {
        List<Text> text = base.ShowValueText(info, nextUpgrade, createText);

        text.Add(createText());
        text.Add(createText());
        text.Add(createText());

        text[2].text = "Range: " + range;
        text[3].text = "Projectile Speed: " + projectileSpeed;
        text[4].text = "Pierce: " + pierce;

        if (nextUpgrade != null)
        {
            ShooterTowerUpgrade upgrade = (ShooterTowerUpgrade)nextUpgrade;

            text[2].text += " (-> " + upgrade.range + ")";
            text[3].text += " (-> " + upgrade.projectileSpeed + ")";
            text[4].text += " (-> " + upgrade.pierce + ")";
        }

        return text;
    }
}
