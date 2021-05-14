using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigosController : MonoBehaviour
{
	public GameObject enemy;

	public float timepoGeneration = 2;

	void Start()
	{
		InvokeRepeating("CreateEnemy", 0, timepoGeneration);
	}


	void Update()
	{

	}

	void CreateEnemy()
	{
		Instantiate(enemy, transform.position, Quaternion.identity);
	}



}
