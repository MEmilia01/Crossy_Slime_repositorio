using UnityEngine;
public enum TipoCasillas
{
    normal,ice,teleport,longjump,breakable
}
public class Casilla : MonoBehaviour
{
    public Transform Pivot;
    public TipoCasillas TCasilla;
    private void Start()
    {
    }
    public void Comportamiento()
    {
        //if (TCasilla == TipoCasillas.ice)
        //{
           
        //    // Logica de casilla hielo
        //}
    }
    //Recibimos el pivote 
    public Transform GetPivot()
    {
        return Pivot;
    }
}
