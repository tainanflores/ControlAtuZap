using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlAtuZap.Classes
{
  public static class ftps
  {
    const string EnderecoFTP = "ftp://160.153.33.167/public_html";
    const string UsuarioFTP = "gashpe2qnobb";
    const string SenhaFTP = "135246Ser.";

    public static void CarregaFTP(string Caminho_Online, string Caminho_Local, string Nome_Arquivo)
    {
      try
      {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(EnderecoFTP + "/" + Caminho_Online + "/" + Path.GetFileName(Nome_Arquivo));
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(UsuarioFTP, SenhaFTP);
        request.UsePassive = true;
        request.UseBinary = true;
        request.KeepAlive = false;

        var stream = File.OpenRead("caminho" + "\\.pdf");
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        stream.Close();

        var reqStream = request.GetRequestStream();
        reqStream.Write(buffer, 0, buffer.Length);
        reqStream.Close();

      }
      catch (Exception ef)
      {
        MessageBox.Show(ef.Message);
      }
    }
    private static int GetFileSize(string url, NetworkCredential nc)
    {
      // Query size of the file to be downloaded
      WebRequest sizeRequest = WebRequest.Create(url);

      sizeRequest.Credentials = nc;
      sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;

      return (int)sizeRequest.GetResponse().ContentLength;
    }
    public static void BaixarArquivoFTP(string Caminho_Online, string Caminho_Local, string Nome_Arquivo, ProgressBar progressBar1, Label label7)
    {
      try
      {
        Caminho_Local = Caminho_Local += @"\AtendimentoZap";
        string url = EnderecoFTP + "/" + Caminho_Online + "/" + Path.GetFileName(Nome_Arquivo);

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
        request.Method = WebRequestMethods.Ftp.DownloadFile;

        NetworkCredential networkcredential = new NetworkCredential(UsuarioFTP, SenhaFTP);
        request.Credentials = new NetworkCredential(UsuarioFTP, SenhaFTP);
        request.UsePassive = true;
        request.UseBinary = true;
        request.KeepAlive = false;

        int size = GetFileSize(url, networkcredential);
        progressBar1.Maximum = size;
        //progressBar1.Invoke((MethodInvoker)(() => progressBar1.Maximum = size));

        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {
          using (Stream rs = response.GetResponseStream())
          {
            var cam = @"C:\Mundo Digital\AtendimentoZap";

            if (!Directory.Exists(cam))
            {
              Directory.CreateDirectory(cam);
            }

            using (FileStream ws = new FileStream(cam + @"\" + Nome_Arquivo, FileMode.Create))
            {
              byte[] buffer = new byte[2048];
              int bytesRead = rs.Read(buffer, 0, buffer.Length);
              while (bytesRead > 0)
              {
                ws.Write(buffer, 0, bytesRead);
                bytesRead = rs.Read(buffer, 0, buffer.Length);

                int position = (int)ws.Position;
                progressBar1.Value = position;
                label7.Text = "Taxa de Transferencia " + (position / 1024) + "            " + string.Format("{0:N0}", progressBar1.Value / 1000) + " de " + string.Format("{0:N0}", progressBar1.Maximum / 1000) + " Mega bytes";
                //Application.DoEvents();
                //progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = position));
                //label7.Invoke((MethodInvoker)(() => label7.Text = "Taxa de Transferencia " + (position / 1024)+ "            " + string.Format("{0:N0}", progressBar1.Value / 1000) + " de " + string.Format("{0:N0}", progressBar1.Maximum / 1000) + " Mega bytes"));
              }
            }
          }
        }
      }
      catch (Exception ef)
      {
        throw ef.InnerException;
      }
    }

    public static async Task VerificarVersao(string Caminho_Online, ProgressBar progressBar, Label label)
    {
      string caminhoLocal = $@"C:\Mundo Digital\AtendimentoZap\ControlAtendimento";
      string caminhoOnline = @"Atualizacao/" + Caminho_Online;
      string nomeArquivo = "ControlAtendimento.exe";
      string nomeArquivoCompactado = "ControlAtendimento.zip";

      try
      {

        if (!File.Exists(caminhoLocal + @"\" + nomeArquivo))
        {
          BaixarArquivoFTP(caminhoOnline, caminhoLocal, nomeArquivoCompactado, progressBar, label);
          FormIni.atualizar = true;
        }
        else
        {
          // Obtém a versão do arquivo local
          string versaoLocal = FileVersionInfo.GetVersionInfo(caminhoLocal + @"\" + nomeArquivo).FileVersion;

          // Obtém a versão do arquivo no servidor FTP
          string url = EnderecoFTP + "/" + caminhoOnline + "/ControlAtendimento.xml";
          string versaoOnline = GetFileVersionFromFTP(url);

          // Compara as versões
          if (VersaoOnlineMaiorQueLocal(versaoOnline, versaoLocal))
          {
            Console.WriteLine("A versão online é mais recente. Baixando...");
            // Chame sua função para baixar o arquivo
            BaixarArquivoFTP(caminhoOnline, caminhoLocal, nomeArquivoCompactado, progressBar, label);
            FormIni.atualizar = true;
          }
          else
          {
            Console.WriteLine("A versão local é igual ou mais recente. Não é necessário baixar.");
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao verificar a versão do arquivo: " + ex.Message);
      }
    }

    static bool VersaoOnlineMaiorQueLocal(string versaoOnline, string versaoLocal)
    {
      // Implemente a lógica para comparar as versões
      // Aqui, estou apenas comparando as strings das versões, mas você pode precisar de uma lógica mais sofisticada
      return string.Compare(versaoOnline, versaoLocal) > 0;
    }

    static string GetFileVersionFromFTP(string url)
    {
      try
      {
        string diretorio = $@"C:\Mundo Digital\AtendimentoZap\ControlAtendimento.xml";
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
        request.Method = WebRequestMethods.Ftp.DownloadFile;

        NetworkCredential networkcredential = new NetworkCredential(UsuarioFTP, SenhaFTP);
        request.Credentials = networkcredential;
        request.Timeout = 3000;

        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {
          // Ler os dados do arquivo
          using (Stream responseStream = response.GetResponseStream())
          {
            // Criar um fluxo de arquivo local
            using (FileStream fileStream = File.Create(diretorio))
            {
              // Copiar os dados do arquivo do servidor FTP para o arquivo local
              responseStream.CopyTo(fileStream);
            }
          }
        }

        string version = "";
        var dsConexao = new DataSet();

        if (File.Exists(diretorio))
        {
          dsConexao = new DataSet();
          dsConexao.ReadXml(diretorio);

          version = dsConexao.Tables[0].Rows[0]["Versao"].ToString();
        }
          // Agora, lemos a versão do executável localmente
        return version;

      }
      catch (Exception ex)
      {
        throw new Exception("Erro ao obter a versão do arquivo do servidor FTP: " + ex.Message);
      }
    }
  }
}

