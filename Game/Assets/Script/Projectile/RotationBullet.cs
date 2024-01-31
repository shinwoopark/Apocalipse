using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBullet : MonoBehaviour
{
    private float _rotation = 500;
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotation) * Time.deltaTime);
    }
}
