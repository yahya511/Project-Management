public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesRequest, IEnumerable<GetEmployeeDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllEmployeesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GetEmployeeDTO>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();

        var employeeDTOs = new List<GetEmployeeDTO>();

        foreach (var employee in employees)
        {
            var address = employee.AddressID.HasValue
                ? (await _unitOfWork.Addresses.GetEntityByIdAsync(employee.AddressID.Value))?.AddressText: null;

            employeeDTOs.Add(new GetEmployeeDTO
            {
                ID=employee.EmployeeID,
                FullName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                JobTitle = Regex.Replace(employee.JobTitle.ToString(), "([A-Z])", " $1").Trim(),
                DepartmentID = employee.DepartmentID,
                ManagerID = employee.ManagerID,
                Address = address,
                HireDate = employee.HireDate,
                Salary = employee.Salary
            });
        }

        return employeeDTOs;
    }

}
