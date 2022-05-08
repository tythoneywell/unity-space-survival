using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSurvival : MonoBehaviour
{
    public static PlayerSurvival main;

    public float hunger;
    public float foodQuality;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        hunger = 0.8f;
    }

    void Update()
    {
        hunger -= Time.deltaTime / 300;
        hunger = Mathf.Clamp01(hunger);
        if (hunger == 0 || ShipSystemController.main.oxygenAmount == 0)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    public void Eat(ItemData item)
    {
        hunger += (float)item.hungerRestoreAmount / 10;
        hunger = Mathf.Clamp01(hunger);
        PlayerMovement.main.foodSprintBoost = item.foodSprintBoost;
    }
}
