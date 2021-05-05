using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public Transform healthBar;

    public override void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.playerManager.AddCoins(reward);

            Die();
        }
        else
        {
            level = Mathf.CeilToInt(health / (Settings.instance.hardDifficulty ? 2 : 1));
            float percentage = ((float)level / (float)reward);

            healthBar.localPosition = new Vector3(2 * percentage, 0, 0);
            healthBar.localScale = new Vector3(1 - percentage, 1, 1);
        }
    }

}
