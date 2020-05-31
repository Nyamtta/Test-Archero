using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Playar : MonoBehaviour
{

    [SerializeField] public float HitPoint = default;
    [SerializeField] private float AttackDamag = default;
    [SerializeField] private float AttackSpeed = default;
    [SerializeField] private float BulledSpeed = default;
    [SerializeField] private  float MovSpeed = default;
    [SerializeField] private Rigidbody2D MyRigigbody = default;
    [SerializeField] private BulletShooter Weapon = default;

    private bool AttackActiv = default;
    private JoystickControler MovControler = default; 
    private List<GameObject> EnemyList = new List<GameObject>();
    private PausBar Paus = default;

    private void Start() {

        Paus = FindObjectOfType<PausBar>();
        MovControler = FindObjectOfType<JoystickControler>();
        AttackActiv = true;

        SaveParametrs data = new SaveParametrs(HitPoint, MovSpeed, AttackDamag, AttackSpeed, BulledSpeed);
        
        SaveEndLoad.CheckParametrs(ref data, "Player");

        data.SetParameters(ref HitPoint, ref MovSpeed, ref AttackDamag, ref AttackSpeed, ref BulledSpeed);
    }

    private void Update() {

        if(IsLive()) {

            bool isMov = false;
            Movment(out isMov);

            if(isMov == false && AttackActiv) {

                AttackEnemy(AttackDamag);
            }
            

        }
        else {

            Paus.GameOver("Game Over");
            Destroy(gameObject);
        }

    }

    public void SetEnemyList(List<GameObject> enemys) {

        EnemyList = enemys;
    }

    public void GetDamag(float damag) {

        HitPoint -= damag;

        // перевірка щоб не було відємного зторовя в HitBarUI;
        UpdateHpEvent?.Invoke(((int)HitPoint > 0) ? (int)HitPoint : 0);

    }
   
    public float GetHitPoint() {
        
        return HitPoint;
    }

    private void AttackEnemy(float damag) {

        if(EnemyList.Count != 0)
            StartCoroutine(AttackEn(damag));
    }

    private void Movment(out bool mov) {
        
        MyRigigbody.velocity = new Vector2(
            MovControler.Horizontal() * MovSpeed * Time.deltaTime,
            MovControler.Vertical() * MovSpeed * Time.deltaTime);

        if(MyRigigbody.velocity != Vector2.zero)
            mov = true;
        else {
            mov = false;
        }
    }

    private GameObject GetСlosesEnemi() {

        GameObject temp = EnemyList.First();
       
        foreach(var enemy in EnemyList) {
            if(Vector2.Distance(transform.position, enemy.transform.position) < 
                Vector2.Distance(transform.position, temp.transform.position)) {

                temp = enemy;
            }
        }

        return temp;
    }

    private bool IsLive() {

        return HitPoint > 0;
    }

    IEnumerator AttackEn(float damag) {

        AttackActiv = false;
        
        Weapon.Fire(damag, BulledSpeed, GetСlosesEnemi().transform.position);
        
        yield return new WaitForSeconds(AttackSpeed);


        AttackActiv = true;
    }

    public event UpdateHitPoint UpdateHpEvent;
    public delegate void UpdateHitPoint(int hp);

}
