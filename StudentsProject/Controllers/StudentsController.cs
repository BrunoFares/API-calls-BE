using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[Controller]")]
public class StudentsController : ControllerBase
{
    private readonly ILogger<StudentsController> _logger;

    Student[] students = {
        new Student(1, "Pierre", "6/4"),
        new Student(2, "John", "7/3"),
        new Student(3, "Paul", "8/4"),
        new Student(4, "Sarah", "1S2"),
        new Student(5, "Ali", "3S1"),
        new Student(6, "Elma", "5/1"),
        new Student(7, "Elias", "9/1")
    };

    public StudentsController(ILogger<StudentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetAll()")]
    public IActionResult GetAll()
    {
        return Ok(students);
    }

    [HttpGet("GetStudentByID({id})")]
    public IActionResult GetStudentByID([FromRoute] int id)
    {
        int studentId = -1;
        var std = Array.Find(students, item =>
        {
            studentId++;
            if (item.ID == id)
            {
                return true;
            }
            return false;
        });

        if (std != null)
        {
            return Ok(students[studentId]);
        }
        else
        {
            return NotFound("No user available for ID " + id);
        }
    }

    [HttpPost("AddStudent({id}-{name}-{classroom})")]
    public IActionResult AddStudent(int id, string name, string classroom)
    {
        int studentId = -1;
        var std = Array.Find(students, item =>
        {
            studentId++;
            if (item.ID == id)
            {
                return true;
            }
            return false;
        });

        if (std != null)
        {
            return BadRequest("ID " + studentId + " is already taken");
        }
        else if (name.Length == 0 || classroom.Length == 0)
        {
            return BadRequest("No field can be left empty.");
        }

        var student = new Student(id, name, classroom);
        students.Append<Student>(student);
        return Ok("Added student with ID " + student.ID);
    }

    [HttpDelete("DeleteStudent({id})")]
    public ActionResult DeleteStudent(int id)
    {
        int studentId = -1;
        var std = Array.Find(students, item =>
        {
            studentId++;
            if (item.ID == id)
            {
                return true;
            }
            return false;
        });

        Student[] newStudents = [];

        if (std == null)
        {
            return BadRequest();
        }

        for (int i = 0; i < students.Length; i++)
        {
            if (i == studentId)
            {
                continue;
            }
            _ = newStudents.Append(students[i]);
        }
        students = newStudents;
        return Ok(students);
    }

    [HttpPut("UpdateStudent({id}-{name}-{classroom})")]
    public ActionResult UpdateStudent(int id, string name, string classroom)
    {
        int studentId = -1;
        var std = Array.Find(students, item =>
        {
            studentId++;
            if (item.ID == id)
            {
                return true;
            }
            return false;
        });

        if (std == null)
        {
            return BadRequest();
        }

        Student student = new(id, name, classroom);
        students[studentId] = student;
        return Ok(student);
    }
}