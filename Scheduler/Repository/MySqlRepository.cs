using Scheduler.Models;

namespace Scheduler.Repository
{
    public abstract class MySqlRepository
    {
        public readonly string Server = "52.206.157.109";
        public readonly string Database = "U04bgv";
        public readonly string Uid = "U04bgv";
        public readonly string Password = "53688194549";

        public string ConnectionString
        {
            get { return $"SERVER={Server}; DATABASE={Database}; Uid={Uid}; Pwd={Password};" + "SslMode=None; Convert Zero Datetime = True;"; }
        }

        public abstract void PopulateData();
    }
}
