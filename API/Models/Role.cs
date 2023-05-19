using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }


        //Cardinalitas dengan AccountRole
        public ICollection<AccountRole>? AccountRole { get; set; }
    }
}
