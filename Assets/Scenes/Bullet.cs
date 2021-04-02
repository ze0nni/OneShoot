using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeTime = 5;
    public Rigidbody Rigidbody;

    private float _lifeTime;
    
    private void Awake()
    {
        _lifeTime = LifeTime;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}