namespace Clase3Tp1.Modelos
{
    public class Grupo
    {
        public string CodigoGrupo { get; set; } = "" ;
        public List<string> EstudiantesDNI { get; set; } = new List<string>();
        public bool Participado { get; set; } = false;
    }
}