namespace Luftborn.Application.Common.Configurations;

public class AppSettings
    {
        public LoggingSettings Logging { get; set; } = new ();
        public ConnectionStringsSettings ConnectionStrings { get; set; } = new ();
        public SerilogSettings Serilog { get; set; } = new ();
        public string AllowedHosts { get; set; } = "*";
    }

    public class LoggingSettings
    {
        public LogLevelSettings LogLevel { get; set; } = new ();
    }

    public class LogLevelSettings
    {
        public string Default { get; set; } = "Information";
        public Dictionary<string, string> Override { get; set; } = new();
    }

    public class ConnectionStringsSettings
    {
        public string EcommerceDbConnection { get; set; } = string.Empty;
        public string EcommerceDbLogConnection { get; set; } = string.Empty;
        public string Hangfire { get; set; } = string.Empty;
    }

    public class SerilogSettings
    {
        public string SeqServerUrl { get; set; } = string.Empty;
        public string SeqServerToken { get; set; } = string.Empty;
        public List<string> Using { get; set; } = new();
        public MinimumLevelSettings MinimumLevel { get; set; } = new ();
        public List<WriteToSettings> WriteTo { get; set; } = new();
        public List<string> Enrich { get; set; } = new();
        public Dictionary<string, string> Properties { get; set; } = new();
    }

    public class MinimumLevelSettings
    {
        public string Default { get; set; } = "Information";
        public Dictionary<string, string> Override { get; set; } = new();
    }

    public class WriteToSettings
    {
        public string Name { get; set; } = string.Empty;
        public WriteToArgs Args { get; set; } = new ();
    }

    public class WriteToArgs
    {
        public string? OutputTemplate { get; set; }
        public string? Path { get; set; }
        public string? RollingInterval { get; set; }
    }