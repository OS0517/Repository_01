using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEditor;
using UnityEngine.Android;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public Image healthBar;
    public Image staminaBar;

    public TextMeshProUGUI healthCount;
    public TextMeshProUGUI staminaCount;

    public GameObject pauseMenu;

    public bool paused = false;
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {


            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            pauseMenu = GameObject.FindGameObjectWithTag("Pause");
            pauseMenu.SetActive(false);

        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        healthBar = GameObject.Find("healthBar").GetComponent<Image>();
        staminaBar = GameObject.Find("staminaBar").GetComponent<Image>();

        healthCount = GameObject.Find("healthCount").GetComponent<TextMeshProUGUI>();
        staminaCount = GameObject.Find("staminaCount").GetComponent<TextMeshProUGUI>();


    }

// Update is called once per frame
void Update()
    {

        healthBar.fillAmount = (float)player.health / (float)player.maxHealth;
        staminaBar.fillAmount = (float)player.stamina / (float)player.maxStamina;

        healthCount.text = "HP: " + (int)player.health + "/" + (int)player.maxHealth;
        if (player.health <= 0)
        {
            healthCount.text = "DEAD";
        }
        staminaCount.text = "Stamina: " + player.stamina + "/" + player.maxStamina;
		
		healthBar.fillAmount = (float)player.health / (float)player.maxHealth;
		
        staminaCount.text = "Stamina: " + (int)player.stamina + "/" + (int)player.maxStamina;

    }
    public void Pause()
    {
        paused = !paused;

        pauseMenu.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void LoadLevel(int levelID)
    {
        if (levelID >= SceneManager.sceneCountInBuildSettings)
            Debug.Log("Level ID is too high: " + levelID);
        else
            SceneManager.LoadScene(levelID);
    }
    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MainMenu()
    {
        LoadLevel(0);
    }
    public void Quit()
    {
        Application.Quit();

    }
}