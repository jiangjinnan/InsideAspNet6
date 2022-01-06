using App;
using System.Data;

_ = new DatabaseSourceListener();
DatabaseSource.Instance.OnCommandExecute(CommandType.Text, "SELECT * FROM T_USER");
