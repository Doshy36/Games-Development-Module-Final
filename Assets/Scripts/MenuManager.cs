using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{

    public GameObject[] levels;
    public string[] levelNames;
    private int level;

    public AudioSource source;

    [Header("Settings")]
    public Text soundButton;
    public Text difficultyButton;
    public AudioMixer mixer;

    void Start()
    {
        Settings.instance.sound = PlayerPrefs.HasKey("sound") ? PlayerPrefs.GetInt("sound") == 1 : true;
        Settings.instance.hardDifficulty = PlayerPrefs.HasKey("difficulty") ? PlayerPrefs.GetInt("difficulty") == 1 : false;

        soundButton.text = Settings.instance.sound ? "Sound: On" : "Sound: Off";
        difficultyButton.text = Settings.instance.hardDifficulty ? "Difficulty: Hard" : "Difficulty: Normal";

        if (Settings.instance.sound)
        {
            mixer.SetFloat("Volume", 20.0F);
        }
        else
        {
            mixer.SetFloat("Volume", -80.0F);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void SetLevel(int i)
    {
        levels[level].SetActive(false);
        levels[i].SetActive(true);

        this.level = i;
    }

    public void ToggleSound()
    {
        int value = PlayerPrefs.HasKey("sound") ? (PlayerPrefs.GetInt("sound") == 1 ? 0 : 1) : 0;

        PlayerPrefs.SetInt("sound", value);
        soundButton.text = value == 1 ? "Sound: On" : "Sound: Off";

        Settings.instance.sound = value == 1;

        if (Settings.instance.sound)
        {
            mixer.SetFloat("Volume", 20.0F);
        }
        else
        {
            mixer.SetFloat("Volume", -80.0F);
        }
    }

    public void ToggleDifficulty()
    {
        int value = PlayerPrefs.HasKey("difficulty") ? (PlayerPrefs.GetInt("difficulty") == 1 ? 0 : 1) : 1;

        PlayerPrefs.SetInt("difficulty", value);
        difficultyButton.text = value == 1 ? "Difficulty: Hard" : "Difficulty: Normal";
        Settings.instance.hardDifficulty = value == 1;
    }

    IEnumerator StartGameCoroutine()
    {
        source.Play();

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelNames[level], LoadSceneMode.Single);
    }

}
