using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private SpriteRenderer sr;
	private Animator _animator;
	private Rigidbody2D rb2d;


	//escalera
	private bool puedeSubirEscalera = false;


	public float speed = 5;
	public float upSpeed = 25;
	private bool puedoSaltar = true;
	private int contadorSaltos = 0;




	//vida
	public Text vidaTexto;
	public int vidaScore = 3;

	//score
	public Text scoreTexto;
	public int Score = 0;

	//Balas
	public GameObject rightKunai; //bala derecha
	public GameObject leftKunai; // bala izquierda

	//Volar
	private bool puedoVolar = false;
	private float gravity = 1f;


	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

		scoreTexto.text = "PUNTAJE: " + Score;
		vidaTexto.text = "VIDAS: " + vidaScore;

		setIdleAnimation();

		//correr normal
		if (Input.GetKey(KeyCode.RightArrow))
		{
			sr.flipX = false;
			setRumAnimation();
			rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			sr.flipX = true;
			setRumAnimation();
			rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
		}


		//salto
		if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
		{
			setJumpAnimation();
			rb2d.velocity = Vector2.up * upSpeed;

			if (contadorSaltos == 0 && puedoSaltar == true)
			{
				puedoSaltar = true;
				contadorSaltos++;
			}
			else
			{
				puedoSaltar = false;
			}

		}

		//disparar
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (!sr.flipX)
			{
				var position = new Vector2(transform.position.x + 1, transform.position.y);
				Instantiate(rightKunai, position, rightKunai.transform.rotation);
			}
			/*else
			{
				var position = new Vector2(transform.position.x - 2, transform.position.y);
				Instantiate(leftKunai, position, leftKunai.transform.rotation);
			}*/

		}



		//escalera
		if (puedeSubirEscalera && Input.GetKey(KeyCode.UpArrow))
		{

			setClimbAnimation();
			rb2d.gravityScale = 0;
			rb2d.velocity = Vector2.up * (speed - 2);
			DeshabilitarColisionSuelo();
		}

		if (puedeSubirEscalera && Input.GetKey(KeyCode.DownArrow))
		{
			setClimbAnimation();
			rb2d.gravityScale = 10;
			rb2d.velocity = Vector2.down * (speed - 2);
			DeshabilitarColisionSuelo();
		}


		//deslisarse
		if (Input.GetKey(KeyCode.C))
		{
			//sr.flipX = false;

			if (!sr.flipX)
			{
				setSlideAnimation();

				rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

			}
			else
			{
				setSlideAnimation();

				rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
			}
		}

		//vida
		if (vidaScore == 0)
		{
			setDeadAnimation();

		}

		//volar

		if (puedoVolar == true)
		{

			setGildeAnimation();

			rb2d.gravityScale = gravity;

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				rb2d.velocity = new Vector2(rb2d.velocity.x, -speed);
			}
		}

		//bajar nube
		if (Input.GetKeyDown(KeyCode.V) && puedoVolar)
		{
			setJumpAnimation();
			rb2d.gravityScale = 10;
			puedoVolar = false;
		}

	}


	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Suelo")
		{
			puedoSaltar = true;
			contadorSaltos = 0;
		}
		//enemy
		if (other.gameObject.tag == "Enemy")
		{
			vidaScore--;
		}

	}

	//suelo
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Escalera")
		{
			puedeSubirEscalera = true;
			rb2d.gravityScale = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Escalera")
		{
			puedeSubirEscalera = false;
			rb2d.gravityScale = 10;
			HabilitarColisionSuelo();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "ChocaVuela")
		{
			puedoVolar = true;
		}
	}



	//Aumentapuntaje
	public void Incrementar()
	{
		Score += 10;

	}


	//desabilitar colicion suelo
	private void DeshabilitarColisionSuelo()
	{
		Physics2D.IgnoreLayerCollision(3, 8, true);
	}

	private void HabilitarColisionSuelo()
	{
		Physics2D.IgnoreLayerCollision(3, 8, false);
	}



	private void setIdleAnimation()
	{
		_animator.SetInteger("Estado", 0);
	}

	private void setJumpAnimation()
	{
		_animator.SetInteger("Estado", 1);
	}

	private void setRumAnimation()
	{
		_animator.SetInteger("Estado", 2);
	}

	private void setSlideAnimation()
	{
		_animator.SetInteger("Estado", 3);
	}


	private void setGildeAnimation()
	{
		_animator.SetInteger("Estado", 4);
	}

	private void setClimbAnimation()
	{
		_animator.SetInteger("Estado", 5);
	}


	private void setDeadAnimation()
	{
		_animator.SetInteger("Estado", 6);
	}

}
