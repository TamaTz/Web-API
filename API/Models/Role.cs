using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_roles")]
public class Role : BaseEntity
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    
    // Cardinality
    public ICollection<AccountRole>? AccountRoles { get; set; }
}