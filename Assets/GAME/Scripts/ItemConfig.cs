using System;

[Serializable]
public class ItemConfig
{
    public string id;
    public string tipo; // Cristal o Herramienta
    public string nombre;
    public int escenaAsignada;
    public float posX;
    public float posY;
    public float posZ;
}

[Serializable]
public class ItemsWrapper
{
    public ItemConfig[] items;
}