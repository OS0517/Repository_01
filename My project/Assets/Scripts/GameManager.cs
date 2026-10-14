using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEditor;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public Image healthBar;
    public Image staminaBar;
    public Image sprintStatus;

    public GameObject pauseMenu;

    public TextMeshProUGUI healthCount;
    public TextMeshProUGUI staminaCount;

    public bool paused = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


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
        staminaCount.text = "Stamina: " + (int)player.stamina + "/" + (int)player.maxStamina;
    }

    public void Pause()
    {
        paused = !paused;

        pauseMenu.SetActive(paused);

        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1f;
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