namespace Microsoft.Extensions.Configuration;

public class EnvireConfiguration
{
    public ConnectionStrings ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    public string Mongo { get; set; }
}