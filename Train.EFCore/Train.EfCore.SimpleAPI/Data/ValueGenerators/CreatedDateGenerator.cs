using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Train.EfCore.SimpleAPI.Data.ValueGenerators;

public class CreatedDateGenerator : ValueGenerator<DateTime>
{
    public override DateTime Next(EntityEntry entry)
    {
        return DateTime.UtcNow;
    }

    public override bool GeneratesTemporaryValues => false;
}