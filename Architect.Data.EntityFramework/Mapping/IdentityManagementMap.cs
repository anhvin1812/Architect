using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architect.Entities.IdentityManagement;

namespace Architect.Data.EntityFramework.Mapping
{
    internal static class IdentityManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MemberMap());
        }

        private class MemberMap : EntityTypeConfiguration<Member>
        {
            public MemberMap()
            {
                ToTable("Member");
                // primary key
                HasKey(t => t.MemberId);
            }
        }
    }
}
