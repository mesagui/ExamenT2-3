using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject redHatBoyPlayer;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		MovientoCamaraPlayer();
	}

	public void MovientoCamaraPlayer()
	{
		var x = redHatBoyPlayer.transform.position.x + 5;

		transform.position = new Vector3(x, 0, transform.position.z);
	}
}
