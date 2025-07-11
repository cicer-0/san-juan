public class Curso
{
    public int Id { get; set; }
    public string CursoNombre{ get; set; }
    public int Creditos { get; set; }
    public int HorasSemanal { get; set; }
    public string Ciclo { get; set; }
    public int IdDocente { get; set; }
    public Docente Docente { get; set; }
}
