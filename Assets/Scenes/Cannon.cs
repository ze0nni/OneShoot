
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
    private Quaternion _look;
    
    private float _timeout;

    void Update()
    {
        _yaw = Mathf.Clamp(_yaw + Joystick.Vertical * JoystickScale, 0, 30);
        _pitch = Mathf.Clamp(_pitch + Joystick.Horizontal * JoystickScale, -30, 30);
        _look = Quaternion.Euler(0, _pitch, 0) * Quaternion.Euler(-_yaw, 0, 0);
        
        transform.localRotation = _look;
        _timeout -= Time.deltaTime;
    }

    public void Fire()
    {
        if (BulletsCount <= 0)
            return;

        BulletsCount -= 1;
        OnBulletsCountChanged?.Invoke(BulletsCount);


        var bullet = Instantiate(BulletPrefab);
        bullet.Rigidbody.AddForce(_look * Vector3.forward * BulletForce);

        if (BulletsCount == 0)
        {
            _timeout = BulletPrefab.LifeTime;
        }
            
    }

    public void SetBullets(int count)
    {
        BulletsCount = count;
        OnBulletsCountChanged?.Invoke(BulletsCount);
    }
}
