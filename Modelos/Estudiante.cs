namespace Modelos;

public class Estudiante 
{
    public string Dni { get; set; } = "" ;
    public string Apellido { get; set; } = "" ;
    public string Nombre { get; set; } = "" ;
    public string Correo { get; set; } = "" ;
    public string CodigoGrupo { get; set; } = "NO ASIGNADO";
    public bool YaParticipo { get; set; } = false;
}