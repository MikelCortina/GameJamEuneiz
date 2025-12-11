using UnityEngine;

public class Oveja : EntidadData
{
    public MoverObjetoPorDistancia moverObjetoPorDistancia;
    public ContadorOvejas contadorOvejas;
    public override void Morir()
    {
        contadorOvejas.valor++;
        contadorOvejas.ActualizarTexto();
        moverObjetoPorDistancia.Mover2();
        base.Morir(); 
    }
}
