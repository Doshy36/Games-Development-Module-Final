using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public int damage = 1;
    [Min(0.01f)]
    public float fireRate = 1.0f;
    [HideInInspector]
    public bool moving = false;
    [HideInInspector]
    public int price = 0;
    public TowerType type;
    public int upgradeLevel;
    public AudioSource audioSource;
    public GameObject radius;

    private bool isPlaceable = true;
    private float firePerSecond;
    private float lastFire = 0;

    void Awake()
    {
        firePerSecond = 1 / fireRate;
        upgradeLevel = -1;

        radius = Instantiate(GameManager.instance.towerManager.radiusPrefab);
        radius.transform.SetParent(gameObject.transform);
        radius.transform.localPosition = Vector3.zero;
        radius.SetActive(false);
    }

    void Update()
    {
        if (moving)
        {
            HandleMove();
        }
        OnUpdate();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.paused)
        {
            return;
        }

        if (!moving && HandleFiring())
        {
            if (Time.time - lastFire >= firePerSecond)
            {
                lastFire = Time.time;
                Fire();
            }
        }
    }

    protected virtual void OnUpdate() { }

    protected abstract void Spawn();

    public virtual void Upgrade(TowerUpgrade upgradeInfo)
    {
        damage = upgradeInfo.damage;
        fireRate = upgradeInfo.fireRate;
        upgradeLevel++;
    }

    protected abstract void Fire();

    protected abstract bool HandleFiring();

    public virtual void OnOpenUpgradeMenu()
    {
        radius.SetActive(true);
    }

    public virtual void OnCloseUpgradeMenu()
    {
        radius.SetActive(false);
    }

    public virtual List<Text> ShowValueText(TowerInfo info, TowerUpgrade nextUpgrade, Func<Text> createText)
    {
        List<Text> text = new List<Text>();

        text.Add(createText());
        text.Add(createText());

        text[0].text = "Damage: " + damage;
        text[1].text = "Fire Rate: " + fireRate;

        if (nextUpgrade != null)
        {
            text[0].text += " (-> " + nextUpgrade.damage + ")";
            text[1].text += " (-> " + nextUpgrade.fireRate + ")";
        }
        return text;
    }

    protected void HandleMove()
    {
        Collider2D[] colliding = new Collider2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.NameToLayer("Ground");
        GetComponent<BoxCollider2D>().OverlapCollider(filter, colliding);

        bool isColliding = colliding[0] != null;

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.instance.shopManager.StopPlacingTower();

            Destroy(gameObject);
        }
        else if (Input.GetMouseButtonDown(0))
        {

            // Place it if there is no collisions
            if (moving && !isColliding)
            {
                moving = false;

                // Reset just in case
                SetOverlay(Color.white);

                GameManager.instance.shopManager.PurchaseTower(this);
                Spawn();
            }
        }
        else
        {
            Vector3 newPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;

            transform.position = newPosition;

            if (isColliding && isPlaceable)
            {
                isPlaceable = false;

                // Give a red overlay
                SetOverlay(Color.red);
            }
            else if (!isColliding && !isPlaceable)
            {
                isPlaceable = true;

                // Remove the overlay
                SetOverlay(Color.white);
            }
        }
    }

    private void SetOverlay(Color color)
    {
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = color;
        }
    }

}
