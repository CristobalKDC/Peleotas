using UnityEngine;

public class MovimientoBola2D : MonoBehaviour
{
    public float fuerzaMovimiento = 5f;

    public enum TipoControl
    {
        WASD,
        Flechas
    }

    [Header("Tipo de control")]
    public TipoControl tipoControl = TipoControl.WASD;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float inputHorizontal = 0f;
        float inputVertical = 0f;

        // Selección del tipo de control
        if (tipoControl == TipoControl.WASD)
        {
            inputHorizontal = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
            inputVertical = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);
        }
        else if (tipoControl == TipoControl.Flechas)
        {
            inputHorizontal = (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0) + (Input.GetKey(KeyCode.RightArrow) ? 1 : 0);
            inputVertical = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
        }

        Vector2 direccionMovimiento = new Vector2(inputHorizontal, inputVertical);

        rb.AddForce(direccionMovimiento * fuerzaMovimiento);
    }
}
