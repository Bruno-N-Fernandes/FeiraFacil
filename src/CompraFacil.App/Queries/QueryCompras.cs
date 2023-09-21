namespace CompraFacil.App.Queries
{
    public static class QueryCompras
    {
        public static string SqlGiroCompras => @"
Drop Table If Exists #tmpGiroCompraRaphael;
Select * Into #tmpGiroCompraRaphael From viewGiroCompraRaphael;

Select Top 10000
	Case 
		When Relevancia <= 80 Then 'A'
		When Relevancia <= 95 Then 'B'
		Else 'C'
	End As ABC
	, Codigo
	, Descricao
	, QTD_Venda
	, Giro_Mes
	, Est_Total
	, Pedido
	, CustoM
	, CustoC
	, VerbaMes
	, Marca
	, Familia
	, Grupo
	, Est_01
	, Est_03
	, Est_04
	, Relevancia
	, VerbaAcumlada
	, VerbaTotal
From (
	Select 
		100.0 * VerbaAcumlada / VerbaTotal As Relevancia
		, *
	From (
		select top 100 percent
			(Select Sum(VerbaMes) From #tmpGiroCompraRaphael g where (g.Giro_Mes >= Giro.Giro_Mes)) As VerbaAcumlada
			, (Select Sum(VerbaMes) From #tmpGiroCompraRaphael) As VerbaTotal
			, *
		From #tmpGiroCompraRaphael Giro

		order by Giro_Mes desc
	) as v
) as v 
order by Giro_Mes desc;
";
    }
}
