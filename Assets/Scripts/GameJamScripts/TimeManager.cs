using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Timer https://www.youtube.com/watch?v=HmHPJL-OcQE 

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField]
    float startTime = 8;

    [SerializeField]
    float endTime = 20;

    [SerializeField]
    float roombaSpawn = 9;

    [SerializeField]
    float dogSpawn = 12;

    [SerializeField]
    float hour = 60; 

    [SerializeField]
    int timeMultiplyer = 4;

    [SerializeField]
    TextMeshProUGUI timerText;

    private float time;
    private float gameTime;

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
        time = startTime * hour;
        gameTime = endTime * hour;
    }



    void Update()
    {
        if (time < gameTime)
        {
            time += Time.deltaTime * timeMultiplyer;
        }
        else
        {
            time = gameTime;
        }

        displayTime(time);
    }



    void displayTime(float timeToDisplay)
    {
        if (timeToDisplay > gameTime)
        {
            timeToDisplay = gameTime;
        }

        float hours = Mathf.FloorToInt(timeToDisplay / hour);
        float minutes = Mathf.FloorToInt(timeToDisplay % hour);

        if (hours == 20) 
        { 
            GameManager.Instance.TimesUp();
        }

        timerText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }



    public bool CompareTimeInHoursHasPassed(int hour)
    {
        if (ConvertTimeToHours(time) >= hour)
        {

            return true;
        }

        return false;
    }



    public int GetRoombaAwakeTimeHours()
    {
        return Mathf.FloorToInt(roombaSpawn);
    }



    public int GetDogAwakeTimeHours()
    {
        return Mathf.FloorToInt(dogSpawn);
    }



    private int ConvertTimeToHours(float sec)
    {
        return Mathf.FloorToInt(sec / hour);
    }
    
}
