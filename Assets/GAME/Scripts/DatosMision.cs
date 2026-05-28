using UnityEngine;

[System.Serializable]
public class DatosMision
{
    public int numeroNivel;
    public string nombreNivel;
    public string nombreEscena;
    public float tiempoSegundos;
    public string descripcionPlaya;
    public string mensajeEntrada;
}

[System.Serializable]
public class ListaMisiones
{
    public DatosMision[] niveles;
}