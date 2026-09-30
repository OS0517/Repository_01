using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public Image healthBar;

    public TextMeshProUGUI healthCount;
    public TextMeshProUGUI staminaCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthBar = GameObject.Find("healthBar").GetComponent<Image>();
        healthCount = GameObject.Find("healthCount").GetComponent<TextMeshProUGUI>();
        staminaCount = GameObject.Find("staminaCount").GetComponent <TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)player.health / (float)player.maxHealth;

        healthCount.text = "HP: " + player.health + "/" + player.maxHealth;
        if (player.health <=0)
        {
            healthCount.text = "DEAD";
        }
        staminaCount.text = "Stamina: " + player.stamina + "/" + player.maxStamina;
    }
}
