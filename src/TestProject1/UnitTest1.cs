using FluentAssertions;
using System.Drawing;

namespace TestProject1
{
    public class UnitTest1
    {

        [Theory]
        [InlineData(1, "um real")]
        [InlineData(2, "dois reais"), InlineData(3, "três reais"), InlineData(4, "quatro reais")]
        [InlineData(5, "cinco reais"), InlineData(6, "seis reais"), InlineData(7, "sete reais")]
        [InlineData(8, "oito reais"), InlineData(9, "nove reais"), InlineData(10, "dez reais")]

        [InlineData(11, "onze reais"), InlineData(12, "doze reais"), InlineData(13, "treze reais")]
        [InlineData(14, "quatorze reais"), InlineData(15, "quinze reais"), InlineData(16, "dezesseis reais")]
        [InlineData(17, "dezessete reais"), InlineData(18, "dezoito reais"), InlineData(19, "dezenove reais")]

        [InlineData(20, "vinte reais"), InlineData(30, "trinta reais"), InlineData(40, "quarenta reais")]
        [InlineData(50, "cinquenta reais"), InlineData(60, "sessenta reais"), InlineData(70, "setenta reais")]
        [InlineData(80, "oitenta reais"), InlineData(90, "noventa reais"), InlineData(100, "cem reais")]
        public void QuandoPedirParaEscreverChequeComNumerosInteiros_DeveRetornaroValorPorExtenso(decimal valor, string valorEsperado)
        {
            var chequePorExtenso = new ChequePorExtenso(valor);

            var porExtenso = chequePorExtenso.Escrever();

            porExtenso.Should().Be(valorEsperado);
        }

        [Theory]
        [InlineData(21, "vinte e um reais")]
        [InlineData(22, "vinte e dois reais")]
        [InlineData(33, "trinta e três reais")]
        [InlineData(234, "duzentos e trinta e quatro reais")]
        public void QuandoPedirParaEscreverChequeComNumerosDezenasUnidades_DeveRetornaroValorPorExtenso(decimal valor, string valorEsperado)
        {
            var chequePorExtenso = new ChequePorExtenso(valor);

            var porExtenso = chequePorExtenso.Escrever();

            porExtenso.Should().Be(valorEsperado);
        }
    }

    public class ChequePorExtenso
    {
        public Decimal Valor { get; private set; }
        public int ValorInteiro => Convert.ToInt32(Valor);
        public int ValorDecimal => Convert.ToInt32((Valor - ValorInteiro) * 100);

        public int Centena => ValorInteiro % 1000;
        public int Dezena => ValorInteiro % 100;
        public int Unidade => ValorInteiro % 10;

        public int DigitoCentena => Centena / 100;
        public int DigitoDezena => Dezena / 10;
        public int DigitoUnidade => Unidade / 1;


        public ChequePorExtenso(decimal valor)
        {
            Valor = valor;
        }

        public string Escrever()
        {
            var retorno = "";
            if (DigitoCentena > 0)
                retorno += GetCentena(DigitoCentena, DigitoDezena + DigitoUnidade > 0);


            if (DigitoDezena <= 1)
                retorno += GetUnidade(ValorInteiro % 100, false);
            else
            {
                retorno += GetDezena(DigitoDezena, DigitoUnidade > 0);
                if (DigitoUnidade > 0)
                    retorno += GetUnidade(DigitoUnidade, false);
            }

            return retorno + GetMoeda(ValorInteiro);
        }

        private string GetMoeda(int valorInteiro)
        {
            return valorInteiro switch
            {
                0 => "",
                1 => " real",
                _ => " reais",
            };
        }

        private static string GetUnidade(int valor, bool plural)
        {
            var retorno = valor switch
            {
                1 => "um",
                2 => "dois",
                3 => "três",
                4 => "quatro",
                5 => "cinco",
                6 => "seis",
                7 => "sete",
                8 => "oito",
                9 => "nove",
                10 => "dez",
                11 => "onze",
                12 => "doze",
                13 => "treze",
                14 => "quatorze",
                15 => "quinze",
                16 => "dezesseis",
                17 => "dezessete",
                18 => "dezoito",
                19 => "dezenove",
                _ => "",
            };
            return retorno;
        }

        private static string GetDezena(int valor, bool plural)
        {
            var retorno = valor switch
            {
                2 => "vinte",
                3 => "trinta",
                4 => "quarenta",
                5 => "cinquenta",
                6 => "sessenta",
                7 => "setenta",
                8 => "oitenta",
                9 => "noventa",
                10 => "cem",
                _ => "",
            };

            return retorno + (plural ? " e " : "");
        }

        private static string GetCentena(int valor, bool plural)
        {
            var retorno = valor switch
            {
                1 => plural ? "cento" : "cem",
                2 => "duzentos",
                3 => "trezentos",
                4 => "quatrocentos",
                5 => "quinhentos",
                6 => "seiscentos",
                7 => "setecentos",
                8 => "oitocentos",
                9 => "novecentos",
                10 => "mil",
                _ => "",
            };

            return retorno + (plural ? " e " : "");
        }
    }
}