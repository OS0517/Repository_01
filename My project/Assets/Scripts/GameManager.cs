using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEditor;
using UnityEngine.Android;
using NUnit.Framework;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public ProjectileBehavior projectile;
    public Image healthBar;
    public Image staminaBar;

    public TextMeshProUGUI healthCount;
    public TextMeshProUGUI staminaCount;
    public TextMeshProUGUI projectileAngleDisplay;

    public bool paused = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        projectile = GameObject.Find("Projectile").GetComponent<ProjectileBehavior>();

        healthBar = GameObject.Find("healthBar").GetComponent<Image>();
        staminaBar = GameObject.Find("staminaBar").GetComponent <Image>();

        healthCount = GameObject.Find("healthCount").GetComponent<TextMeshProUGUI>();
        staminaCount = GameObject.Find("staminaCount").GetComponent<TextMeshProUGUI>();

    }

// Update is called once per frame
void Update()
    {

        healthCount.text = "HP: " + player.health + "/" + player.maxHealth;
        if (player.health <= 0)
        {
            healthCount.text = "DEAD";
        }
        staminaCount.text = "Stamina: " + player.stamina + "/" + player.maxStamina;

        healthBar.fillAmount = (float)player.health / (float)player.maxHealth;
        staminaBar.fillAmount = (float)player.stamina / (float)player.maxStamina;
    }
}