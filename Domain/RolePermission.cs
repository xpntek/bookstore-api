using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class RolePermission:BaseEntity
{
    public string RoleId { get; set; }
    [ForeignKey("RoleId")] public IdentityRole? Role { get; set; }
    public int PermissionId { get; set; }
    [ForeignKey("PermissionId")] public Permission? Permission { get; set; }
}