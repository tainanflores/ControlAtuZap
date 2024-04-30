using System.Diagnostics;
using System.IO;

namespace ControlAtuZap.Classes
{
  public class Evento
  {
    public static string verificarVersao(string caminhoFiscal)
    {
      if (File.Exists(caminhoFiscal + @"\ControlAtendimento.exe"))
      {
        FileVersionInfo.GetVersionInfo(Path.Combine(caminhoFiscal, "ControlAtendimento.exe"));
        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(caminhoFiscal + "\\ControlAtendimento.exe");

        return myFileVersionInfo.FileMajorPart + "." + myFileVersionInfo.FileMinorPart + "." + myFileVersionInfo.FileBuildPart.ToString() + "." + myFileVersionInfo.FilePrivatePart;
      }

      return string.Empty;
    }
  }
}

