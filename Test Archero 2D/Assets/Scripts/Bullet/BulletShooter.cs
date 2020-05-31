using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] GameObject BulletPrifab = default;
    [SerializeField] private float DefaultSpeed = default;
    [SerializeField] private float DefaultDamag = default;

    private void Start() {
        
        if(BulletPrifab.GetComponent<IBullet>() == null) {
            Debug.Log("Неправильний пріфаб");
        }
    }

    public void Fire(float damag, Vector3 target) {

        GameObject bullet = Instantiate(BulletPrifab, transform.position, Quaternion.identity, transform);
        bullet.GetComponent<IBullet>().SetDamag(damag);
        bullet.GetComponent<IBullet>().SetDirection(target);
        bullet.GetComponent<IBullet>().SetSpeed(DefaultSpeed);
    }
    
    public void Fire(float damag, float speed, Vector3 target) {

        GameObject bullet = Instantiate(BulletPrifab, transform.position, Quaternion.identity, transform);
        SetParameters(damag, speed, target, ref bullet);

    }
    
    public void Fire(float damag, float speed, Vector3 target, Vector3 instPosition) {

        GameObject bullet = Instantiate(BulletPrifab, instPosition, Quaternion.identity, transform);
        SetParameters(damag, speed, target, ref bullet);
    }
    public void Fire(Vector3 target) {

        GameObject bullet = Instantiate(BulletPrifab, transform.position, Quaternion.identity, transform);
        SetParameters(DefaultDamag, DefaultSpeed, target,ref bullet);
    }

    private void SetParameters(float damag, float speed, Vector3 direction, ref GameObject bull) {
        
        bull.GetComponent<IBullet>().SetDamag(damag);
        bull.GetComponent<IBullet>().SetSpeed(speed);
        bull.GetComponent<IBullet>().SetDirection(direction);
    }

}
