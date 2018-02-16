using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float currentHealthPoints;
    [SerializeField] int maxHealthPoints = 100;

	// Use this for initialization
	void Start () {
        currentHealthPoints = maxHealthPoints;
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
		
	}
}
