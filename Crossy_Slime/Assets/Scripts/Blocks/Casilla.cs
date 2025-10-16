using UnityEngine;
public enum TipoCasillas
{
    normal,ice,teleport,longjump,breakable
}
public class Casilla : MonoBehaviour
{
    public Transform Pivot;
    public TipoCasillas TCasilla;
    Movement player;
    private void Start()
    {
        player = FindFirstObjectByType<Movement>();
    }
    public void Comportamiento()
    {
        if (TCasilla == TipoCasillas.ice)
        {
            // Logica de casilla hielo
            if (player.lastInput == Input.GetKeyDown(KeyCode.W))
            {
                player.MoveForward();
            }
            else if (player.lastInput == Input.GetKeyDown(KeyCode.S))
            {
                player.MoveBackward();
            }
            else if (player.lastInput == Input.GetKeyDown(KeyCode.D))
            {
                player.MoveRight();
            }
            else if (player.lastInput == Input.GetKeyDown(KeyCode.A))
            {
                player.MoveLeft();
            }

        }
    }
    //Recibimos el pivote 
    public Transform GetPivot()
    {
        return Pivot;
    }
}
