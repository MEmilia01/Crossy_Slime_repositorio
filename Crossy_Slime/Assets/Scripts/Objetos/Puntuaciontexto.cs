using UnityEngine;
using TMPro;

public class Puntuaciontexto : MonoBehaviour
{
    private int puntosinico = 0;
    public int puntosbloques;
    public int puntosfinal;

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
