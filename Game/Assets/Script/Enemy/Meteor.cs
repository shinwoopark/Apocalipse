using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public SoundManager SoundManager;
    public GameObject SoundManager_gb;

    [SerializeField]
    private float MoveSpeed = 10f;

    public GameObject EsplodeFX;

    [SerializeField]
    private float _lifeTime = 3f;
    void Start()
    {
        SoundManager_gb = GameObject.Find("Managers");
        SoundManager = SoundManager_gb.GetComponent<SoundManager>();
        SoundManager.PlaySFX(16);
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, -MoveSpeed * Time.deltaTime, 0f));
    }
}
