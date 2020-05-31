using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveParametrs
{

    public float HItPoint;
    public float MovSpeed;
    public float AttackDamag;
    public float AttackSpeed;
    public float BulledSpeed;

    public SaveParametrs(float hp, float movspeed, float attackDamag, float attackSpeed, float bulledSpeed = 0) {

        HItPoint = hp;
        MovSpeed = movspeed;
        AttackDamag = attackDamag;
        AttackSpeed = attackSpeed;
        BulledSpeed = bulledSpeed;
    }

    public void SetParameters(ref float hp, ref float movspeed, 
        ref float attackDamag, ref float attackSpeed, ref float bulledSpeed) {

        hp = HItPoint;
        movspeed = MovSpeed;
        attackDamag = AttackDamag;
        attackSpeed = AttackSpeed;
        bulledSpeed = BulledSpeed;

    }

}
