using UnityEngine;

public class ImpulsoBola : MonoBehaviour
{
    [Header("Configuración del Impulso")]
    public float fuerzaMinima = 5f;
    public float fuerzaMaxima = 20f; // He bajado esto un poco, 120 es muchísimo para ForceMode.Impulse
    public float velocidadCarga = 15f;

    [Header("Visualización")]
    [SerializeField] private float fuerzaActual;
    [SerializeField] private bool cargando;
    [SerializeField] private float ultimaDireccionX = 1f; // Empieza mirando a la derecha por defecto

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fuerzaActual = fuerzaMinima;
    }

    void Update()
    {
        // --- 1. DETECTAR LA DIRECCIÓN ---
        // Leemos lo que pulsa el jugador
        float inputX = Input.GetAxisRaw("Horizontal");

        // Si el jugador está pulsando algo, actualizamos la "última dirección"
        if (inputX != 0)
        {
            ultimaDireccionX = inputX;
        }
        // Si no pulsa nada, pero la bola se mueve rápido, usamos la dirección del movimiento
        else if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            // Mathf.Sign devuelve 1 si es positivo, -1 si es negativo
            ultimaDireccionX = Mathf.Sign(rb.linearVelocity.x);
        }

        // --- 2. LÓGICA DE CARGA (Igual que antes) ---
        if (Input.GetKeyDown(KeyCode.I))
        {
            cargando = true;
            fuerzaActual = fuerzaMinima;
        }

        if (Input.GetKey(KeyCode.I) && cargando)
        {
            fuerzaActual += velocidadCarga * Time.deltaTime;
            if (fuerzaActual > fuerzaMaxima) fuerzaActual = fuerzaMaxima;
        }

        // --- 3. LANZAMIENTO ---
        if (Input.GetKeyUp(KeyCode.I) && cargando)
        {
            EjecutarImpulso();
            cargando = false;
            fuerzaActual = fuerzaMinima;
        }
    }

    void EjecutarImpulso()
    {
        // AQUI ESTÁ EL CAMBIO CLAVE:
        // En lugar de leer el Input actual (que puede ser 0), usamos la 'ultimaDireccionX'
        // que hemos estado guardando todo el rato.

        Vector2 direccionImpulso = new Vector2(ultimaDireccionX, 0).normalized;

        // Aplicamos el impulso. 
        // ForceMode2D.Impulse ignora si estás en el suelo o aire, es un golpe seco inmediato.
        rb.AddForce(direccionImpulso * fuerzaActual, ForceMode2D.Impulse);

        Debug.Log("Impulso hacia: " + ultimaDireccionX + " con fuerza: " + fuerzaActual);
    }
}