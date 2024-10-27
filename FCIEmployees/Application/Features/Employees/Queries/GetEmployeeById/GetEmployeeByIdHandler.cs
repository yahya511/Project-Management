


namespace Application.Features.Employees.Queries.GetEmployeeById

{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdRequest, GetEmployeeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeeByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetEmployeeDTO> Handle(GetEmployeeByIdRequest request, CancellationToken cancellationToken)
        {
            // جلب الموظف من قاعدة البيانات
            var employee = await _unitOfWork.Employees.GetEntityByIdAsync(request.EmployeeID);
            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

            // تحويل رقم JobTitle إلى النص المقابل له باستخدام الـ enum
            var jobTitleText = employee.JobTitle.ToString();

            // التحقق من أن AddressID ليس فارغًا قبل جلب العنوان
            var address = employee.AddressID.HasValue
                ? await _unitOfWork.Addresses.GetEntityByIdAsync(employee.AddressID.Value)
                : null;

            // إنشاء الكائن GetEmployeeDTO
            var emp = new GetEmployeeDTO
            {
                ID=employee.EmployeeID,
                FullName = employee.FirstName + " " + (employee.MiddleName ?? "") + " " + employee.LastName,
                Address = address?.AddressText ?? "No address available",
                DepartmentID = employee.DepartmentID,
                ManagerID = employee.ManagerID,
                JobTitle = Regex.Replace(jobTitleText, "([A-Z])", " $1").Trim(),
                HireDate = employee.HireDate,
                Salary = employee.Salary
            };

            return emp;
        }

   
    }

}
