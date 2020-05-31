using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatort : MonoBehaviour {


    [SerializeField] private GameObject[] EnemyArrey = default;
    [SerializeField] private Transform[] SpaunPoint = default;
    [SerializeField] private int EnemyNumber = default;


    private Playar player = default;
    private List<GameObject> EnemyList = new List<GameObject>();

    private void Start() {

        for(int i = 0; i < EnemyNumber; i++) {

            CreateEnemy(EnemyArrey[Random.Range(0, EnemyArrey.Length)]);
        }

        player = FindObjectOfType<Playar>();

        player.SetEnemyList(EnemyList);

    }

    public void CreateEnemy(GameObject enemy) {

        EnemyList.Add(Instantiate(
            enemy, SpaunPoint[UnityEngine.Random.Range(0, SpaunPoint.Length)].position,
            Quaternion.identity, gameObject.transform));

    }

    public void SetDeatEnemy(GameObject eneme) {

        EnemyList.Remove(eneme);

        if(EnemyList.Count ==  0) {

            StopAllCoroutines();
            
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel() {

        yield return new WaitForSeconds(3f);

        FindObjectOfType<ChengScene>()?.StartNewScene();
    }


}
