using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoretechBack.Database.Models;

public class NotificationStatusRelationConfiguration : IEntityTypeConfiguration<NotificationStatusRelation>
{
    public void Configure(EntityTypeBuilder<NotificationStatusRelation> builder)
    {
        builder.HasKey(relation => relation.Id);
    }
}