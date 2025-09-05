using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private int valueLevel;
    [SerializeField] private int incomeLevel;

    public List<float> valuePerLevel;
    public List<int> incomePerLevel;

    public int multiplierValue;

    public float incomePercentage;
    public float moneyStackMod;


    private void Awake()
    {
        instance = this;

        valueLevel = PlayerPrefs.GetInt("Value_Level", 1);
        incomeLevel = PlayerPrefs.GetInt("Income_Level", 1);

        // Safe initialization with null checks
        if (valuePerLevel != null && valuePerLevel.Count > 0 && valueLevel > 0)
        {
            moneyStackMod = valuePerLevel[Mathf.Min(valueLevel - 1, valuePerLevel.Count - 1)];
        }
        else
        {
            moneyStackMod = 1.0f; // Default value
        }

        if (incomePerLevel != null && incomePerLevel.Count > 0 && incomeLevel > 0)
        {
            incomePercentage = incomePerLevel[Mathf.Min(incomeLevel - 1, incomePerLevel.Count - 1)];
        }
        else
        {
            incomePercentage = 100; // Default value
        }
    }

    private void Start()
    {
    }

    void Update()
    {
    }

    public void RestartGame()
    {
        // Reset PlayerPrefs data
        PlayerPrefs.DeleteAll();

        // Reset runtime values too (optional but safer)
        GameManager.totalGemAmount = 0f;
        GameManager.instance.levelNo = 0;

        // Reset upgrade levels
        valueLevel = 1;
        incomeLevel = 1;
        
        // Safe reset with null checks
        if (valuePerLevel != null && valuePerLevel.Count > 0)
        {
            moneyStackMod = valuePerLevel[0];
        }
        else
        {
            moneyStackMod = 1.0f;
        }
        
        if (incomePerLevel != null && incomePerLevel.Count > 0)
        {
            incomePercentage = incomePerLevel[0];
        }
        else
        {
            incomePercentage = 100;
        }

        // Save defaults back
        PlayerPrefs.SetInt("Value_Level", valueLevel);
        PlayerPrefs.SetInt("Income_Level", incomeLevel);
        PlayerPrefs.SetInt("Level_Number", GameManager.instance.levelNo);
        PlayerPrefs.SetFloat("Total_Gem", GameManager.totalGemAmount);

        PlayerPrefs.Save();

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("Game Restarted");
    }

    public void ContinueGame()
    {
        GameManager.instance.levelNo++;
        GameManager.totalGemAmount += GameManager.instance.currentGemCollected * multiplierValue;
        if (GameManager.instance.levelNo > GameManager.instance.dataLevels.Count - 1)
        {
            GameManager.instance.levelNo = 0;
        }
        PlayerPrefs.SetInt("Level_Number", GameManager.instance.levelNo);
        PlayerPrefs.SetFloat("Total_Gem", GameManager.totalGemAmount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ContinueWithoutMultiplierButton()
    {
        GameManager.instance.levelNo++;
        GameManager.totalGemAmount += GameManager.instance.currentGemCollected * multiplierValue;
        if (GameManager.instance.levelNo > GameManager.instance.dataLevels.Count - 1)
        {
            GameManager.instance.levelNo = 0;
        }
        PlayerPrefs.SetInt("Level_Number", GameManager.instance.levelNo);
        PlayerPrefs.SetFloat("Total_Gem", GameManager.totalGemAmount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
