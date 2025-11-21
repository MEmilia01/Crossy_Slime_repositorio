using UnityEngine;
using TMPro;

public class Puntuaciontexto : MonoBehaviour
{
    private float puntosinico = 0f;
    public float puntosbloques;
    public float puntosfinal;

    private TextMeshProUGUI textMesh;

    void Start()
    {
        puntosbloques = puntosinico;
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = puntosbloques.ToString("0");
    }
}
