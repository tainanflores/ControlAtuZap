using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace ControlAtuZap
{
  static class Program
  {
    /// <summary>
    /// Ponto de entrada principal para o aplicativo.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new FormIni());

      //if (IsAdministrator())
      //{
      //  var diretorioProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

      //  var diretorioPrograma = diretorioProgramFiles + @"\AtendimentoZap";

      //  if (!Directory.Exists(diretorioPrograma))
      //  {
      //    try
      //    {
      //      Directory.CreateDirectory(diretorioPrograma);
      //    }
      //    catch (Exception ef)
      //    {
      //      MessageBox.Show(ef.Message);
      //    }
      //  }

       // Application.Run(new FormIni());
      //}
      //else
      //{
        //RestartAsAdmin();
      //}
    }

    static bool IsAdministrator()
    {
      WindowsIdentity identity = WindowsIdentity.GetCurrent();
      WindowsPrincipal principal = new WindowsPrincipal(identity);
      return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    // Reinicia o aplicativo com privilégios elevados
    static void RestartAsAdmin()
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
      startInfo.Verb = "runas"; // Solicita privilégios de administrador
      try
      {
        Process.Start(startInfo);
      }
      catch (System.ComponentModel.Win32Exception)
      {
        // O usuário cancelou a solicitação de privilégios de administrador
        Console.WriteLine("Você precisa de privilégios de administrador para acessar o diretório do Program Files.");
      }
    }
  }
}
