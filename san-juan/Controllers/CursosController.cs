using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CursosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/cursos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
    {
        return await _context.Cursos.Include(c => c.Docente).ToListAsync();
    }

    // GET: api/cursos/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCurso(int id)
    {
        var curso = await _context.Cursos.Include(c => c.Docente)
                                         .FirstOrDefaultAsync(c => c.Id == id);
        if (curso == null)
        {
            return NotFound();
        }
        return curso;
    }

    // GET: api/cursos/ciclo/{ciclo}
    [HttpGet("ciclo/{ciclo}")]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursosByCiclo(string ciclo)
    {
        var cursos = await _context.Cursos.Include(c => c.Docente)
                                           .Where(c => c.Ciclo == ciclo)
                                           .ToListAsync();
        return cursos;
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> PostCurso(CursoDTO cursoDTO)
    {
        // Validación del modelo
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Mapeo del DTO al modelo de Curso
        var curso = new Curso
        {
            CursoNombre = cursoDTO.Curso, // Mapeo de 'CursoNombre'
            Creditos = cursoDTO.Creditos,
            HorasSemanal = cursoDTO.HorasSemanal,
            Ciclo = cursoDTO.Ciclo,
            IdDocente = cursoDTO.IdDocente // Asignación del ID del Docente
        };

        // Guardar el curso en la base de datos
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();

        // Retornar el curso recién creado
        return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
    }

    // PUT: api/cursos/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCurso(int id, Curso curso)
    {
        if (id != curso.Id)
        {
            return BadRequest();
        }

        _context.Entry(curso).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/cursos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
