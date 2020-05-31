using System.Collections;
using System.Collections.Generic;

public interface IEnemy 
{

    void DestroiEnemy();

    void GetDamag(float damag);

    void Attack();

    bool IsLive();


}
