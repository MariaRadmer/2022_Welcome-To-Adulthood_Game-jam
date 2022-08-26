using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    GameObject baby;

    [SerializeField]
    GameObject roomba;

    [SerializeField]
    GameObject dog;

    [SerializeField]
    int maxPoops;

    float poopCounter;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }

    }

    public void Play()
    {
        SceneManager.LoadScene("WorldScene");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        print("quit");
        Application.Quit();
    }

    public void Lose() 
    {
        SceneManager.LoadScene("Lose");
    }

    public void TimesUp()
    {
        SceneManager.LoadScene(poopCounter <= maxPoops ? "Win": "Lose_Dirty");
    }


    public void IncreasePoopCounter() { poopCounter++; }
    public void DecreasePoopCounter() { poopCounter--; }

    public GameObject GetRoomba() { return roomba; }

}
