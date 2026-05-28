using System;

// Clases molde para la Historia
[Serializable]
public class PaginaHistoria
{
    public string nombreImagen;
    public string textoNarrativo;
}

[Serializable]
public class HistoriaWrapper
{
    public PaginaHistoria[] paginas;
}

// Clases molde para los Niveles
[Serializable]
public class ConfigNivel
{
    public int numeroNivel;
    public float tiempoSegundos;
    public string mensajeEntrada;
}

[Serializable]
public class NivelesWrapper
{
    public ConfigNivel[] niveles;
}