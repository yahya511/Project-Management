namespace Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdRequest : IRequest<Department>
    {
        public int DepartmentID { get; set; }
    }
}