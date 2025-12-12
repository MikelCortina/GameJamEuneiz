using UnityEngine;

public class Oveja : EntidadData
{
    public MoverObjetoPorDistancia moverObjetoPorDistancia;
    public ContadorOvejas contadorOvejas;
    public override void Morir()
    {
        Debug.Log(  contadorOvejas.valor);
        contadorOvejas.ActualizarTexto();
        moverObjetoPorDistancia.Mover2();
        base.Morir(); 
    }
}
