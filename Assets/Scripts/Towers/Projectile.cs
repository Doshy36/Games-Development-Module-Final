using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public int damage;
    public int pierce = 1;
    public Func<Vector3> direction;

    void Start()
    {
        OnSpawn();
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    public virtual void Hit(Enemy enemy)
    {
        enemy.Damage(damage);

        if (--pierce <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnSpawn()
    {
        direction = () =>
        {
            return transform.up;
        };
    }

    protected virtual void OnFixedUpdate()
    {
        if (GameManager.instance.paused)
        {
            return;
        }

        transform.position += direction() * Time.deltaTime * speed;

        if (!gameObject.GetComponent<Renderer>().IsVisibleFrom(GameManager.instance.mainCamera))
        {
            Destroy(gameObject);
        }
    }
}
