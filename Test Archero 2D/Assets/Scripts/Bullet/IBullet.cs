using UnityEngine;

public interface IBullet
{
    void SetDamag(float damag);
    void SetSpeed(float speed);
    void SetDirection(Vector2 direction);
    void DestroiBullet();
}
