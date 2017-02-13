using System;

namespace Todo.Core.Database
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int InsertUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
