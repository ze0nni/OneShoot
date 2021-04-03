
using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float BulletForce = 600;
    public Bullet BulletPrefab;
    
    [Space]
    public bl_Joystick Joystick;
    public float JoystickScale = 0.1f;

    public int BulletsCount { get; private set; }

    public bool BulletsOut => BulletsCount == 0 && _timeout <= 0;

    public Action<int> OnBulletsCountChanged;  
    
    private float _yaw;
    private float _pitch;
    
    private float _timeout;

    void Update()
    {
        _yaw = Mathf.Clamp(_yaw + Joystick.Vertical * JoystickScale, 0, 30);
        _pitch = Mathf.Clamp(_pitch + Joystick.Horizontal * JoystickScale, -30, 30);
        var look = Quaternion.Euler(0, _pitch, 0) * Quaternion.Euler(-_yaw, 0, 0);
        
        transform.localRotation = look;

        if (Input.GetMouseButtonDown(0))
        {
            if (BulletsCount > 0)
            {
                BulletsCount -= 1;
                OnBulletsCountChanged?.Invoke(BulletsCount);
                Fire(look * Vector3.forward);

                if (BulletsCount == 0)
                {
                    _timeout = BulletPrefab.LifeTime;
                }
            }
        }
        
        _timeout -= Time.deltaTime;
    }

    private void Fire(Vector3 direction)
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.Rigidbody.AddForce(direction * BulletForce);
    }

    public void SetBullets(int count)
    {
        BulletsCount = count;
        OnBulletsCountChanged?.Invoke(BulletsCount);
    }
}
