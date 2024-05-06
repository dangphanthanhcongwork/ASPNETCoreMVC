using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

public class RookiesController : Controller
{
    private List<Person> _persons =
    [
        // Add your dummy data here
        new Person { FirstName = "Công", LastName = "Đặng Phan Thành", Gender = "Male", DateOfBirth = new DateTime(2000, 6, 15), PhoneNumber = "0375284637", BirthPlace = "Lâm Đồng", IsGraduated = true },
        new Person { FirstName = "Linh", LastName = "Nguyễn Mỹ", Gender = "Female", DateOfBirth = new DateTime(1995, 7, 4), PhoneNumber = "0375284636", BirthPlace = "Hà Nội", IsGraduated = true },
        new Person { FirstName = "Phương", LastName = "Nguyễn Thị Mai", Gender = "Female", DateOfBirth = new DateTime(2002, 4, 7), PhoneNumber = "0375284635", BirthPlace = "Hải Phòng", IsGraduated = false },
        new Person { FirstName = "Thu", LastName = "Phan Thị Hà", Gender = "Female", DateOfBirth = new DateTime(2003, 2, 27), PhoneNumber = "0375284634", BirthPlace = "Huế", IsGraduated = false },
        new Person { FirstName = "Quang", LastName = "Trần Huy", Gender = "Male", DateOfBirth = new DateTime(1994, 4, 20), PhoneNumber = "0375284633", BirthPlace = "Hà Nội", IsGraduated = false },
    ];

    public IActionResult Index()
    {
        return View("RookiesHomeView");
    }

    public IActionResult GetAll()
    {
        return View("SharedView", _persons);
    }

    public IActionResult GetMales()
    {
        var males = _persons.Where(p => p.Gender == "Male").ToList();
        return View("SharedView", males);
    }

    public IActionResult GetOldest()
    {
        var oldest = _persons.OrderBy(p => p.DateOfBirth).First();
        return View("SharedView", new List<Person> { oldest });
    }

    public IActionResult GetFullNames()
    {
        return View("GetFullNamesView", _persons);
    }


    public IActionResult GetByBirthYear(int year)
    {
        var people = _persons.Where(p => p.DateOfBirth.Year == year).ToList();
        return View("SharedView", people);
    }

    public IActionResult GetByBirthYearGreaterThan(int year)
    {
        var people = _persons.Where(p => p.DateOfBirth.Year > year).ToList();
        return View("SharedView", people);
    }

    public IActionResult GetByBirthYearLessThan(int year)
    {
        var people = _persons.Where(p => p.DateOfBirth.Year < year).ToList();
        return View("SharedView", people);
    }

    public IActionResult DownloadExcel()
    {
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var worksheet = package.Workbook.Worksheets.Add("Persons");
            worksheet.Cells.LoadFromCollection(_persons, PrintHeaders: true);
            package.Save();
        }

        stream.Position = 0;
        string excelName = $"Persons.xlsx";

        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
    }
}
