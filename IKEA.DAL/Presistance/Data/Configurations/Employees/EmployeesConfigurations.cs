using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKEA.DAL.Presistance.Data.Configurations.Employees
{
    internal class EmployeesConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(e => e.Address).HasColumnType("nvarchar(100)");
            builder.Property(e => e.Salary).HasColumnType("decimal(8,2)");
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(e=>e.Gender).HasConversion(
                (gender)=>gender.ToString(),
                (gender)=>(Gender)Enum.Parse(typeof(Gender),gender)
                );
            builder.Property(e => e.EmployeeType).HasConversion(
                (employeeType) => employeeType.ToString(),
                (employeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType)
                );
        }
    }
}
