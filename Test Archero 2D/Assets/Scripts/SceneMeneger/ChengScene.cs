using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChengScene : MonoBehaviour
{

    [SerializeField] private int SceneIndexs = default;

    public void StartNewScene() {

        SceneManager.LoadScene(SceneIndexs);
    }

}
