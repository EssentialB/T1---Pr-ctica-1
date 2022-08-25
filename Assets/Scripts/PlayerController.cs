using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 20; //Velocidad del jugador
    public float fuerzaSalto = 25;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    bool puedeSaltar = true;

    const int ANIMATION_IDLE = 0;
    const int ANIMATION_RUN = 1;
    const int ANIMATION_JUMP = 2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Iniciando script de player"); //dejar un mensaje en la consola
        rb = GetComponent<Rigidbody2D>(); //Obtener el componente rigidbody
        sr = GetComponent<SpriteRenderer>(); //Obtener el Sprite renderer para cambiar de posicion x al player
        animator = GetComponent<Animator>(); //Obtener Animator para poder cambiar de animacion
    }

    // Update is called once per frame
    void Update()
    {
        //GetKey: mientras presiono la tecla
        //GetKeyUp: cuando soltamos la tecla
        //GetKeyDown: al momento de presionar la tecla

        rb.velocity = new Vector2(0, rb.velocity.y); //La velocidad es 0, el personaje se queda quieto
        ChangeAnimation(ANIMATION_IDLE); //El estado es cero, el personaje no se mueve mientras no tenga condicion
        

        if(Input.GetKeyUp(KeyCode.Space) && puedeSaltar){
            ChangeAnimation(ANIMATION_JUMP);
            Saltar();
            puedeSaltar = false;
        }
        if(Input.GetKey(KeyCode.RightArrow)) //Al presionar la tecla flecha derecha
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y); //Dando velocidad en X y en Y :::: "rb.velocity.y" es la velocidad por defecto de Y
            sr.flipX = false; //Posicion X es false, Plyaer mira hacia la derecha
            ChangeAnimation(ANIMATION_RUN); //Si el estado es 1, el personaje corre
        }
        if(Input.GetKey(KeyCode.LeftArrow)) //al presionar la tecla flecha izquierda
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y); //Dando velocidad en X y en Y :::: "rb.velocity.y" es la velocidad por defecto de Y
            sr.flipX = true; //Posicion X es true, Plyaer mira hacia la izquierda
            ChangeAnimation(ANIMATION_RUN); //Si el estado es 1, el personaje corre
        }
         
    }

    //Metodo para animacion
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);
    }

    //Metodo para saltar
    private void Saltar(){
        rb.AddForce(new Vector2(0,fuerzaSalto), ForceMode2D.Impulse);
    }

    //Colisiones con enemigos
    void OnCollisionEnter2D(Collision2D other){
        puedeSaltar = true; //para que no salte mas de una vez

        if(other.gameObject.tag == "Enemy"){
            Debug.Log("Estas muerto");
        }
        
    }

    
}
