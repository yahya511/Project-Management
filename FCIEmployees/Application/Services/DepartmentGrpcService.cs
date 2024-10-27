using Application.IServices;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Proto;

namespace Application.Services
{
    public class DepartmentGrpcService : IDepartmentGrpcService
    {
        private readonly string _departmentGrpcUrl;

        public DepartmentGrpcService(IConfiguration configuration)
        {
            _departmentGrpcUrl = configuration["GrpcSettings:DepartmentGrpcUrl"] ?? "http://localhost:5134"; // استخدم المنفذ المحدث هنا
        }

        public async Task<bool> CheckDepartmentExistsAsync(int departmentId)
        {
            // إعداد HttpClientHandler لتجاوز مشاكل SSL في بيئات التطوير
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator // استخدم بحذر
            };
            using var channel = GrpcChannel.ForAddress(_departmentGrpcUrl, new GrpcChannelOptions { HttpClient = new HttpClient(httpClientHandler) });
            var client = new DepartmentService.DepartmentServiceClient(channel);

            var request = new DepartmentExistRequest { DepartmentId = departmentId };
            var response = await client.DepartmentExistAsync(request);
            return response.Exists;
        }


        public async Task<bool> CheckManagerExistsAsync(int managerId)
        {
            // إعداد HttpClientHandler لتجاوز مشاكل SSL في بيئات التطوير
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator // استخدم بحذر
            };
            using var channel = GrpcChannel.ForAddress(_departmentGrpcUrl, new GrpcChannelOptions { HttpClient = new HttpClient(httpClientHandler) });
            var client = new DepartmentService.DepartmentServiceClient(channel);

            var request = new ManagerExistRequest { ManagerId = managerId };
            var response = await client.ManagerExistAsync(request);
            return response.Exists;
        }
    }
}
