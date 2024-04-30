using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ControlAtuZap.Classes
{
  public class Conexao
  {
    public IDbConnection conexao;
    public string connectionString;
    public DataSet dsConexao;
    static IDbCommand comand;

    public bool Conectar()
    {
      try
      {
        conexao = new SqlConnection();

        dsConexao = new DataSet();

        string conn = string.Empty;

        dsConexao = new DataSet();

        //conn = @"Data Source=184.168.47.19;Integrated Security=False;Initial Catalog=controlGerenciador; User ID=Mundo-Digital; Password=246138Ser; Connect Timeout=30;Encrypt=False;Packet Size=4096";
        conn = @"SERVER=mundodigital.ddns.net, 4200\SQLEXPRESS;Initial Catalog=controlGerenciador;User Id=MundoDigital;Password=135246;Connect Timeout=30";

        conexao.ConnectionString = conn;

        conexao.Open();

        string nome_Computador = Environment.MachineName;
      }
      catch
      {
        return false;
      }

      return true;
    }

    public string FormataDataHora(DateTime dataHora)
    {
      string resultadoDataHora = string.Empty;

      resultadoDataHora = "'" +
                    dataHora.Year.ToString() + "." +
                    dataHora.Month.ToString().PadLeft(2, '0') + "." +
                    dataHora.Day.ToString().PadLeft(2, '0') + " " +
                    dataHora.Hour.ToString().PadLeft(2, '0') + ":" +
                    dataHora.Minute.ToString().PadLeft(2, '0') + ":" +
                    dataHora.Second.ToString().PadLeft(2, '0') + "'";

      return resultadoDataHora;
    }
    public DateTime dataServidor()
    {
      return DateTime.Parse(RetornaDados("SELECT GETDATE()"));
    }
    //public void BindingControl(Control control, string property, string field, BindingSource bds)
    //{
    //  if (control is ComboBox || control is PictureBox || control is CheckBox || control is MaskedTextBox ||
    //    control is ControlAtuZap.Data.Components.DateData)
    //  {
    //    control.DataBindings.Add(property, bds, field, true);
    //  }
    //  else
    //  {
    //    control.DataBindings.Add(property, bds, field);
    //  }
    //}

    public void SetMask(TextBox txtcontrol, string format)
    {
      if (txtcontrol.DataBindings.Count == 0)
      {
        return;
      }

      txtcontrol.DataBindings[0].FormattingEnabled = true;
      txtcontrol.DataBindings[0].FormatString = format;
    }

    public void CarregaTable(string sql, ref IDbDataAdapter adapter, BindingSource bds, DataSet ds, IDbConnection conexaoAExecutar)
    {
      try
      {
        if (conexao is SqlConnection)
        {
          //sql += ("; DBCC FREEPROCCACHE ");
          //sql += ("; DBCC DROPCLEANBUFFERS ");
          sql += ("; DBCC FREESYSTEMCACHE ('ALL') WITH MARK_IN_USE_FOR_REMOVAL; ");
          //sql += ("; DBCC FREEPROCCACHE WITH NO_INFOMSGS; ");

          adapter = new SqlDataAdapter(sql, (SqlConnection)conexaoAExecutar);
          SqlCommandBuilder builder = new SqlCommandBuilder((SqlDataAdapter)adapter);
          builder.ConflictOption = ConflictOption.OverwriteChanges;
        }

        ds.Tables.Clear();
        bds.DataSource = null;
        adapter.Fill(ds);
        bds.DataMember = ds.Tables[0].TableName;
        bds.DataSource = ds;
      }
      catch
      {
        throw;
      }
    }

    public void CarregaTable(string sql, ref IDbDataAdapter adapter, BindingSource bds, DataSet ds)
    {
      CarregaTable(sql, ref adapter, bds, ds, conexao);
    }

    public string RetornaDados(string consulta, IDbConnection conexaoAExecutar)
    {
      string result = string.Empty;
      //consulta += ("; DBCC FREEPROCCACHE ");
      //consulta += ("; DBCC DROPCLEANBUFFERS ");
      //consulta += ("; DBCC FREESYSTEMCACHE ('ALL') WITH MARK_IN_USE_FOR_REMOVAL; ");
      //consulta += ("; DBCC FREEPROCCACHE WITH NO_INFOMSGS; ");

      if (conexao is SqlConnection)
      {
        if (conexao.State != ConnectionState.Open)
          conexao.Open();

        comand = new SqlCommand(consulta, conexao as SqlConnection);
      }

      try
      {

        object aux = comand.ExecuteScalar();

        comand.Dispose();

        if (aux != null)
        {
          result = aux.ToString();
        }
      }
      catch
      {
        throw;
      }
      return result;
    }

    public string RetornaDados(string consulta)
    {
      return RetornaDados(consulta, conexao);
    }

    public object RetornaDadosObject(string consulta, IDbConnection conexaoAExecutar)
    {
      object result = null;

      if (conexao is SqlConnection)
      {
        comand = new SqlCommand(consulta, (SqlConnection)conexaoAExecutar);
      }


      try
      {
        result = comand.ExecuteScalar();
        comand.Dispose();
      }
      catch
      {
      }
      return result;
    }

    public object RetornaDadosObject(string consulta)
    {
      return RetornaDadosObject(consulta, conexao);
    }

    public void ExecutaComando(string comandoexec)
    {
      ExecutaComando(comandoexec, conexao);
    }

    public void ExecutaComando(string comandoexec, IDbConnection conexaoAExecutar)
    {

      if (conexao.State != ConnectionState.Open)
        Conectar();

      if (conexao is SqlConnection)
      {
        //comandoexec += ("; DBCC FREEPROCCACHE ");
        //comandoexec += ("; DBCC DROPCLEANBUFFERS ");
        //comandoexec += ("; DBCC FREESYSTEMCACHE ('ALL') WITH MARK_IN_USE_FOR_REMOVAL; ");
        //comandoexec += ("; DBCC FREEPROCCACHE WITH NO_INFOMSGS; ");

        comand = new SqlCommand(comandoexec, conexao as SqlConnection);
      }


      try
      {
        comand.ExecuteNonQuery();
        comand.Dispose();
      }
      catch
      {
        throw;
      }
    }

    public void CarregaObjDados(string sql, ref IDbDataAdapter adapter, BindingSource bds, DataSet ds)
    {
      CarregaObjDados(sql, ref adapter, bds, ds, conexao);
    }

    public void CarregaObjDados(string sql, ref IDbDataAdapter adapter, BindingSource bds, DataSet ds, IDbConnection conexaoExecutar)
    {
      try
      {
        if (conexao is SqlConnection)
        {
          adapter = new SqlDataAdapter(sql, (SqlConnection)conexaoExecutar);
        }

        ds.Tables.Clear();
        bds.DataSource = null;
        adapter.Fill(ds);
        bds.DataMember = ds.Tables[0].TableName;
        bds.DataSource = ds;
      }
      catch
      {
        throw;
      }
    }

    public string FormataData(DateTime data)
    {
      string resultado = string.Empty;

      resultado = " CAST('" + data.Year.ToString() + "." + data.Month.ToString().PadLeft(2, '0') + "." + data.Day.ToString().PadLeft(2, '0') + "' AS DATE)";

      return resultado;
    }

    public DataSet RetornaDataSet(string comandoSQL)
    {
      SqlDataAdapter adapter = new SqlDataAdapter(comandoSQL, conexao as SqlConnection);
      var ds = new DataSet();
      try
      {
        //comandoSQL += ("; DBCC FREEPROCCACHE ");
        //comandoSQL += ("; DBCC DROPCLEANBUFFERS ");
        comandoSQL += ("; DBCC FREESYSTEMCACHE ('ALL') WITH MARK_IN_USE_FOR_REMOVAL; ");
        //comandoSQL += ("; DBCC FREEPROCCACHE WITH NO_INFOMSGS; ");

        adapter.Fill(ds);
      }
      catch
      {
        return null;
      }

      adapter.Dispose();

      return ds;
    }
  }
}
