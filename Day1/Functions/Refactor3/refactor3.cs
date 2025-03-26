public class EmployeeDatabase 
{
    private readonly IDbConnection _db;

    public EmployeeDatabase(IDbConnection dbConnection) 
    {
        _db = dbConnection;
    }

    public Employee GetEmployee(int id) 
    {
        
        Employee employee =  _db.QueryFirstOrDefault<Employee>(
            "SELECT * FROM Employees WHERE Id = @Id", 
            new { Id = id });

        return employee;
    }

    public void SaveEmployee(Employee emp) 
    {       

        _db.Execute(
            "UPDATE Employees SET Name = @Name WHERE Id = @Id", 
            new { emp.Name, emp.Id });
    }

    public void DeleteEmployee(int id) 
    {       
        _db.Execute(
            "DELETE FROM Employees WHERE Id = @Id", 
            new { Id = id });
    }
}



public class EmployeeService 
{
    private readonly EmployeeDatabase _employeeDb;
    private const int MAX_NAME_LENGTH = 150;
    private const int MIN_EMPLOYEE_ID = 50;
    private const int CHAIRMAN_ID = 1;
    public EmployeeService(EmployeeDatabase _employeeDb) 
    {
        _employeeDb = employeeDb;
    }
   

    public Employee GetEmployee(int id) 
    { 
        ValidateEmployeeIDforGetEmployee(id);

        return _employeeDb.GetEmployee(id);
    }

    public void UpdateEmployee(Employee emp) 
    {
        validateEmployee(emp);

        _employeeDb.SaveEmployee(emp);
    }

    public void RemoveEmployee(int id) 
    {
        ValidateEmployeeIDforRemoveEmployee(id);
        _employeeDb.DeleteEmployee(id);
    }

    private ValidateEmployeeIDforGetEmployee(int id)
    {
        if (id < MIN_EMPLOYEE_ID)
            throw new InvalidDataException("Employee ID starts from 50 , 1 to 49 is reserved for promoters , investors etc . Data cant be shared in HRMS due to Security issues ");
    }

    private ValidateEmployeeIDforRemoveEmployee(int id)
    {
        if (id == CHAIRMAN_ID)
            throw new InvalidDataException("Chairman cant be removed ");
    }

    private ValidateEmployee(Employee employee)
    {
       if (emp.Name.Length > MAX_NAME_LENGTH)
            throw new InvalidDataException("Name too long");
    }
}