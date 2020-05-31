using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerOnStartLevel : MonoBehaviour
{

    [SerializeField] private GameObject pfPlayer = default; 
    [SerializeField] private Transform PlayerPosition = default;

    private void Awake() {

        GameObject temp = Instantiate(pfPlayer, PlayerPosition.position, Quaternion.identity);

    }

}
