
using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float BulletForce = 600;
    public Bullet BulletPrefab;

    [NonSerialized] public int BulletsCount;
    [NonSerialized] public float Timeout;

    public bool BulletsOut => BulletsCount == 0 && Timeout <= 0;
    
    private float _yaw;
    private float _pitch;

    void Update()
    {
        _yaw = Mathf.Clamp(_yaw + Input.GetAxis("Mouse Y"), 0, 30);
        _pitch = Mathf.Clamp(_pitch + Input.GetAxis("Mouse X"), -30, 30);
        var look = Quaternion.Euler(0, _pitch, 0) * Quaternion.Euler(-_yaw, 0, 0);
        
        transform.localRotation = look;

        if (Input.GetMouseButtonDown(0))
        {
            if (BulletsCount > 0)
            {
                BulletsCount -= 1;
                Fire(look * Vector3.forward);

                if (BulletsCount == 0)
                {
                    Timeout = 3;
                }
            }
        }
        
        Timeout -= Time.deltaTime;
    }

    private void Fire(Vector3 direction)
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.Rigidbody.AddForce(direction * BulletForce);
    }
}
