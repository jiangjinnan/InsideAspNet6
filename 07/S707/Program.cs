using App;
using System.Data;
using System.Diagnostics;

DiagnosticListener.AllListeners.Subscribe( new Observer<DiagnosticListener>(
    listener => {
        if (listener.Name == "Artech-Data-SqlClient")
        {
            listener.SubscribeWithAdapter(new DatabaseSourceCollector());
        }
    }));

var source = new DiagnosticListener("Artech-Data-SqlClient");
source.Write("CommandExecution", new {
        CommandType = CommandType.Text,
        CommandText = "SELECT * FROM T_USER"
    });