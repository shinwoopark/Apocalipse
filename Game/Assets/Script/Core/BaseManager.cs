using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected GameManager _gameManager;

    public GameManager GameManager {  get { return _gameManager; } }

    public virtual void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}
