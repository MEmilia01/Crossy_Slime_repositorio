using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.GPUSort;

public class CargaMenu : MonoBehaviour
{
    [SerializeField] GameObject carga;
    public static CargaMenu cm; 
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (cm == null)
        {
            cm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += ApagarCarga;
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= ApagarCarga;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CargarMenuCarga()
    {
        carga.SetActive(true);
        StartCoroutine(EmpezarDelay());
    }
    public void ApagarCarga(Scene front, Scene to)
    {
        carga.SetActive(false);
    }
    IEnumerator EmpezarDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
