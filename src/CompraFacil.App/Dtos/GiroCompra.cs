using MPSTI.PlenoSoft.Core.Office.OpenXml.Planilhas.Integracao;

namespace CompraFacil.App.Dtos
{
    public class GiroCompra
    {
        [ExcelColumn("ABC", 1, 5)]
        public char ABC { get; set; }

        [ExcelColumn("Código", 2, 8)]
        public string Codigo { get; set; }

        [ExcelColumn("Descrição", 3, 60)]
        public string Descricao { get; set; }

        [ExcelColumn("QTD Venda", 4, 11)]
        public decimal QTD_Venda { get; set; }

        [ExcelColumn("Giro Mês", 5, 10)]
        public decimal Giro_Mes { get; set; }

        [ExcelColumn("Est Total", 6, 10)]
        public decimal Est_Total { get; set; }

        [ExcelColumn("Pedido", 7, 10)]
        public decimal Pedido { get; set; }

        [ExcelColumn("Custo M", 8, 10)]
        public decimal CustoM { get; set; }

        [ExcelColumn("Custo C", 9, 10)]
        public decimal CustoC { get; set; }

        [ExcelColumn("Verba Mês", 10, 15)]
        public decimal VerbaMes { get; set; }

        [ExcelColumn("Marca", 11, 20)]
        public string Marca { get; set; }

        [ExcelColumn("Família", 12, 20)]
        public string Familia { get; set; }

        [ExcelColumn("Grupo", 13, 20)]
        public string Grupo { get; set; }

        [ExcelColumn("Est 01", 14, 10)]
        public decimal Est_01 { get; set; }

        [ExcelColumn("Est 03", 15, 10)]
        public decimal Est_03 { get; set; }

        [ExcelColumn("Est 04", 16, 10)]
        public decimal Est_04 { get; set; }

        [ExcelColumn("Relevância", 17, 11)]
        public decimal Relevancia { get; set; }

        [ExcelColumn("Verba Acumlada", 18, 18)]
        public decimal VerbaAcumlada { get; set; }

        [ExcelColumn("Verba Total", 19, 18)]
        public decimal VerbaTotal { get; set; }
    }
}