using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBoss : MonoBehaviour, IEnemy {

    [SerializeField] private float HitPoint = default;
    [SerializeField] private float AttackDamag = default;
    [SerializeField] private float AttackSpeed = default;
    [SerializeField] private float BulledAttackSpeed = default;
    [SerializeField] private float BulledMovSpeed = default;
    [SerializeField] private float MovSpeed = default;
    [SerializeField] private float TimeStateActivity = default;
    [SerializeField] private Rigidbody2D MyRigidbody = default;
    [SerializeField] BulletShooter Weapon = default;

    Vector2 RandomMovDirection = default;
    private PausBar WinBar = default;
    private GameObject player = default;

    private bool touchAttack = default;
    private bool buledAttack = default;

    private int StateNum = default;
    private float tempTimeStateActivity = default;

    private int attackNomber = default;

    private void Start() {

        WinBar = FindObjectOfType<PausBar>();
        
        tempTimeStateActivity = TimeStateActivity;

        attackNomber = 0;

        touchAttack = true;
        buledAttack = true;

        player = FindObjectOfType<Playar>().gameObject;

        SaveParametrs data = new SaveParametrs(HitPoint, MovSpeed, AttackDamag, AttackSpeed, BulledMovSpeed);
        SaveEndLoad.CheckParametrs(ref data, "Boss");

        data.SetParameters(ref HitPoint, ref MovSpeed, ref AttackDamag, ref AttackSpeed, ref BulledMovSpeed);

        ChengState();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }

    private void Update() {

        if(player == null) {
            Destroy(this);
        }

        if(IsLive() == false) {
            DestroiEnemy();
        }

        switch(StateNum) {
            case 1:
                State_1();
                break;
            case 2:
                State_2();
                break;
            case 3:
                break;
            default:
                State_1();
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {

        if(collision.gameObject == player && touchAttack) {

            StartCoroutine(TouchAttack(AttackDamag));
        }
    }

    private void Movement(Vector2 derection, float speed) {

        Vector2 toPisition = Vector2.MoveTowards(transform.position, derection, speed * Time.deltaTime);

        MyRigidbody.MovePosition(toPisition);
    }

    private void State_1() {

        Movement(player.transform.position, MovSpeed * 2);
        

        if(tempTimeStateActivity <= 0) {
            tempTimeStateActivity = TimeStateActivity;
            ChengState();
        }
        else {
            tempTimeStateActivity -= Time.deltaTime;
        }

    }

    private void State_2() {

        if(attackNomber < 3) {

            Attack();

            RandomMovDirection = Vector2.one * Random.Range(0.1f, 0.99f);
        }
        else {

            Movement(RandomMovDirection, MovSpeed);
        }

        if(tempTimeStateActivity <= 0) {
            tempTimeStateActivity = TimeStateActivity;
            attackNomber = 0;
            ChengState();
        }
        else {
            tempTimeStateActivity -= Time.deltaTime;
        }
    }

    private void State_3() {

        if(tempTimeStateActivity <= 0) {
            tempTimeStateActivity = TimeStateActivity;
            ChengState();
        }
        else {
            tempTimeStateActivity -= Time.deltaTime;
        }
    }
    
    public void Attack() {
        
        if(buledAttack) {
            StartCoroutine(ShotBullet_1(AttackDamag));
        }
    }

    private void ChengState() {

        StateNum = Random.Range(1, 3);
    }

    public void DestroiEnemy() {

        FindObjectOfType<EnemyCreatort>().SetDeatEnemy(gameObject);
        WinBar.GameOver("Win");
        Destroy(gameObject);
    }

    public void GetDamag(float damag) {

        HitPoint -= damag;
    }

    public bool IsLive() {

        return HitPoint > 0;
    }

    IEnumerator TouchAttack( float damag) {

        touchAttack = false;

        player.GetComponent<Playar>().GetDamag(damag);

        yield return new WaitForSeconds(AttackSpeed);

        touchAttack = true;
    }

    IEnumerator ShotBullet_1(float damag) {

        buledAttack = false;

        Weapon.Fire(damag, BulledMovSpeed, player.transform.position);

        yield return new WaitForSeconds(BulledAttackSpeed);

        attackNomber++;

        buledAttack = true;

    }

    IEnumerator ShotBullet_2(float damag) {

        buledAttack = false;

        Weapon.Fire(damag, BulledMovSpeed, player.transform.position);

        yield return new WaitForSeconds(BulledAttackSpeed);

        buledAttack = true;

    }

}
