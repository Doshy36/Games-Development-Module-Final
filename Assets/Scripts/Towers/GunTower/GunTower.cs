using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunTower : Tower
{

    public GameObject gunObject;
    public float gunSpeed;
    public float projectileSpeed;
    public float range;
    public int pierce;

    protected override void Spawn()
    {
        Vector3 vectorToTarget = GameManager.instance.level.spawn.position - gunObject.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        gunObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        radius.transform.localScale = Vector3.one * (range / 2);
    }

    protected override void Fire()
    {
        audioSource.Play();
        GameObject spawned = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation, GameManager.instance.level.projectileHolder.transform);
        Projectile projectile = spawned.GetComponent<Projectile>();
        projectile.damage = damage;

        projectile.speed = projectileSpeed;
        projectile.pierce = pierce;
    }

    protected override bool HandleFiring()
    {
        Enemy enemy = GameManager.instance.GetClosestEnemy(transform.position, range);
        if (enemy != null)
        {
            Vector3 vectorToTarget = enemy.transform.position - gunObject.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            float difference = Quaternion.Angle(gunObject.transform.rotation, target);
            gunObject.transform.rotation = Quaternion.Lerp(gunObject.transform.rotation, target, gunSpeed);
            return difference <= 3f;
        }
        return false;
    }

    public override void Upgrade(TowerUpgrade upgradeInfo)
    {
        base.Upgrade(upgradeInfo);

        GunTowerUpgrade upgrade = (GunTowerUpgrade)upgradeInfo;

        range = upgrade.range;
        gunSpeed = upgrade.gunSpeed;
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
        text.Add(createText());

        text[2].text = "Range: " + range;
        text[3].text = "Gun Speed: " + gunSpeed;
        text[4].text = "Pierce: " + pierce;
        text[5].text = "Projectile Speed: " + projectileSpeed;

        if (nextUpgrade != null)
        {
            GunTowerUpgrade upgrade = (GunTowerUpgrade)nextUpgrade;

            text[2].text += " (-> " + upgrade.range + ")";
            text[3].text += " (-> " + upgrade.gunSpeed + ")";
            text[4].text += " (-> " + upgrade.pierce + ")";
            text[5].text += " (-> " + upgrade.projectileSpeed + ")";
        }

        return text;
    }

}