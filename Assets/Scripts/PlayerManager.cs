using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public GameManager gameManager;
    public int coins;
    public int health;
    public Text healthAmount;
    public Text gameOverText;
    public Text victoryText;

    void Start()
    {
        coins = gameManager.startingCoins;
        gameManager.coinText.text = coins + "";

        health = gameManager.startingHealth;
        healthAmount.text = health + "";

        gameManager.shopManager.UpdateButtonColors();
    }

    void Update()
    {
        if (gameOverText.enabled || victoryText.enabled)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(gameManager.level.levelName, LoadSceneMode.Single);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
            }
        }
    }

    public void Damage(int damage)
    {
        if (gameManager.infiniteHealth)
        {
            return;
        }

        health = Mathf.Max(0, health - damage);

        healthAmount.text = health + "";

        if (health <= 0)
        {
            gameManager.Pause();

            gameManager.gameOverSource.Play();
            gameOverText.enabled = true;
        }
    }

    public void AddCoins(int coins)
    {
        SetCoins(this.coins + coins);
    }

    public bool UseCoins(int coins)
    {
        if (!HasCoins(coins))
        {
            return false;
        }

        if (!gameManager.infiniteMoney)
        {
            SetCoins(this.coins - coins);
        }
        return true;
    }

    public bool HasCoins(int coins)
    {
        if (coins <= this.coins)
        {
            return true;
        }
        return gameManager.infiniteMoney;
    }

    private void SetCoins(int coins)
    {
        this.coins = coins;

        gameManager.coinText.text = coins + "";
        gameManager.shopManager.UpdateButtonColors();
    }
}
