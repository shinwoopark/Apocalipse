using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFuelSystem : MonoBehaviour
{
    public float Fuel = 100f;
    public float MaxFuel = 100f;
    public float FuelDecreaseSpeed = 2f;

    void Start()
    {
        Fuel = GameInstance.instance.CurrentPlayerFuel;
    }

    public void InitFuel()
    {
        Fuel = MaxFuel;
        GameInstance.instance.CurrentPlayerFuel = Fuel;
    }

    void Update()
    {
        if (GameManager.Instance.bStageCleared) return;

        Fuel -= FuelDecreaseSpeed * Time.deltaTime;
        GameInstance.instance.CurrentPlayerFuel = Fuel;

        if (Fuel <= 0f)
        {
            GameManager.Instance.PlayerCharacter.DeadProcess();
        }
    }
}

