using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Objetos a seguir")]
    public Transform objetoA;
    public Transform objetoB;

    [Header("Ajustes de cámara")]
    public float suavizado = 5f;
    public float distanciaZ = -10f; // Mantiene la cámara en Z negativa

    void LateUpdate()
    {
        if (objetoA == null || objetoB == null)
            return;

        // Calcular punto medio
        Vector3 puntoMedio = (objetoA.position + objetoB.position) / 2f;

        // Mantener Z estable para la cámara 2D
        puntoMedio.z = distanciaZ;

        // Movimiento suave
        transform.position = Vector3.Lerp(transform.position, puntoMedio, Time.deltaTime * suavizado);
    }
}
