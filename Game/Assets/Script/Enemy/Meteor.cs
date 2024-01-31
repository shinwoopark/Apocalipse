using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 10f;

    public GameObject EsplodeFX;

    [SerializeField]
    private float _lifeTime = 3f;
    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, -MoveSpeed * Time.deltaTime, 0f));
    }
}
