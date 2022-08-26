using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// SOURCE:https://www.youtube.com/watch?v=Oadq-IrOazg

public class LevelChanger : MonoBehaviour
{

    [SerializeField] Animator animator;

    public static LevelChanger Instance;

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


    public void FadeInWorld()
    {
        SceneManager.LoadScene("WorldScene");
    }


    public void FadeOutWorld()
    {
        animator.SetTrigger("GoToBed");

    }

    public void FadeOutComplete()
    {
        SceneManager.LoadScene("Credits");
    }
}
