using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    public GameObject canvas;
    public GameObject player;
    [Header("Text Values")]
    public Text healthUIValue;
    public Text shieldUIValue;
    public Text XPUIValue;
    public Text killsUIValues;
    public Text ammoUIValues;
    public Text moneyUIValues;

    private int health;
    private int shield;
    private int XP;
    private int kills;
    private int ammo;
    private float money;

    private int startHealth = 100;
    private int startShield = 50;
    private int startXP = 0;
    private int startKills = 0;
    private int startAmmo = 1000;
    private float startMoney = 200;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(player);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
            Destroy(canvas);
            Destroy(player);
        }
    }

    private void Start()
    {
        ResetStats();
    }

    // Update is called once per frame
    void Update()
    {
        healthUIValue.text = health.ToString();
        shieldUIValue.text = shield.ToString();
        XPUIValue.text = XP.ToString();
        killsUIValues.text = kills.ToString();
        ammoUIValues.text = ammo.ToString();
        moneyUIValues.text = money.ToString();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.health = health;
        data.shield = shield;
        data.ammo = ammo;
        data.XP = XP;
        data.kills = kills;
        data.money = money;

        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            health = data.health;
            shield = data.shield;
            XP = data.XP;
            kills = data.kills;
            ammo = data.ammo;
            money = data.money;

        }
    }
    public void ResetStats()
    {
        health = startHealth;
        shield = startShield;
        ammo = startAmmo;
        XP = startXP;
        kills = startKills;
        money = startMoney;
    }
    public void AddHealth()
    {
        health = health + 5;
        if (health >= 100)
        {
            health = 100;
        }
    }
    public void RemoveHealth()
    {
        health = health - 5;
        if (health <= 0)
        {
            health = 0;
        }
    }
    public void AddShield()
    {
        shield = shield + 5;
        if (shield >= 50)
        {
            shield = 50;
        }
    }
    public void RemoveShield()
    {
        shield = shield - 5;
        if (shield <= 0)
        {
            shield = 0;
        }
    }
    public void AddAmmo()
    {
        ammo = ammo + 50;
       if (ammo >= 10000)
        {
            ammo = 10000;
        }
    }
    public void RemoveAmmo()
    {
        ammo = ammo - 50;
        if (ammo <= 0)
        {
            ammo = 0;
        }
    }
    public void AddXP()
    {
        XP = XP + 100;
    }
    public void RemoveXP()
    {
        XP = XP - 100;
        if (XP <= 0)
        {
            XP = 0;
        }
    }
    public void AddKills()
    {
        kills = kills + 1;
    }
    public void RemoveKills()
    {
        kills = kills - 1;
        if (kills <= 0)
        {
            kills = 0;
        }
    }
    public void AddMoney()
    {
        money = money + 100;
    }
    public void RemoveMoney()
    {
        money = money - 100;
        if (money <= 0)
        {
            money = 0;
        }
    }
    public void GoToScene2()
    {
        SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
    }
    public void GoToScene1()
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }
}
[Serializable]
class PlayerData
{
    public int health;
    public int shield;
    public int XP;
    public int kills;
    public int ammo;
    public float money;
}
