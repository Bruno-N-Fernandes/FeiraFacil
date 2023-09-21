using CompraFacil.App.Dtos;
using CompraFacil.App.Extensions;
using CompraFacil.App.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MPSTI.PlenoSoft.Core.Office.OpenXml.Planilhas.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompraFacil.App
{
    public partial class FormRelatorioCompras : Form
    {
        private readonly IGiroCompraRepository _giroCompraRepository;
        private readonly FileDialogFilterBuilder _fileFilter = FileDialogFilterBuilder.New("Arquivo de Planilhas (Excel)", "*.xlsx");
        private string _action;
        private IEnumerable<GiroCompra> _listaCompras;

        public FormRelatorioCompras(IServiceProvider serviceProvider = null)
        {
            InitializeComponent();
            _giroCompraRepository = serviceProvider.GetRequiredService<IGiroCompraRepository>();
        }

        private async void BtConsultar_Click(object sender, EventArgs e)
        {
            if (Lock("Consultando as Informações"))
            {
                _listaCompras = await _giroCompraRepository.ObterListaCompras();
                dgvAnaliseCompras.DataSource = _listaCompras;
                Unlock();
            }
        }

        private void BtGerarExcel_Click(object sender, EventArgs e)
        {
            if (Lock("Gerando sua Planilha") && !GerarExcel())
                Unlock();
        }

        private void BtSair_Click(object sender, EventArgs e)
        {
            if (Lock(_action))
                Close();
            else if (DialogResult.Yes == MessageBox.Show(this, $"Deseja realmente Sair", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                Close();
        }

        private bool GerarExcel()
        {
            var hoje = DateTime.Today;
            var inicio = hoje.AddDays(-90);
            var nomeArquivo = $"Compras-{inicio:yyyy.MM.dd}-{hoje:yyyy.MM.dd}.xlsx";
            var fileInfo = _fileFilter.SaveDialog(nomeArquivo);
            if (fileInfo != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var plenoExcel = new PlenoExcel(fileInfo, Mode.Seguro);
                    plenoExcel.Export(_listaCompras);
                    plenoExcel.Close();

                    Process.Start(new ProcessStartInfo(fileInfo.FullName) { UseShellExecute = true });
                    Unlock();

                    this.Invoke(new Action(() => MessageBox.Show(this, "Planilha gerada com sucesso", "Uhulll", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                });
                MessageBox.Show(this, "Aguarde enquanto sua planilha é gerada. Ela será aberta automaticamente!", "Só um instante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return (fileInfo != null);
        }

        private bool Lock(string action)
        {
            var result = string.IsNullOrWhiteSpace(_action);
            if (result)
                _action = action;
            else
                MessageBox.Show(this, $"Aguarde! Estamos {_action}", "Só um instante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        private void Unlock() => _action = "";
    }
}