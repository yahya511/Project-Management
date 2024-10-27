using Application.DTOs;
using Application.IServices;
using Domain.Enums;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Proto;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;

namespace Application.Services
{
    public class EmployeeGrpcService : IEmployeeGrpcService
    {
        private readonly string _employeeGrpcUrl;

        public EmployeeGrpcService(IConfiguration configuration)
        {
            // تأكد من أن عنوان gRPC هو http://localhost:5134
            _employeeGrpcUrl = configuration["GrpcSettings:EmployeeGrpcUrl"] ?? "http://localhost:5134";
        }

        public async Task<bool> CheckManagerExistsAsync(int managerId)
        {
            // إعداد HttpClientHandler لتجاوز مشاكل SSL في بيئات التطوير
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator // استخدم بحذر
            };

            // استخدام القناة مع HttpClientHandler
            using var channel = GrpcChannel.ForAddress(_employeeGrpcUrl, new GrpcChannelOptions { HttpClient = new HttpClient(httpClientHandler) });
            var client = new EmployeeService.EmployeeServiceClient(channel);

            var request = new EmployeeExistRequest { ManagerId = managerId };
            var response = await client.EmployeeExistAsync(request);

            return response.Exists;
        }


        public async Task<DepartmentEmployeeDto> GetEmployeeByIdAsync(int employeeId)
        {
            // إعداد HttpClientHandler لتجاوز مشاكل SSL في بيئات التطوير
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator // استخدم بحذر
            };

            // استخدام القناة مع HttpClientHandler
            using var channel = GrpcChannel.ForAddress(_employeeGrpcUrl, new GrpcChannelOptions { HttpClient = new HttpClient(httpClientHandler) });
            var client = new EmployeeService.EmployeeServiceClient(channel);

            var request = new GetEmployeeByIdRequest { EmployeeId = employeeId };
            var response = await client.GetEmployeeByIdAsync(request);

            var JobTitleText = ((JobTitle)response.JobTitle).ToString();

            return new DepartmentEmployeeDto
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                JobTitle = Regex.Replace(JobTitleText, "([A-Z])", " $1").Trim()
            };
        }
    }
}
