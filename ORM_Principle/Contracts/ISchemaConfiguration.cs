using System;

namespace ORM_Principle.Contracts
{
    public interface ISchemaConfiguration
    {
        string GetConnectionString();
        Type GetProviderType();
    }
}
