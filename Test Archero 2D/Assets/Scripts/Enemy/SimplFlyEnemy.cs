using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplFlyEnemy : MonoBehaviour, IEnemy
{

    [SerializeField] private float HitPoint = default;
    [SerializeField] private float AttackDamag = default;
    [SerializeField] private float MovSpeed = default;
    [SerializeField] private float BulledMovSpeed = default;
    [SerializeField] private float AttackSpeed = default;
    [SerializeField] private Animator MyAnimator = default;
    [SerializeField] private Collider2D MapCollider = default;
    [SerializeField] private BulletShooter Weapon = default;

    private GameObject player = default;
    private Vector2 NextPosition = default;
    private bool AttackActiv = default;

    private void Start() {

        MapCollider = GameObject.FindGameObjectWithTag("Map")?.GetComponent<Collider2D>();
        player = GameObject.FindObjectOfType<Playar>()?.gameObject;
        
        AttackActiv = true;

        GetNextposition();

        SaveParametrs data = new SaveParametrs(HitPoint, MovSpeed, AttackDamag, AttackSpeed, BulledMovSpeed);
        
        SaveEndLoad.CheckParametrs(ref data, "FlyEnemy");
        
        data.SetParameters(ref HitPoint, ref MovSpeed, ref AttackDamag, ref AttackSpeed, ref BulledMovSpeed);
    }

    private void Update() {
        
        if(IsLive() && AttackActiv) {

            Movment(NextPosition);

            CheckAttack();
        }
        else if(IsLive() == false) {

            DestroiEnemy();
        }

    }



    private Vector2 GetNextposition() {

        NextPosition = new Vector2(
            Random.Range(MapCollider.bounds.min.x, MapCollider.bounds.max.x),
            Random.Range(MapCollider.bounds.min.y, MapCollider.bounds.max.y));

        return NextPosition;

    }

    private void Movment(Vector2 to) {

        transform.position = Vector2.MoveTowards(transform.position, to, MovSpeed * Time.deltaTime);
    }

    private void CheckAttack() {

        if((Vector2)transform.position == NextPosition) {

            Attack();
        }

    }

    public void Attack() {

        MyAnimator.SetTrigger("Attack");

        StartCoroutine(AttackPlayar(AttackDamag));

    }

    public void GetDamag(float damag) {

        HitPoint -= damag;
    }

    public bool IsLive() {

        return HitPoint > 0;

    }

    // активація з анімації.
    public void ActivMov() {

        AttackActiv = true;
    }

    public void DestroiEnemy() {

        FindObjectOfType<EnemyCreatort>().SetDeatEnemy(gameObject);
        Destroy(gameObject);
    }

    IEnumerator AttackPlayar( float damag) {

        AttackActiv = false;

        yield return new WaitForSeconds(AttackSpeed);

        Weapon.Fire(damag, BulledMovSpeed, player.transform.position);
        
        GetNextposition();
    }

}
