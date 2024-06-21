using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Common.Interfaces;

namespace DataAccess.Common;

public class BaseEntity : IEntity
{
    [Key] [Column("Id", Order = 0)] public int Id { get; set; }
}