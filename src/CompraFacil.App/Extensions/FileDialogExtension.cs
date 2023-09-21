using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CompraFacil.App.Extensions
{
    public static class FileDialogExtension
    {
        public static FileInfo SaveDialog(this FileDialogFilterBuilder filterBuilder, String fileName)
        {
            return GetFileInfo(new SaveFileDialog(), filterBuilder.Filter, fileName);
        }

        public static FileInfo OpenDialog(this FileDialogFilterBuilder filterBuilder, String fileName)
        {
            return GetFileInfo(new OpenFileDialog(), filterBuilder.Filter, fileName);
        }

        private static FileInfo GetFileInfo(FileDialog fileDialog, String filtro, String fileName)
        {
            FileInfo retorno = null;
            fileDialog.Filter = filtro;
            fileDialog.InitialDirectory = Environment.GetEnvironmentVariable("%USERPROFILE%") + @"\Desktop\";
            fileDialog.FileName = fileName;
            if (DialogResult.OK == fileDialog.ShowDialog())
                retorno = new FileInfo(fileDialog.FileName);
            fileDialog.Dispose();

            return retorno;
        }
    }

    public class FileDialogFilterBuilder
    {
        private readonly List<KeyValuePair<String, String>> _filtros = new List<KeyValuePair<String, String>>();
        public String Filter => String.Join("|", _filtros.Select(i => $"{i.Key}|{i.Value}"));

        public FileDialogFilterBuilder Add(String descricao, params String[] extensoes)
        {
            return add(descricao, extensoes);
        }

        protected virtual FileDialogFilterBuilder add(String descricao, IEnumerable<String> extensoes)
        {
            var extensao = String.Join(";", extensoes);
            _filtros.Add(new KeyValuePair<String, String>(descricao, extensao));
            return this;
        }

        public static FileDialogFilterBuilder New(String descricao, params String[] extensoes)
            => new FileDialogFilterBuilder().Add(descricao, extensoes);
    }
}
