using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootingRange : MonoBehaviour
{
    //Target Health
    public float health;
    public Text targetHealth;
    private new GameObject gameObject;

    //New Level
    public int iLevelToLoad;
    public string sLevelToLoad;
    public bool useIntegerToLoadLEvel;

    void Start()
    {
        gameObject = GameObject.FindGameObjectWithTag("Polygon");
    }
   
    void Update()
    {
        targetHealth.text = health.ToString();

        if (health <= 0)
        {
            Debug.Log("Target Destroyed");
            LoadScene();
            gameObject.SetActive(false);
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(true);
            health = 100;
        }
    }
    void LoadScene()
    {
        if (useIntegerToLoadLEvel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
    }
}
