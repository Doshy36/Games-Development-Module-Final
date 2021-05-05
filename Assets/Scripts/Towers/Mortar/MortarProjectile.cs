using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : Projectile
{

    public LayerMask searchLayer;
    public float range;
    public Vector3 start;
    public Vector3 midPoint;
    public Vector3 target;
    public AudioSource explosionSource;

    private float count;

    protected override void OnSpawn()
    {
        start = transform.position;
        midPoint = start + (target - start) / 2 + Vector3.up * 100.0f;
    }

    protected override void OnFixedUpdate()
    {
        if (GameManager.instance.paused)
        {
            return;
        }

        if (count < 1.0F)
        {
            count += speed * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(start, midPoint, count);
            Vector3 m2 = Vector3.Lerp(midPoint, target, count);
            gameObject.transform.position = Vector3.Lerp(m1, m2, count);
        }
        else
        {
            GameManager.instance.getNearbyEnemies(transform.position, range * 2).ForEach(enemy =>
            {
                Hit(enemy);
            });

            StartCoroutine(PlaySoundAndDestroy());

            Destroy(gameObject);
        }
    }

    public override void Hit(Enemy enemy)
    {
        enemy.Damage(damage);
    }

    private IEnumerator PlaySoundAndDestroy()
    {
        explosionSource.gameObject.transform.SetParent(GameManager.instance.transform);
        explosionSource.Play();

        while (explosionSource.isPlaying)
        {
            yield return null;
        }

        Destroy(explosionSource.gameObject);
    }
}
