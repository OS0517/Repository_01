using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public Image healthBar;

    public TextMeshProUGUI time;
    public TextMeshProUGUI charge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthBar = GameObject.Find("healthBar").GetComponent<Image>();

        charge = GameObject.Find("Charge").GetComponent <TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)player.health / (float)player.maxHealth;

    }
}
