namespace TCP.API.Routes.Dto;

public record CreateUpdateQemuConfigDto(string? Description, string? CiPassword, string? CiUser, string? IpGateway, int? CpuCores, int? CpuLimit, string? IpAddress);