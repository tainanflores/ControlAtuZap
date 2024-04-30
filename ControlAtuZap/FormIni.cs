using ControlAtuZap.Classes;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlAtuZap
{
  public partial class FormIni : Form
  {
    string diretorioPrograma = AppDomain.CurrentDomain.BaseDirectory;
    string diretorioProgramFiles = $@"C:\Mundo Digital\AtendimentoZap";

    public static bool atualizar = false;

    bool abril = false, japassou = false;
    public FormIni()
    {
      InitializeComponent();
      label8.Text = Application.ProductVersion;
    }
    
    private void FormIni_Shown(object sender, EventArgs e)
    {
      japassou = false;

      diretorioProgramFiles = $@"C:\Mundo Digital\AtendimentoZap";

      diretorioPrograma = @"C:\Mundo Digital\AtendimentoZap\ControlAtendimento";

      if (!Directory.Exists(diretorioPrograma))
      {
        try
        {
          Directory.CreateDirectory(diretorioPrograma);
        }
        catch (Exception ef)
        {
          MessageBox.Show(ef.Message);
        }
      }

      string versao = Evento.verificarVersao(diretorioPrograma);
      label4.Text = versao;

      ThreadStart threadStart = new ThreadStart(loop);
      Thread thread = new Thread(threadStart);
      thread.Start();
    }

    private delegate void SafeCallDelegate(string text);
    private void SetIvonk(string text)
    {
      if (label3.InvokeRequired)
      {
        var d = new SafeCallDelegate(SetIvonk);
        label5.Invoke(d, new object[] { text });
      }
      else
      {
        label5.Text = text;
        label6.Visible = false;
      }
    }

    private async void loop()
    {
      string diretorioPrograma = $@"C:\Mundo Digital\AtendimentoZap\ControlAtendimento";
      string diretorio = @"C:\Mundo Digital\AtendimentoZap";// Aqui você vai definir o arquivo temporario
      string Arquivo = "ControlAtendimento";

      Task task = Task.Run(async () =>
      {
        progressBar1.Value = 0;
        if (File.Exists(diretorio + @"\" + Arquivo + ".zip"))
          File.Delete(diretorio + @"\" + Arquivo + ".zip");

        this.Invoke((MethodInvoker)(() => label7.Visible = true));
        this.Invoke((MethodInvoker)(() => progressBar1.Visible = true));
        this.Invoke((MethodInvoker)(async () => { await ftps.VerificarVersao("ControlAtuZap", progressBar1, label7); }));
        this.Invoke((MethodInvoker)(() => progressBar1.Visible = false));
        this.Invoke((MethodInvoker)(() => label7.Text = "Nova Versão Baixada com sucesso!"));

        string version = "";
        var dsConexao = new DataSet();

        if (File.Exists(diretorio))
        {
          dsConexao = new DataSet();
          dsConexao.ReadXml(diretorio);

          version = dsConexao.Tables[0].Rows[0]["Versao"].ToString();
          this.Invoke((MethodInvoker)(() => label4.Text = version));
        }
      });

      await task.ContinueWith(result =>
      {
        if (atualizar)
        {
          if (File.Exists(diretorio + @"\" + Arquivo + ".zip"))
          {
            this.Invoke((MethodInvoker)(() => label7.Text = "Descompactando " + Arquivo + ".zip"));

            ZipZap.ExtrairArquivoZip(diretorio + @"\" + Arquivo + ".zip", diretorio);

            string diretorioPadrao = @"C:\Mundo Digital\AtendimentoZap\ControlAtendimento\";
            if (Directory.Exists(diretorio + @"\Release"))
            {
              try
              {
                if (Directory.Exists(diretorioPadrao))
                 //teste git
                Directory.Delete(diretorioPadrao, true);

                if (!Directory.Exists(diretorio + $@"\{Arquivo}"))
                  Directory.Move(diretorio + @"\Release", diretorio + $@"\{Arquivo}");

                File.Delete(diretorio + @"\" + Arquivo + ".zip");
              }
              catch (Exception e) { }
            }

            this.Invoke((MethodInvoker)(() => label7.Text = "Executando a Nova Versão..."));
            executarAtendimento();
          }
        }
        else
        {
          string version = "";
          var dsConexao = new DataSet();

          if (File.Exists(diretorio))
          {
            dsConexao = new DataSet();
            dsConexao.ReadXml(diretorio);

            version = dsConexao.Tables[0].Rows[0]["Versao"].ToString();
            this.Invoke((MethodInvoker)(() => label4.Text = version));
          }

          this.Invoke((MethodInvoker)(() => label7.Text = "Executando Atendimento..."));
          executarAtendimento();
        }
      }, TaskContinuationOptions.ExecuteSynchronously).ConfigureAwait(true);
    }

    static void CopiarPasta(string origem, string destino)
    {
      // Verifique se a pasta de origem existe
      if (!Directory.Exists(origem))
      {
        Console.WriteLine("A pasta de origem não existe.");
        return;
      }

      // Crie o diretório de destino, se necessário
      if (!Directory.Exists(destino))
      {
        Directory.CreateDirectory(destino);
      }

      // Obtenha todos os arquivos na pasta de origem
      string[] arquivos = Directory.GetFiles(origem);

      // Copie cada arquivo para a pasta de destino
      foreach (string arquivo in arquivos)
      {
        string nomeArquivo = Path.GetFileName(arquivo);
        string caminhoDestino = Path.Combine(destino, nomeArquivo);
        File.Copy(arquivo, caminhoDestino, true); // Use true para substituir arquivos existentes
      }

      // Obtenha todas as subpastas na pasta de origem
      string[] subpastas = Directory.GetDirectories(origem);

      // Recursivamente copie cada subpasta
      foreach (string subpasta in subpastas)
      {
        string nomeSubpasta = Path.GetFileName(subpasta);
        string caminhoDestinoSubpasta = Path.Combine(destino, nomeSubpasta);
        CopiarPasta(subpasta, caminhoDestinoSubpasta);
      }
    }

    private void createDiretory(string caminho)
    {
      try
      {
        Directory.CreateDirectory(caminho);
      }
      catch (Exception ef)
      {
        MessageBox.Show("Não foi possivel Limpar Atualizacao Antiga devido ao " + ef.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
    private void deleteCopy(string Caminho)
    {
      try
      {
        Directory.Delete(Caminho, true);
      }
      catch (Exception ef)
      {
        MessageBox.Show("Não foi possivel Limpar Atualizacao Antiga devido ao " + ef.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
    private void copy_Paste(string CopyDe, string CopyPara)
    {
      string nome = "";

      try
      {
        DirectoryInfo inforep = new DirectoryInfo(CopyDe);
        foreach (var file in inforep.GetFiles())
        {
          nome = file.Name;
          this.Invoke((MethodInvoker)(() => label7.Text = "Atualizando -" + nome));

          File.Copy(CopyDe + @"\" + nome, CopyPara + @"\" + nome, true);
          File.Delete(CopyDe + @"\" + nome);
        }
      }
      catch (Exception ef)
      {
        MessageBox.Show("Não foi possivel Copy " + nome + " devido ao" + ef.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void executarAtendimento()
    {
      if (!japassou)
      {

        if (!abril)
          Process.Start(@"C:\Mundo Digital\AtendimentoZap\ControlAtendimento\ControlAtendimento.exe");
        Thread.Sleep(1000);

        japassou = true;

        this.Invoke((MethodInvoker)(() => Process.GetCurrentProcess().Kill()));
      }
    }
  }
}