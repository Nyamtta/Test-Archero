using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SimplEnemy : MonoBehaviour, IEnemy
{

    [SerializeField] private float HitPoint = default;
    [SerializeField] private float AttackDamag = default;
    [SerializeField] private float MoveSpeed = default;
    [SerializeField] private float AttackDistans = default;
    [SerializeField] private float AttackSpeed = default;
    [SerializeField] private Rigidbody2D MyRigidbody = default;
    [SerializeField] private Animator MyAnimator = default;

    private GameObject player = default;
    private bool AttackActiv = default;

    private void Start() {

        AttackActiv = true;
        player = GameObject.FindObjectOfType<Playar>()?.gameObject;

        SaveParametrs data = new SaveParametrs(HitPoint, MoveSpeed, AttackDamag, AttackSpeed);
        
        SaveEndLoad.CheckParametrs(ref data, "SimplEnemy");

        float temp = 0;

        data.SetParameters(ref HitPoint, ref MoveSpeed, ref AttackDamag, ref AttackSpeed, ref temp);
    }


    private void Update() {

        if(player == null) {

            Destroy(this);
        }

        if(IsLive()) {

            if(IsAtackDistans()) {

                Attack();
            }
            else {
                
                Movement(player.transform.position);
            }
        }
        else {

            DestroiEnemy();
        }

    }

    private void Movement(Vector2 derection) {

        // Роблю через rb щоб можна будо відштовхувати гравцем ворога і ворог бачив усі перешкоди
        Vector2 toPisition = Vector2.MoveTowards(transform.position, derection, MoveSpeed * Time.deltaTime);

        MyRigidbody.MovePosition(toPisition);
    }

    public void Attack() {

        if(AttackActiv) {

            StartCoroutine(AttackPlayar(AttackDamag));
        }

    }

    public void GetDamag(float damag) {

        HitPoint -= damag;
    }

    public bool IsLive() {

        return HitPoint > 0;

    }

    private bool IsAtackDistans() {

        float distans = Vector2.Distance(transform.position, player.transform.position);

        if((distans <= AttackDistans)) {

            return true;
        }

        return false;
    }

    public void DestroiEnemy() {

        FindObjectOfType<EnemyCreatort>().SetDeatEnemy(gameObject);
        Destroy(gameObject);
    }

    IEnumerator AttackPlayar(float damag) {
        
        AttackActiv = false;

        MyAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(AttackSpeed);

        player.GetComponent<Playar>()?.GetDamag(damag);
        
        AttackActiv = true;
    }


    /*
             
    Замітка з поясненням:
        
    Звичайно можна булоб використовувати функцію OnCollision, 
    але зе зробило б скріат вузьконаправленим і його булом вашко пристосувати то іншого типу вохожмх ворогів
    наприклад: меч, алібарда і тому подібне у яких переміщення одинакові але дистанція спрацювання атаки різна,
    тому вирішив перевіряти через дистанцію хоч і через це потрібно робити перевірку кожен кадр.

    ще є варіант поєднання  OnCollision з OnTriger і 2 калайдерами ріхних розмірів.

    ну і накінець мій варіант підхотить якшо на пкрсонажі колайдер є трігером а рух виконується за допомогою Pasfainder;

     */

}
