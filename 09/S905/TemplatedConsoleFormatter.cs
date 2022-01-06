using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System.Text;

namespace App
{
    public class TemplatedConsoleFormatter : ConsoleFormatter
    {
        private readonly bool _includeScopes;
        private readonly string _tempalte;

        public TemplatedConsoleFormatter(IOptions<TemplatedConsoleFormatterOptions> options)
            : base("templated")
        {
            _includeScopes = options.Value.IncludeScopes;
            _tempalte = options.Value?.Template
                ?? "[{LogLevel}]{Category}/{EventId}:{Message}\n{Scopes}\n";
        }

        public override void Write<TState>(in LogEntry<TState> logEntry,
            IExternalScopeProvider scopeProvider, TextWriter textWriter)
        {
            var builder = new StringBuilder(_tempalte);
            builder.Replace("{Category}", logEntry.Category);
            builder.Replace("{EventId}", logEntry.EventId.ToString());
            builder.Replace("{LogLevel}", logEntry.LogLevel.ToString());
            builder.Replace("{Message}",
                logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception));

            if (_includeScopes && scopeProvider != null)
            {
                var buidler2 = new StringBuilder();
                var writer = new StringWriter(buidler2);
                scopeProvider.ForEachScope(WriteScope, writer);

                void WriteScope(object? scope, StringWriter state)
                {
                    writer.Write("=>" + scope);
                }

                builder.Replace("{Scopes}", buidler2.ToString());
            }
            textWriter.Write(builder);
        }
    }
}