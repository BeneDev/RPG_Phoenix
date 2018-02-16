using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

    private float currentHealthPoints;
    [SerializeField] int maxHealthPoints = 100;
    [SerializeField] float chaseRadius = 5f;
    private GameObject player;
    private AICharacterControl aiController;

	// Use this for initialization
	void Start () {
        currentHealthPoints = maxHealthPoints;
        player = GameObject.FindGameObjectWithTag("Player");
        aiController = GetComponent<AICharacterControl>();
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints -= damage;
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }
	
	// Update is called once per frame
	void Update () {
        var toPlayer = player.transform.position - transform.position;
        if (toPlayer.magnitude <= chaseRadius)
        {
            aiController.target = player.transform;
        }
        else
        {
            aiController.target = null;
        }
	}
}
